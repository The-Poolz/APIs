using System.Collections.Generic;
using System.Linq;

namespace QuickSQL.Helpers
{
    public static class RequestValidator
    {
        public static bool IsValidAPIRequest(Request requestSettings)
        {
            if (!NotNullSelectedTables(requestSettings))
                return false;
            if (!NotNullSelectedColumns(requestSettings))
                return false;
            if (!IsValidJoinCondition(requestSettings))
                return false;
            return true;
        }

        private static bool NotNullSelectedTables(Request requestSettings)
        {
            if (requestSettings.SelectedTables == null || requestSettings.SelectedTables.Length == 0)
                return false;
            return true;
        }
        private static bool NotNullSelectedColumns(Request requestSettings)
        {
            if (requestSettings.SelectedColumns == null || requestSettings.SelectedColumns.Length == 0)
                return false;
            return true;
        }
        private static bool NotNullJoinCondition(Request requestSettings)
        {
            if (requestSettings.JoinCondition == null || requestSettings.JoinCondition.Length == 0)
                return false;
            return true;
        }
        private static bool IsValidJoinCondition(Request requestSettings)
        {
            if (NotNullJoinCondition(requestSettings))
            {
                //Check how many tables
                List<string> names = requestSettings.SelectedTables.Split(",").ToList();
                names = names.Select(name => name.Replace(" ", string.Empty)).ToList();     // Remove all whitespace
                if (names.Count < 2)
                    return false;
            }
            return true;
        }
    }
}
