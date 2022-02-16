using System;
using System.Collections.Generic;
using Interfaces.DBModel;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Poolz
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

        public SqlConnection GetConnection() => new SqlConnection(ConnectionString);
        public SqlDataReader GetReader(string commandQuery, SqlConnection connection) =>
            new SqlCommand(commandQuery, connection).ExecuteReader();

        public string FormatTableToJson(List<object[]> table) =>
            JsonConvert.SerializeObject(table);
        public Dictionary<string, dynamic> FormatJson(string json) =>
            JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);

        public String CreateCommandQuery(string json)
        {
            Dictionary<string, dynamic> data = FormatJson(json);
            if (data == null || data.Count == 0)
                throw new ArgumentException("An error occurred while trying to generate a query string. Missing data.");

            string tableName = data["TableName"];
            if (tableName == null || tableName == string.Empty)
                throw new ArgumentException("An error occurred while trying to generate a query string. Table name missing.");
            data.Remove("TableName");


            if (data.ContainsKey("Id"))
            {
                var id = data["Id"];
                if (id == null)
                    throw new ArgumentException("Parameter 'Id' cannot be null.");
                if (id < 0)
                    throw new ArgumentException("Parameter 'Id' cannot be negative.");
            }

            if (data.ContainsKey("Address"))
            {
                var address = data["Address"];
                if (address == null)
                    throw new ArgumentException("Parameter 'Address' cannot be null.");
                if (address.Length != 42 || !address.StartsWith("0x"))
                    throw new ArgumentException("Invalid address.");
            }

            List<string> selectColumns = new List<string>();
            List<string> conditions = new List<string>();

            foreach (var item in data)
            {
                if (item.Value == null)
                    throw new ArgumentException($"Parameter '{item.Key}' cannot be null.");

                var value = item.Value;
                if (value.GetType() == typeof(String))
                    conditions.Add($"{tableName}.{item.Key} = '{item.Value}'");
                else
                    conditions.Add($"{tableName}.{item.Key} = {item.Value}");

                selectColumns.Add($"{tableName}.{item.Key}");
            }
            if (selectColumns == null || selectColumns.Count == 0)
                throw new ArgumentException("An error occurred while trying to generate a query string. Missing parameters.");

            string condition = string.Join(" AND ", conditions);
            if (tableName.ToLower() == "signup")
                return $"SELECT SignUp.Address, SignUp.PoolId, LeaderBoard.Amount FROM SignUp, LeaderBoard WHERE {condition} AND LeaderBoard.Owner = SignUp.Address";

            string commandQuery = $"SELECT {string.Join(", ", selectColumns)} FROM {tableName} WHERE {condition}";
            Console.WriteLine(commandQuery);
            return commandQuery;
        }
        private List<object[]> GetTableData(string commandQuery)
        {
            List<object[]> table = new List<object[]>();
            using (Context)
            {
                try
                {
                    var connection = GetConnection();
                    using (connection)
                    {
                        connection.Open();
                        var reader = GetReader(commandQuery, connection);
                        if (!reader.HasRows)
                            return null;

                        while (reader.Read())
                        {
                            // Given a SqlDataReader, use the GetValues method to retrieve a full row of data.
                            // Test the GetValues method, passing in an array large enough for all the columns.
                            Object[] values = new Object[reader.FieldCount];
                            int fieldCount = reader.GetValues(values);
                            table.Add(values);
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

        /// <summary>Find table in database as table name, and return selected data in found table.</summary>
        /// <param name="data">JSON data containing table name and conditions.</param>
        /// <returns>Return an JSON string if the table is found.</returns>
        public String GetTable(string data)
        {
            string commandQuery = CreateCommandQuery(data);

            var table = GetTableData(commandQuery);
            return FormatTableToJson(table);
        }

        /// <summary>Joins tables and returns all data of selected tables.</summary>
        /// <param name="leftTableName">Specify left(first) table name.</param>
        /// <param name="rightTableName">Specify right(second) table name.</param>
        /// <param name="conditions">Pass an array of strings with one or more conditions.</param>
        /// <returns>Returns an object if tables joined successfully, or return null if operation failed.</returns>
        public String InnerJoinTables(string leftTableName, string rightTableName, string[] conditions)
        {
            if (leftTableName == null || leftTableName == string.Empty)
                throw new ArgumentException("Value cannot be empty or null.", "string leftTableName");
            if (rightTableName == null || rightTableName == string.Empty)
                throw new ArgumentException("Value cannot be empty or null.", "string leftTableName");
            if (conditions == null || conditions.Length == 0)
                throw new ArgumentException("Value cannot be empty or null.", "string[] conditions");

            var table = GetTableData(
                $"SELECT * " +
                $"FROM {leftTableName} " +
                $"INNER JOIN {rightTableName} " +
                $"ON {string.Join(" AND ", conditions)}");

            return FormatTableToJson(table);
        }

        /// <summary>Joins tables and returns the data of the selected tables.</summary>
        /// <param name="leftTableName">Specify left(first) table name.</param>
        /// <param name="rightTableName">Specify right(second) table name.</param>
        /// <param name="conditions">Pass an array of strings with one or more conditions.</param>
        /// <param name="selectColumns">Specify the column names for the selection.</param>
        /// <returns>Returns an object if tables joined successfully, or return null if operation failed.</returns>
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

            var table = GetTableData(
                $"SELECT {string.Join(", ", selectColumns)} " +
                $"FROM {leftTableName} " +
                $"INNER JOIN {rightTableName} " +
                $"ON {string.Join(" AND ", conditions)}");

            return FormatTableToJson(table);
        }
    }
}
