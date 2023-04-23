using System.Data.Common;
using Cff.Extensions.Effects;
using FluentAssertions;
using Microsoft.Data.Sqlite;

namespace Cff.Extensions.AspNetCore.Tests;

using static IHasDapper<Runtime>;
using static IHasTransaction<Runtime>;

public class DapperSpec
{
    [Fact]
    public async Task Dapper의_트랜잭션_기본_테스트()
    {
        var q = from _1 in CreateTableAff()
                from __ in CommitAff()
                select unit;

        using var cts = new CancellationTokenSource();
        await using var conn = FakeDbConnection();
        await conn.OpenAsync(cts.Token);
        await using var tran = await conn.BeginTransactionAsync();
        var rt = new Runtime(conn, tran, cts);

        var ret = await q.Run(rt);

        ret.ThrowIfFail();
    }

    private DbConnection FakeDbConnection() =>
        new SqliteConnection("Data Source=:memory:");


}

readonly file record struct Runtime
(
    DbConnection DbConnection,
    DbTransaction DbTransaction,
    CancellationTokenSource CancellationTokenSource
) : IHasDapper<Runtime>
{
    DbTransaction IHas<Runtime, DbTransaction>.It => DbTransaction;
    DbConnection IHas<Runtime, DbConnection>.It => DbConnection;

}
