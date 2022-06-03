namespace QuickSQL
{
    /// <summary>
    /// This class is an object that combines the name of the sorted column and the sorting direction.
    /// </summary>
    public class OrderRule
    {
        /// <summary>
        /// Don't use this, as object parameters are only set on initialization.<br/>
        /// This constructor for JSON serializing.
        /// </summary>
        public OrderRule() { }
        /// <summary>
        /// Create a order rule.
        /// </summary>
        /// <param name="columnName">Enter the name of the column to be sorted.</param>
        /// <param name="sort">Enter <see cref="SortBy"/> to customize sorting.<br/>
        /// Default value: <see cref="SortBy.ASC"/> </param>
        public OrderRule(string columnName, SortBy sort = SortBy.ASC)
        {
            ColumnName = columnName;
            Sort = sort;
        }
        /// <summary>
        /// The name of the column that will be sorted.
        /// </summary>
        public string ColumnName { get; init; }
        /// <summary>
        /// Sorting direction. Default value: <see cref="SortBy.ASC"/>
        /// </summary>
        public SortBy Sort { get; init; }
    }
}
