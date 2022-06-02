namespace QuickSQL
{
    public class OrderRule
    {
        public OrderRule(string columnName, SortBy sort = SortBy.ASC)
        {
            ColumnName = columnName;
            Sort = sort;
        }
        public string ColumnName { get; init; }
        public SortBy Sort { get; init; }
    }
}
