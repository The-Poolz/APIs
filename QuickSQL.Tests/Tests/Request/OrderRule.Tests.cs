using Xunit;
using System.Text.Json;

namespace QuickSQL.Tests.Requests
{
    public class OrderRuleTests
    {
        [Fact]
        public static void CreateOrderRule()
        {
            var orederRule = new OrderRule();

            Assert.NotNull(orederRule);
            Assert.IsType<OrderRule>(orederRule);
        }

        [Fact]
        public static void CreateOrderRuleWithParams()
        {
            var orederRule = new OrderRule("Id", SortBy.DESC);

            Assert.NotNull(orederRule);
            Assert.IsType<OrderRule>(orederRule);
        }

        [Fact]
        public static void SerializeObject()
        {
            var orederRule = new OrderRule("Id", SortBy.DESC);

            var result = JsonSerializer.Serialize(orederRule);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal("{\"ColumnName\":\"Id\",\"Sort\":1}", result);
        }
    }
}
