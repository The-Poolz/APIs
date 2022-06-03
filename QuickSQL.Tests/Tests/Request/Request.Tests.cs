using Xunit;
using System.Text.Json;
using System.Collections.ObjectModel;

namespace QuickSQL.Tests.Requests
{
    public static class RequestTests
    {
        [Fact]
        public static void CreateRequestWithParams()
        {
            var request = new Request(
                "TokenBalances",
                new Collection<string> { { "Token" }, { "Owner" }, { "Amount" } },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });

            Assert.NotNull(request);
            Assert.IsType<Request>(request);
        }

        [Fact]
        public static void CreateRequestWithCondition()
        {
            var request = new Request(
                "TokenBalances",
                new Collection<string> { { "Token" }, { "Owner" }, { "Amount" } },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });

            Assert.NotNull(request);
            Assert.IsType<Request>(request);
        }

        [Fact]
        public static void SerializeObject()
        {
            var request = new Request(
                "TokenBalances",
                new Collection<string> { { "Token" }, { "Owner" }, { "Amount" } },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });

            var result = JsonSerializer.Serialize(request);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal("{\"TableName\":\"TokenBalances\",\"SelectedColumns\":[\"Token\",\"Owner\",\"Amount\"],\"WhereConditions\":[{\"ParamName\":\"Id\",\"Operator\":0,\"ParamValue\":\"1\"}],\"OrderRules\":null}", result);
        }
    }
}
