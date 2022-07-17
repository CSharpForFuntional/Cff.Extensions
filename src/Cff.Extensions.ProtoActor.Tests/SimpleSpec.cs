using Proto;
using Cff.Extensions;
using static LanguageExt.Prelude;
using static Cff.Extensions.Prelude;
using LanguageExt;

namespace Cff.Extensions.ProtoActor.Tests;

public class SimpleSpec
{
    [Fact]
    public async Task PingPongAsync()
    {
        var system = new ActorSystem();
        var root = system.Root;
        var pid = root.Spawn(PropsFromFuncAff(ctx => ctx.Message switch
        {
            Hello m => ctx.RespondResultAff(
                Eff(() => "World")
            ).Map(x => unit),
            _ => unitAff,
        }));

        var q = pid.RequestAff<string>(root, new Hello(), 3 * sec);
        var ret = (await q.Run()).ThrowIfFail();

        await system.ShutdownAsync();
    }

    internal record Hello();
}
