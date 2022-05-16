namespace QuickSQL.QueryCreator.Helpers
{
    public static class RequestValidator
    {
        public static bool IsValidRequest(Request request)
            => NotNullTableName(request)
            && NotNullSelectedColumns(request)
            && ConditionsValidator.IsValidWhereCondition(request);

        public static bool NotNullTableName(Request request)
            => request.TableName != null && !string.IsNullOrEmpty(request.TableName.Trim());
        public static bool NotNullSelectedColumns(Request request)
            => request.SelectedColumns != null && !string.IsNullOrEmpty(request.SelectedColumns.Trim());
    }
}
