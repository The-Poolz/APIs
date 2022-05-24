using System.Collections.ObjectModel;

using QuickSQL.DataReader;
using QuickSQL.QueryCreator;

namespace QuickSQL
{
    /// <summary>
    /// Set up an object for work <see cref="QuickSql.InvokeRequest(Request, string, BaseDataReader, BaseQueryCreator)"/>
    /// </summary>
    public class Request
    {
        public Request() { }
        public Request(string tableName, Collection<string> selectedColumns)
        {
            TableName = tableName;
            SelectedColumns = selectedColumns;
        }
        public Request(string tableName, Collection<string> selectedColumns, Collection<Condition> whereConditions)
        {
            TableName = tableName;
            SelectedColumns = selectedColumns;
            WhereConditions = whereConditions;
        }

        /// <summary>
        /// Pass a table from which to take data.<br/>
        /// This is a required parameter.
        /// </summary>
        /// <remarks>
        /// Example: "TableName"
        /// </remarks>
        public string TableName { get; init; }

        /// <summary>
        /// Pass columns from which to take data.<br/>
        /// This is a required parameter.
        /// </summary>
        /// <remarks>
        /// Example: "Id, Name, Address"
        /// </remarks>
        public Collection<string> SelectedColumns { get; init; }

        /// <summary>
        /// Enter condition for search tables.<br/>
        /// Remarks: String parameter must be in quotes(it is default SQL logic)
        /// </summary>
        /// <remarks>
        /// Example: new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
        /// </remarks>
        public Collection<Condition> WhereConditions { get; init; }
    }
}
