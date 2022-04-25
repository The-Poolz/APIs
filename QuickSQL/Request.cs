namespace QuickSQL
{
    /// <summary>
    /// Set up an object for work <see cref="QuickSql.InvokeRequest(Request, string)"/>
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Pass a table from which to take data.<br/>
        /// This is a required parameter.
        /// </summary>
        /// <remarks>
        /// Example: "TableName"
        /// </remarks>
        public string SelectedTable { get; set; }

        /// <summary>
        /// Pass columns from which to take data.<br/>
        /// This is a required parameter.
        /// </summary>
        /// <remarks>
        /// Example : "Id, Name, Address"
        /// </remarks>
        public string SelectedColumns { get; set; }

        /// <summary>
        /// Enter condition for search tables.<br/>
        /// Remarks: String parameter must be in quotes(it is default SQL logic)
        /// </summary>
        /// <remarks>
        /// Example (condition): "FirstTableName.Id = 1" or "Id = 1"<br/>
        /// Example (conditions): "Id = 1, Name = 'String parameter must be in quotes'" 
        /// </remarks>
        public string WhereCondition { get; set; }
    }
}
