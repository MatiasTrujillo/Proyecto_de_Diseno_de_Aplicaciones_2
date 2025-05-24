using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
namespace Simulator.DataAcces.Test;

internal static class ProgramTest
{
    private static readonly SqliteConnection Connection = new("Data Source=:memory:");

    public static SqlContext BuildTestDbContext()
    {
        var options = new DbContextOptionsBuilder<SqlContext>()
            .UseSqlite(Connection)
            .Options;

        Connection.Open();

        var context = new SqlContext(options);

        return context;
    }
}
