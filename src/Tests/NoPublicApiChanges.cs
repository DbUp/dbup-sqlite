using DbUp.Tests.Common;

namespace DbUp.Sqlite.Tests;

public class NoPublicApiChanges : NoPublicApiChangesBase
{
    public NoPublicApiChanges()
        : base(typeof(SqliteExtensions).Assembly)
    {
    }
}
