using Nethereum.Util;
using System.Collections.Generic;
using System.Linq;

namespace UniversalAPI.Helpers
{
    public static class DataChecker
    {
        /// <summary>Get dictionary element by key.</summary>
        /// <param name="data">Dictionary in which to search.</param>
        /// <param name="key">Element key.</param>
        /// <returns>Returns the dictionary element in the object KeyValuePair<string, dynamic>.</returns>
        public static KeyValuePair<string, dynamic>? GetDataItem(Dictionary<string, dynamic> data, string key)
        {
            if (data == null || key == null)
                return null;

            if (!data.ContainsKey(key.ToLower()))
                return null;

            data.TryGetValue(key.ToLower(), out dynamic value);
            return new KeyValuePair<string, dynamic>(key, value);
        }
            
        public static bool CheckId(Dictionary<string, dynamic> data)
        {
            var id = GetDataItem(data, "id");
            if (id == null)
                return true;

            return IsValidId((int?)id.Value.Value);
        }
        public static bool CheckAddress(Dictionary<string, dynamic> data)
        {
            var address = GetDataItem(data, "address");
            if (address == null)
                return true;

            return IsValidAddress(address.Value.Value);
        }
        public static bool CheckOwner(Dictionary<string, dynamic> data)
        {
            var owner = GetDataItem(data, "owner");
            if (owner == null)
                return true;

            return IsValidAddress(owner.Value.Value);
        }

        private static bool IsValidId(int? id)
        {
            if (id == null)
                return false;
            if (id < 0)
                return false;

            return true;
        }
        private static bool IsValidAddress(string address) =>
            AddressExtensions.IsValidEthereumAddressHexFormat(address);
    }
}
