using Interfaces.DBModel;
using Newtonsoft.Json;
using UniversalApi.Helpers;

namespace UniversalApi
{
    public class UniversalAPI
    {
        private readonly string ConnectionString;
        private readonly DynamicDBContext Context;
        public UniversalAPI(string connectionString, DynamicDBContext context)
        {
            ConnectionString = connectionString;
            Context = context;
        }

        public string GetTable(string data)
        {
            string commandQuery = QueryCreator.GetCommandQuery(data);

            object[] table = DataReader.GetData(commandQuery, ConnectionString, Context);

            return JsonConvert.SerializeObject(table);
        }
    }
}
