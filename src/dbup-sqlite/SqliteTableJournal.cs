using System;
using DbUp.Engine;
using DbUp.Engine.Output;
using DbUp.Engine.Transactions;
using DbUp.Support;

namespace DbUp.Sqlite
{
    /// <summary>
    /// An implementation of the <see cref="IJournal"/> interface which tracks version numbers for a
    /// SQLite database using a table called SchemaVersions.
    /// </summary>
    public class SqliteTableJournal : TableJournal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqliteTableJournal"/> class.
        /// </summary>
        /// <param name="connectionManager">The connection manager.</param>
        /// <param name="logger">The log.</param>
        /// <param name="table">The table name.</param>
        public SqliteTableJournal(Func<IConnectionManager> connectionManager, Func<IUpgradeLog> logger, string table) :
            base(connectionManager, logger, new SqliteObjectParser(), null, table)
        { }

        /// <inheritdoc/>
        protected override string GetInsertJournalEntrySql(string @scriptName, string @applied)
        {
            return $"insert into {FqSchemaTableName} (ScriptName, Applied) values ({@scriptName}, {@applied})";
        }

        /// <inheritdoc/>
        protected override string GetJournalEntriesSql()
        {
            return $"select [ScriptName] from {FqSchemaTableName} order by [ScriptName]";
        }

        /// <inheritdoc/>
        protected override string CreateSchemaTableSql(string quotedPrimaryKeyName)
        {
            return
$@"CREATE TABLE {FqSchemaTableName} (
    SchemaVersionID INTEGER CONSTRAINT {quotedPrimaryKeyName} PRIMARY KEY AUTOINCREMENT NOT NULL,
    ScriptName TEXT NOT NULL,
    Applied DATETIME NOT NULL
)";
        }

        /// <inheritdoc/>
        protected override string DoesTableExistSql()
        {
            return $"SELECT count(name) FROM sqlite_master WHERE type = 'table' AND name = '{UnquotedSchemaTableName}'";
        }
    }
}
