using System.Collections.ObjectModel;

namespace QuickSQL.QueryCreator.Helpers
{
    public static class OrderRulesValidator
    {
        public static bool IsValidOrderRules(Collection<OrderRule> orderRules)
        {
            if (orderRules == null || orderRules.Count == 0)
                return true;
            foreach (var rule in orderRules)
            {
                if (!NotNullColumnName(rule.ColumnName)
                    || !IsValidSort(rule.Sort.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool NotNullColumnName(string columnName)
            => columnName != null && !string.IsNullOrEmpty(columnName.Trim());
        public static bool IsValidSort(string sort)
            => sort == "ASC" || sort == "DESC";
    }
}
