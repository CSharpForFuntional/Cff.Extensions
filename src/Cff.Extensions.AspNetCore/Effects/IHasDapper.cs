using System.Data.Common;
using Dapper;

namespace Cff.Extensions.Effects;

public interface IHasDapper<RT> : IHasTransaction<RT>, IHas<RT, DbConnection> where RT : struct, IHasDapper<RT>
{
    public static Aff<RT, Unit> CreateTableAff() =>
        from conn in IHas<RT, DbConnection>.Eff
        from tran in IHas<RT, DbTransaction>.Eff
        from cancel in cancelToken<RT>()
        from _ in Aff(() => conn.ExecuteAsync(new CommandDefinition
        (
            "CREATE TABLE IF NOT EXISTS [Test] ([Id] INTEGER PRIMARY KEY AUTOINCREMENT, [Name] TEXT NOT NULL)",
            transaction: tran,
            cancellationToken: cancel
        )
        {
            
        }).ToValue())
        select unit;
}

