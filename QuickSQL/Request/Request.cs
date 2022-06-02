using System.Collections.ObjectModel;

using QuickSQL.DataReader;
using QuickSQL.QueryCreator;

namespace QuickSQL
{
    /// <summary>
    /// Based on this object, a SQL command is created.<br/>
    /// Set up an object for work <see cref="QuickSql.InvokeRequest(Request, string, BaseDataReader, BaseQueryCreator)"/>.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Don't use this as TableName and SelectedColumns are only set on initialization.
        /// </summary>
        public Request() { }
        /// <summary>
        /// Setting up a request with required fields.
        /// </summary>
        /// <param name="tableName">Pass a table from which to take data.</param>
        /// <param name="selectedColumns">Pass columns from which to take data.</param>
        public Request(string tableName, Collection<string> selectedColumns)
        {
            TableName = tableName;
            SelectedColumns = selectedColumns;
        }
        /// <summary>
        /// Setting up a request with WHERE condition(s).
        /// </summary>
        /// <param name="tableName">Pass a table from which to take data.</param>
        /// <param name="selectedColumns">Pass columns from which to take data.</param>
        /// <param name="whereConditions">Enter condition for search tables.</param>
        public Request(string tableName, Collection<string> selectedColumns, Collection<Condition> whereConditions)
        {
            TableName = tableName;
            SelectedColumns = selectedColumns;
            WhereConditions = whereConditions;
        }

        /// <summary>
        /// Pass a table from which to take data.<br/>
        /// This is a required parameter.<br/>
        /// <example>Example:
        /// <code>Collection&lt;string&gt; selectedCol = new Collection&lt;string&gt; {
        ///     { "Id" }, { "Name" }, { "Address" }
        /// };
        /// 
        /// Request request = new("TableName", selectedCol);</code>
        /// </example>
        /// </summary>
        public string TableName { get; init; }

        /// <summary>
        /// Pass columns from which to take data.<br/>
        /// This is a required parameter.<br/>
        /// <example>Example:
        /// <code>Collection&lt;string&gt; selectedCol = new Collection&lt;string&gt; {
        ///     { "Id" }, { "Name" }, { "Address" }
        /// };
        /// 
        /// Request request = new("TableName", selectedCol);</code>
        /// </example>
        /// </summary>
        public Collection<string> SelectedColumns { get; init; }

        /// <summary>
        /// Enter condition for search tables.<br/>
        /// Remarks: String parameter must be in single quotes.<br/>
        /// <example>Example:
        /// <code>Collection&lt;string&gt; selectedCol = new Collection&lt;string&gt; {
        ///     { "Id" }, { "Name" }, { "Address" }
        /// };
        /// Collection&lt;Condition&gt; conditions = new Collection&lt;Condition&gt; {
        ///     new Condition {
        ///         ParamName = "Name",
        ///         Operator = OperatorName.Equals,
        ///         ParamValue = "'Alex'"
        ///     }
        /// };
        ///         
        /// Request request = new("TableName", selectedCol, conditions);</code>
        /// </example>
        /// </summary>
        public Collection<Condition> WhereConditions { get; init; }
    }
}
