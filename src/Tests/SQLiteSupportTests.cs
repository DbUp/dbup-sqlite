using Shouldly;
using Xunit;

namespace DbUp.SQLite.Tests
{
    public class SQLiteSupportTests
    {
        static readonly string DbFilePath = Path.Combine(Environment.CurrentDirectory, "test.db");

        [Fact]
        public void CanUseSQLite()
        {
            var connectionString = $"Data Source={DbFilePath}";

            var upgrader = DeployChanges.To
                .SQLiteDatabase(connectionString)
                .WithScript("Script0001", "CREATE TABLE IF NOT EXISTS Foo (Id int)")
                .Build();

            var result = upgrader.PerformUpgrade();
            
            result.Error.ShouldBe(null);
            result.Successful.ShouldBe(true);
        }

        [Fact]
        public void DoesNotExhibitSafeHandleError_Issue577()
        {
            var connectionString = "Data source=:memory:";

            var upgrader =
                DeployChanges.To
                    .SQLiteDatabase(connectionString)
                    .WithScript("Script001", @"
create table test (
    contact_id INTEGER PRIMARY KEY
);
")
                    .LogScriptOutput()
                    .LogToConsole()
                    .Build();
            var result = upgrader.PerformUpgrade();
            result.Successful.ShouldBeTrue();
        }
    }
}
