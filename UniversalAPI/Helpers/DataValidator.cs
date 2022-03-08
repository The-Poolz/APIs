using Interfaces.DBModel;
using Nethereum.Util;
using System.Collections.Generic;
using System.Linq;

namespace UniversalApi.Helpers
{
    public static class DataValidator
    {
        public static dynamic GetRequest(Dictionary<string, dynamic> data)
        {
            if (data.ContainsKey("Request") || data.ContainsKey("request"))
            {
                dynamic requestName;
                if (data.TryGetValue("Request", out requestName))
                    return requestName;
                if (data.TryGetValue("request", out requestName))
                    return requestName;
            }
            return null;
        }
        public static dynamic GetId(Dictionary<string, dynamic> data)
        {
            if (data.ContainsKey("Id") || data.ContainsKey("id"))
            {
                dynamic id;
                if (data.TryGetValue("Id", out id))
                    return id;
                if (data.TryGetValue("id", out id))
                    return id;
            }
            return null;
        }
        public static dynamic GetAddress(Dictionary<string, dynamic> data)
        {
            if (data.ContainsKey("Address") || data.ContainsKey("address"))
            {
                dynamic address;
                if (data.TryGetValue("Address", out address))
                    return address;
                if (data.TryGetValue("address", out address))
                    return address;
            }
            return null;
        }
        public static dynamic GetOwner(Dictionary<string, dynamic> data)
        {
            if (data.ContainsKey("Owner") || data.ContainsKey("owner"))
            {
                dynamic owner;
                if (data.TryGetValue("Owner", out owner))
                    return owner;
                if (data.TryGetValue("owner", out owner))
                    return owner;
            }
            return null;
        }

        public static bool HasRequest(Dictionary<string, dynamic> data, out string tables)
        {
            return IsValidRequestName(GetRequest(data), out tables);
        }
        public static bool IsValidRequestName(string requestName, out string tables)
        {
            tables = string.Empty;
            using DynamicDBContext context = DynamicDB.ConnectToDb();
            var request = context.APIRequestList.FirstOrDefault(p => p.Request == requestName);
            if (request != null && request.Tables != string.Empty)
            {
                tables = request.Tables;
                return true;
            }
            return false;
        }

        public static bool CheckId(Dictionary<string, dynamic> data)
        {
            var id = GetId(data);
            if (id == null)
                return true;

            return IsValidId((int?)GetId(data));
        }
        public static bool IsValidId(int? id)
        {
            if (id == null)
                return false;
            if (id < 0)
                return false;

            return true;
        }

        public static bool CheckAddress(Dictionary<string, dynamic> data)
        {
            var address = GetAddress(data);
            if (address == null)
                return true;

            return IsValidAddress(address);
        }
        public static bool IsValidAddress(string address)
        {
            bool result = AddressExtensions.IsValidEthereumAddressHexFormat(address);
            if (!result)
                return false;
            return true;
        }

        public static bool CheckOwner(Dictionary<string, dynamic> data)
        {
            var owner = GetOwner(data);
            if (owner == null)
                return true;

            return IsValidAddress(owner);
        }
    }
}
