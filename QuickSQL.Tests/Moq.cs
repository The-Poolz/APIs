using Moq;
using System.Data;
using System.Text.Json;
using System.Collections.Generic;

namespace QuickSQL.Tests
{
    public static class Mock
    {
        public static IDataReader MockIDataReader(List<TokenBalances> objectsToEmulate)
        {
            var moq = new Mock<IDataReader>();

            // This var stores current position in 'objectsToEmulate' list
            int count = 0;

            moq.Setup(x => x.Read())
                // Return 'True' while list still has an item
                .Returns(() => count < objectsToEmulate.Count - 1)
                // Go to next position
                .Callback(() => count++);

            moq.Setup(x => x.GetValue(count)).Returns(JsonSerializer.Serialize(objectsToEmulate[count]));

            return moq.Object;
        }
    }
}
