using Microsoft.Data.Sqlite;
using Shouldly;
using Xunit;

namespace DbUp.SQLite.Tests
{
    public class SQLiteSupportTests
    {
        static readonly string dbFilePath = Path.Combine(Environment.CurrentDirectory, "test.db");

        [Fact]
        public void CanUseSQLite()
        {
            var connectionString = $"Data Source={dbFilePath}";

            var upgrader = DeployChanges.To
                .SQLiteDatabase(connectionString)
                .WithScript("Script0001", "CREATE TABLE IF NOT EXISTS Foo (Id int)")
                .Build();

            var result = upgrader.PerformUpgrade();
            
            result.Error.ShouldBe(null);
            result.Successful.ShouldBe(true);
        }
    }
}
