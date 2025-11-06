using System;
using System.Data;

namespace DbUp.Sqlite.Helpers
{
    /// <summary>
    /// A database connection wrapper to manage underlying connection as a shared connection
    /// during database upgrade.
    /// <remarks>
    /// if underlying connection is already opened then it will be kept as opened and will not be closed
    /// otherwise it will be opened when object is created and closed when object is disposed
    /// however it will not be disposed
    /// </remarks>
    /// </summary>
    public class SharedConnection : IDbConnection
    {
        readonly bool connectionAlreadyOpened;
        readonly IDbConnection connection;

        /// <summary>
        /// Constructs a new instance
        /// </summary>
        public SharedConnection(IDbConnection dbConnection)
        {
            connection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection), "database connection is null");

            if (connection.State == ConnectionState.Open)
                connectionAlreadyOpened = true;
            else
                connection.Open();
        }

        /// <inheritdoc/>
        public IDbTransaction BeginTransaction(IsolationLevel il) => connection.BeginTransaction(il);

        /// <inheritdoc/>
        public IDbTransaction BeginTransaction() => connection.BeginTransaction();

        /// <inheritdoc/>
        public void ChangeDatabase(string databaseName) => connection.ChangeDatabase(databaseName);

        /// <inheritdoc/>
        public void Close() { } // shared underlying connection is not closed 

        /// <inheritdoc/>
        public string ConnectionString
        {
            get => connection.ConnectionString;
            set => connection.ConnectionString = value;
        }

        /// <inheritdoc/>
        public int ConnectionTimeout => connection.ConnectionTimeout;

        /// <inheritdoc/>
        public IDbCommand CreateCommand() => connection.CreateCommand();

        /// <inheritdoc/>
        public string Database => connection.Database;

        /// <inheritdoc/>
        public void Open()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }

        /// <inheritdoc/>
        public ConnectionState State => connection.State;

        /// <inheritdoc/>
        public void Dispose() { } // shared underlying connection is not disposed

        /// <summary>
        /// Closes the connection if it was opened by this wrapper.
        /// </summary>
        public void DoClose()
        {
            // if shared underlying connection is opened by this object
            // it will be closed here, otherwise the connection is not closed 
            if (!connectionAlreadyOpened && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
