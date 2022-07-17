using Proto;

namespace Cff.Extensions;

public partial class Prelude
{
    public static Props PropsFromFuncAff(Func<IContext, Aff<Unit>> receiveAff) =>
        Props.FromFunc(async ctx => await receiveAff(ctx).Run());
}
