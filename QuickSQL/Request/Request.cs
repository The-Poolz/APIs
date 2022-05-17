using QuickSQL.DataReader;
using QuickSQL.QueryCreator;
using System.Collections.ObjectModel;

namespace QuickSQL
{
    /// <summary>
    /// Set up an object for work <see cref="QuickSql.InvokeRequest(Request, string, BaseDataReader, BaseQueryCreator)"/>
    /// </summary>
    public class Request
    {
        public Request(string tableName, string selectedColumns)
        {
            TableName = tableName;
            SelectedColumns = selectedColumns;
        }
        public Request(string tableName, string selectedColumns, Collection<Condition> whereConditions)
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
        readonly string TableName;

        /// <summary>
        /// Pass columns from which to take data.<br/>
        /// This is a required parameter.
        /// </summary>
        /// <remarks>
        /// Example: "Id, Name, Address"
        /// </remarks>
        readonly string SelectedColumns;

        /// <summary>
        /// Enter condition for search tables.<br/>
        /// Remarks: String parameter must be in quotes(it is default SQL logic)
        /// </summary>
        /// <remarks>
        /// Example: new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
        /// </remarks>
        readonly Collection<Condition> WhereConditions;
    }
}
