using Proto;

namespace Cff.Extensions;

public static class ProtoActorExtensions
{
    public static Aff<T> RequestAff<T>(this PID pid, ISenderContext root, object msg, TimeSpan timeout) =>
        use(Eff(() => new CancellationTokenSource(timeout)), cts =>
        use(Eff(() => CancellationTokenSource.CreateLinkedTokenSource(cts.Token, root.System.Shutdown)), cts1 =>
            Aff(() => root.RequestAsync<T>(pid, msg, cts1.Token).ToValue())));

    public static Aff<T> RespondResultAff<T>(this IContext context, Aff<T> aff) =>
        from _1 in aff
        from _2 in Eff(() =>
        {
            context.Respond(_1);
            return unit;
        })
        select _1;

    public static Aff<T> RespondResultAff<T>(this IContext context, Eff<T> eff) =>
       RespondResultAff(context, eff.ToAff());
}
