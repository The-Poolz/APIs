namespace QuickSQL.Helpers
{
    public static class RequestValidator
    {
        public static bool IsValidAPIRequest(Request requestSettings)
            => NotNullSelectedTables(requestSettings) && NotNullSelectedColumns(requestSettings);

        private static bool NotNullSelectedTables(Request requestSettings)
            => requestSettings.SelectedTable != null && requestSettings.SelectedTable.Length != 0;
        private static bool NotNullSelectedColumns(Request requestSettings)
            => requestSettings.SelectedColumns != null || requestSettings.SelectedColumns.Length != 0;
    }
}
