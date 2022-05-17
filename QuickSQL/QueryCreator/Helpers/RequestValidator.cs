namespace QuickSQL.QueryCreator.Helpers
{
    public static class RequestValidator
    {
        public static bool IsValidRequest(Request request)
            => NotNullTableName(request.TableName)
            && NotNullSelectedColumns(request.SelectedColumns)
            && ConditionsValidator.IsValidWhereCondition(request.WhereConditions);

        public static bool NotNullTableName(string tableName)
            => tableName != null && !string.IsNullOrEmpty(tableName.Trim());
        public static bool NotNullSelectedColumns(string selectedColumns)
            => selectedColumns != null && !string.IsNullOrEmpty(selectedColumns.Trim());
    }
}
