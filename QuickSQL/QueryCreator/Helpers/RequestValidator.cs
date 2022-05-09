namespace QuickSQL.QueryCreator.Helpers
{
    public static class RequestValidator
    {
        public static bool IsValidRequest(Request requestSettings)
            => NotNullTableName(requestSettings) && NotNullSelectedColumns(requestSettings);

        public static bool NotNullTableName(Request requestSettings)
            => requestSettings.TableName != null && requestSettings.TableName.Length != 0;
        public static bool NotNullSelectedColumns(Request requestSettings)
            => requestSettings.SelectedColumns != null && requestSettings.SelectedColumns.Length != 0;
    }
}
