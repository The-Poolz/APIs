using System.Collections.ObjectModel;

namespace QuickSQL.QueryCreator.Helpers
{
    public static class RequestValidator
    {
        public static bool IsValidRequest(Request request)
            => NotNullTableName(request.TableName)
            && NotNullSelectedColumns(request.SelectedColumns)
            && ConditionsValidator.IsValidWhereCondition(request.WhereConditions)
            && OrderRulesValidator.IsValidOrderRules(request.OrderRules);

        public static bool NotNullTableName(string tableName)
            => tableName != null && !string.IsNullOrEmpty(tableName.Trim());
        public static bool NotNullSelectedColumns(Collection<string> selectedColumns)
            => selectedColumns != null && selectedColumns.Count != 0;
    }
}
