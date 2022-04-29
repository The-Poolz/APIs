using QuickSQL.QueryCreators;
using Xunit;
using System;

namespace QuickSQL.Tests.DataReader
{
    public class BaseDataReader
    {
        [Fact]
        public void GetJsonData()
        {
            MySqlDataReader reader = new MySqlDataReader();
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereCondition = "Id = 1"
            };
            string expected = "[{\"Owner\": \"0x1a01ee5577c9d69c35a77496565b1bc95588b521\", \"Token\": \"ADH\", \"Amount\": \"400\"}]";
            string commandQuery = MySqlQueryCreator.CreateCommandQuery(request);
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            string connectionString;
            if (Convert.ToBoolean(isTravisCi))
                connectionString = Environment.GetEnvironmentVariable("TravisCIConnectionString");
            else
                connectionString = LocalConnection.MySqlConnection;

            // Act
            var result = reader.GetJsonData(commandQuery, connectionString);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetJsonDataEmptyJsonResult()
        {
            MySqlDataReader reader = new MySqlDataReader();
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereCondition = "Id = 40"
            };
            string expected = "[]";
            string commandQuery = MySqlQueryCreator.CreateCommandQuery(request);
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            string connectionString;
            if (Convert.ToBoolean(isTravisCi))
                connectionString = Environment.GetEnvironmentVariable("TravisCIConnectionString");
            else
                connectionString = LocalConnection.MySqlConnection;

            // Act
            string result = reader.GetJsonData(commandQuery, connectionString);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetProviderName()
        {
            MySqlDataReader reader = new MySqlDataReader();
            string expected = Providers.MySql.ToString();

            // Act
            string result = reader.ProviderName;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }
    }
}
