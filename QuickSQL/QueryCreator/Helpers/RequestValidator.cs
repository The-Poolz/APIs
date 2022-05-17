namespace QuickSQL.QueryCreator.Helpers
{
    public static class RequestValidator
    {
        public static bool IsValidRequest(Request request)
            => NotNullTableName(request.GetTableName())
            && NotNullSelectedColumns(request.GetSelectedColumns())
            && ConditionsValidator.IsValidWhereCondition(request.GetWhereConditions());

        public static bool NotNullTableName(string tableName)
            => tableName != null && !string.IsNullOrEmpty(tableName.Trim());
        public static bool NotNullSelectedColumns(string selectedColumns)
            => selectedColumns != null && !string.IsNullOrEmpty(selectedColumns.Trim());
    }
}
