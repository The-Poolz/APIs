namespace QuickSQL.Tests
{
    public static class LocalConnection
    {
        public const string MySqlConnection = @"server=127.0.0.1;user id=root;password=stas2526;database=QuickSQL.Test";
        public const string MicrosoftSqlServerConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UniversalApi.Example.Data;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    }
}
