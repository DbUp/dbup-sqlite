[assembly: System.CLSCompliant(true)]
[assembly: System.Reflection.AssemblyMetadata("RepositoryUrl", "https://github.com/DbUp/dbup-sqlite.git")]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: System.Runtime.InteropServices.Guid("9f949414-f078-49bf-b50e-a3859c18fb6e")]
[assembly: System.Runtime.Versioning.TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName=".NET Standard 2.0")]
public static class SqliteExtensions
{
    public static DbUp.Builder.UpgradeEngineBuilder JournalToSqliteTable(this DbUp.Builder.UpgradeEngineBuilder builder, string table) { }
    public static DbUp.Builder.UpgradeEngineBuilder SqliteDatabase(this DbUp.Builder.SupportedDatabases supported, DbUp.Sqlite.Helpers.SharedConnection sharedConnection) { }
    public static DbUp.Builder.UpgradeEngineBuilder SqliteDatabase(this DbUp.Builder.SupportedDatabases supported, string connectionString) { }
}
namespace DbUp.Sqlite
{
    public class SqliteConnectionManager : DbUp.Engine.Transactions.DatabaseConnectionManager
    {
        public SqliteConnectionManager(DbUp.Sqlite.Helpers.SharedConnection sharedConnection) { }
        public SqliteConnectionManager(string connectionString) { }
        public override System.Collections.Generic.IEnumerable<string> SplitScriptIntoCommands(string scriptContents) { }
    }
    public class SqliteObjectParser : DbUp.Support.SqlObjectParser
    {
        public SqliteObjectParser() { }
    }
    public class SqlitePreprocessor : DbUp.Engine.IScriptPreprocessor
    {
        public SqlitePreprocessor() { }
        public string Process(string contents) { }
    }
    public class SqliteScriptExecutor : DbUp.Support.ScriptExecutor
    {
        public SqliteScriptExecutor(System.Func<DbUp.Engine.Transactions.IConnectionManager> connectionManagerFactory, System.Func<DbUp.Engine.Output.IUpgradeLog> log, string schema, System.Func<bool> variablesEnabled, System.Collections.Generic.IEnumerable<DbUp.Engine.IScriptPreprocessor> scriptPreprocessors, System.Func<DbUp.Engine.IJournal> journalFactory) { }
        protected override void ExecuteCommandsWithinExceptionHandler(int index, DbUp.Engine.SqlScript script, System.Action executeCommand) { }
        protected override string GetVerifySchemaSql(string schema) { }
    }
    public class SqliteTableJournal : DbUp.Support.TableJournal
    {
        public SqliteTableJournal(System.Func<DbUp.Engine.Transactions.IConnectionManager> connectionManager, System.Func<DbUp.Engine.Output.IUpgradeLog> logger, string table) { }
        protected override string CreateSchemaTableSql(string quotedPrimaryKeyName) { }
        protected override string DoesTableExistSql() { }
        protected override string GetInsertJournalEntrySql(string scriptName, string applied) { }
        protected override string GetJournalEntriesSql() { }
    }
}
namespace DbUp.Sqlite.Helpers
{
    public class InMemorySqliteDatabase : System.IDisposable
    {
        public InMemorySqliteDatabase() { }
        public string ConnectionString { get; set; }
        public DbUp.Helpers.AdHocSqlRunner SqlRunner { get; }
        public void Dispose() { }
        public DbUp.Engine.Transactions.IConnectionManager GetConnectionManager() { }
    }
    public class SharedConnection : System.Data.IDbConnection, System.IDisposable
    {
        public SharedConnection(System.Data.IDbConnection dbConnection) { }
        public string ConnectionString { get; set; }
        public int ConnectionTimeout { get; }
        public string Database { get; }
        public System.Data.ConnectionState State { get; }
        public System.Data.IDbTransaction BeginTransaction() { }
        public System.Data.IDbTransaction BeginTransaction(System.Data.IsolationLevel il) { }
        public void ChangeDatabase(string databaseName) { }
        public void Close() { }
        public System.Data.IDbCommand CreateCommand() { }
        public void Dispose() { }
        public void DoClose() { }
        public void Open() { }
    }
    public class TemporarySqliteDatabase : System.IDisposable
    {
        public TemporarySqliteDatabase(string name) { }
        public DbUp.Sqlite.Helpers.SharedConnection SharedConnection { get; }
        public DbUp.Helpers.AdHocSqlRunner SqlRunner { get; }
        public void Dispose() { }
    }
}