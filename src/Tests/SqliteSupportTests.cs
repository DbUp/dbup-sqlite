using Shouldly;
using Xunit;

namespace DbUp.Sqlite.Tests
{
    public class SqliteSupportTests
    {
        static readonly string DbFilePath = Path.Combine(Environment.CurrentDirectory, "test.db");

        [Fact]
        public void CanUseSqlite()
        {
            var connectionString = $"Data Source={DbFilePath}";

            var upgrader = DeployChanges.To
                .SqliteDatabase(connectionString)
                .WithScript("Script0001", "CREATE TABLE IF NOT EXISTS Foo (Id int)")
                .Build();

            var result = upgrader.PerformUpgrade();
            
            result.Error.ShouldBe(null);
            result.Successful.ShouldBe(true);
        }

        /// <summary>
        /// Test for https://github.com/DbUp/dbup-sqlite/issues/2
        /// </summary>
        [Fact]
        public void DoesNotExhibitSafeHandleError()
        {
            var connectionString = "Data source=:memory:";

            var upgrader =
                DeployChanges.To
                    .SqliteDatabase(connectionString)
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
