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
            return true;
        }

        private static bool NotNullSelectedTables(Request requestSettings)
        {
            if (requestSettings.SelectedTable == null || requestSettings.SelectedTable.Length == 0)
                return false;
            return true;
        }
        private static bool NotNullSelectedColumns(Request requestSettings)
        {
            if (requestSettings.SelectedColumns == null || requestSettings.SelectedColumns.Length == 0)
                return false;
            return true;
        }
    }
}
