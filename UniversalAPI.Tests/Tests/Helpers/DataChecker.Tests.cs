using System.Collections.Generic;
using UniversalAPI.Helpers;
using Xunit;

namespace UniversalAPI.Tests.Helpers
{
    public class DataCheckerTests
    {
        [Fact]
        public void GetDataItem()
        {
            Dictionary<string, dynamic> dataObj = new Dictionary<string, dynamic>
            {
                { "Id", 3 },
                { "address", "0x3a31ee5557c9369c35573496555b1bc93553b553" }
            };
            string key = "Address";

            var result = DataChecker.GetDataItem(dataObj, key);

            Assert.NotNull(result);
            var resultType = Assert.IsType<KeyValuePair<string, dynamic>>(result);
            Assert.IsAssignableFrom<KeyValuePair<string, dynamic>>(resultType);
            Assert.Equal(result.Value.Value, dataObj["address"]);
        }
    }
}
