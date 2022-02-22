using System;
using Interfaces.DBModel;
using Microsoft.Data.SqlClient;
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

        private Json<string, dynamic> GetData(string commandQuery)
        {
            Json<string, dynamic> table = new Json<string, dynamic>();
            using (Context)
            {
                try
                {
                    var connection = SqlUtil.GetConnection(ConnectionString);
                    using (connection)
                    {
                        connection.Open();
                        var reader = SqlUtil.GetReader(commandQuery, connection);
                        if (!reader.HasRows)
                            return null;

                        int i = 0;
                        while (reader.Read())
                        {
                            Object[] values = new Object[reader.FieldCount];
                            reader.GetValues(values);
                            foreach (var value in values)
                            {
                                table.Add(reader.GetName(i), value);
                            }
                            i++;
                        }
                        reader.Close();
                    }
                    return table;
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return table;
        }

        public String GetTable(string data)
        {
            string commandQuery = DataFormatter.CreateCommandQuery(data);

            Json<string, dynamic> table = GetData(commandQuery);

            return table.GetJsonString();
        }

        public String InnerJoinTables(string leftTableName, string rightTableName, string[] conditions)
        {
            if (leftTableName == null || leftTableName == string.Empty)
                throw new ArgumentException("Value cannot be empty or null.", "string leftTableName");
            if (rightTableName == null || rightTableName == string.Empty)
                throw new ArgumentException("Value cannot be empty or null.", "string leftTableName");
            if (conditions == null || conditions.Length == 0)
                throw new ArgumentException("Value cannot be empty or null.", "string[] conditions");

            var table = GetData(
                $"SELECT * " +
                $"FROM {leftTableName} " +
                $"INNER JOIN {rightTableName} " +
                $"ON {string.Join(" AND ", conditions)}");

            return table.GetJsonString();
        }

        public String InnerJoinTables(string leftTableName, string rightTableName, string[] conditions, string[] selectColumns)
        {
            if (leftTableName == null || leftTableName == string.Empty)
                throw new ArgumentException("Value cannot be empty or null.", "string leftTableName");
            if (rightTableName == null || rightTableName == string.Empty)
                throw new ArgumentException("Value cannot be empty or null.", "string leftTableName");
            if (conditions == null || conditions.Length == 0)
                throw new ArgumentException("Value cannot be empty or null.", "string[] conditions");
            if (selectColumns == null || selectColumns.Length == 0)
                throw new ArgumentException("Value cannot be empty or null.", "string[] selectColumns");

            var table = GetData(
                $"SELECT {string.Join(", ", selectColumns)} " +
                $"FROM {leftTableName} " +
                $"INNER JOIN {rightTableName} " +
                $"ON {string.Join(" AND ", conditions)}");

            return table.GetJsonString();
        }
    }
}
