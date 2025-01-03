﻿using DbUp.Builder;
using DbUp.Sqlite;
using DbUp.Sqlite.Helpers;

/// <summary>
/// Configuration extension methods for SQLite (see http://www.sqlite.org/)
/// </summary>
// NOTE: DO NOT MOVE THIS TO A NAMESPACE
// Since the class just contains extension methods, we leave it in the root so that it is always discovered
// and people don't have to manually add using statements.
// ReSharper disable CheckNamespace
public static class SqliteExtensions
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// Creates an upgrader for SQLite databases.
    /// </summary>
    /// <param name="supported">Fluent helper type.</param>
    /// <param name="connectionString">SQLite database connection string</param>
    /// <returns>
    /// A builder for a database upgrader designed for SQLite databases.
    /// </returns>
    public static UpgradeEngineBuilder SqliteDatabase(this SupportedDatabases supported, string connectionString)
    {
        var builder = new UpgradeEngineBuilder();
        builder.Configure(c => c.ConnectionManager = new SqliteConnectionManager(connectionString));
        builder.Configure(c => c.Journal = new SqliteTableJournal(() => c.ConnectionManager, () => c.Log, "SchemaVersions"));
        builder.Configure(c => c.ScriptExecutor = new SqliteScriptExecutor(() => c.ConnectionManager, () => c.Log, null,
            () => c.VariablesEnabled, c.ScriptPreprocessors, () => c.Journal));
        builder.WithPreprocessor(new SqlitePreprocessor());
        return builder;
    }

    /// <summary>
    /// Creates an upgrader for SQLite databases.
    /// </summary>
    /// <param name="supported">Fluent helper type.</param>
    /// <param name="sharedConnection">SQLite database connection which you control when it is closed</param>
    /// <returns>
    /// A builder for a database upgrader designed for SQLite databases.
    /// </returns>
    public static UpgradeEngineBuilder SqliteDatabase(this SupportedDatabases supported, SharedConnection sharedConnection)
    {
        var builder = new UpgradeEngineBuilder();
        builder.Configure(c => c.ConnectionManager = new SqliteConnectionManager(sharedConnection));
        builder.Configure(c => c.Journal = new SqliteTableJournal(() => c.ConnectionManager, () => c.Log, "SchemaVersions"));
        builder.Configure(c => c.ScriptExecutor = new SqliteScriptExecutor(() => c.ConnectionManager, () => c.Log, null,
            () => c.VariablesEnabled, c.ScriptPreprocessors, () => c.Journal));
        builder.WithPreprocessor(new SqlitePreprocessor());
        return builder;
    }
    
    /// <summary>
    /// Tracks the list of executed scripts in a custom SQLite table.
    /// </summary>
    /// <param name="table">The name of the table used to store the list of executed scripts.</param>
    /// <returns>The <see cref="UpgradeEngineBuilder"/> used to set the journal table name.</returns>
    public static UpgradeEngineBuilder JournalToSqliteTable(this UpgradeEngineBuilder builder, string table)
    {
        builder.Configure(c => c.Journal = new SqliteTableJournal(() => c.ConnectionManager, () => c.Log, table));
        return builder;
    }
}
