using System.Data.Common;

namespace Cff.Extensions.Effects;

public interface IHasTransaction<RT> : IHas<RT, DbTransaction> where RT : struct, IHasTransaction<RT>
{
    public static Aff<RT, Unit> CommitAff() =>
        from trans in Eff
        from cancel in cancelToken<RT>()
        from _ in Aff(() => trans.CommitAsync(cancel).ToUnit().ToValue())
        select unit;
}

