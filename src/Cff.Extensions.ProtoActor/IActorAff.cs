using Proto;

namespace Cff.Extensions;

public interface IActorAff : IActor
{
    Aff<Unit> ReceiveAff(IContext context);

    async Task IActor.ReceiveAsync(IContext context) => await ReceiveAff(context).Run();
}
