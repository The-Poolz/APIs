using QuickSQL.DataReader;
using QuickSQL.QueryCreator;
using System.Collections.Generic;

namespace QuickSQL
{
    /// <summary>
    /// Set up an object for work <see cref="QuickSql.InvokeRequest(Request, string, BaseDataReader, BaseQueryCreator)"/>
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
        public string TableName { get; set; }

        /// <summary>
        /// Pass columns from which to take data.<br/>
        /// This is a required parameter.
        /// </summary>
        /// <remarks>
        /// Example: "Id, Name, Address"
        /// </remarks>
        public string SelectedColumns { get; set; }

        /// <summary>
        /// Enter condition for search tables.<br/>
        /// Remarks: String parameter must be in quotes(it is default SQL logic)
        /// </summary>
        /// <remarks>
        /// Example: 
        /// </remarks>
        public List<Condition> WhereConditions { get; set; }
    }
}
