namespace Interfaces
{
    public static class ConnectionString
    {
        public static string ConnectionToData = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UniversalApi.Example.Data;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string ConnectionToApi = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UniversalApi.Example.API;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    }
}
