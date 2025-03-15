namespace Irt.Application.Helpers;

public class DatabaseSettings
{
    public string DefaultDatabaseType { get; set; } = "Postgres"; // Default to Couchbase
    public string Scope { get; } = "irt";
}