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
        /// Don't use this, as object parameters are only set on initialization.<br/>
        /// This constructor for JSON serializing.
        /// </summary>
        public Request() { }
        /// <summary>
        /// Setting up a default request.
        /// </summary>
        /// <param name="tableName">Pass a table from which to take data.</param>
        /// <param name="selectedColumns">Pass column(s) from which to take data.</param>
        public Request(string tableName, Collection<string> selectedColumns)
        {
            TableName = tableName;
            SelectedColumns = selectedColumns;
        }
        /// <summary>
        /// Setting up a request with WHERE condition(s).
        /// </summary>
        /// <param name="tableName">Pass a table from which to take data.</param>
        /// <param name="selectedColumns">Pass column(s) from which to take data.</param>
        /// <param name="whereConditions">Enter condition(s) for search tables.</param>
        public Request(string tableName, Collection<string> selectedColumns, Collection<Condition> whereConditions)
        {
            TableName = tableName;
            SelectedColumns = selectedColumns;
            WhereConditions = whereConditions;
        }
        /// <summary>
        /// Setting up a request with ORDER BY rule(s).
        /// </summary>
        /// <param name="tableName">Pass a table from which to take data.</param>
        /// <param name="selectedColumns">Pass column(s) from which to take data.</param>
        /// <param name="orderRules">Enter order rule(s).</param>
        public Request(string tableName, Collection<string> selectedColumns, Collection<OrderRule> orderRules)
        {
            TableName = tableName;
            SelectedColumns = selectedColumns;
            OrderRules = orderRules;
        }
        /// <summary>
        /// Setting up a request with WHERE condition(s) and ORDER BY rule(s).
        /// </summary>
        /// <param name="tableName">Pass a table from which to take data.</param>
        /// <param name="selectedColumns">Pass column(s) from which to take data.</param>
        /// <param name="whereConditions">Enter condition(s) for search tables.</param>
        /// <param name="orderRules">Enter order rule(s).</param>
        public Request(string tableName, Collection<string> selectedColumns, Collection<Condition> whereConditions, Collection<OrderRule> orderRules)
        {
            TableName = tableName;
            SelectedColumns = selectedColumns;
            WhereConditions = whereConditions;
            OrderRules = orderRules;
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
        /// Pass column(s) from which to take data.<br/>
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
        /// Enter condition(s).<br/>
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

        /// <summary>
        /// Enter condition for search tables.<br/>
        /// <example>Example:
        /// <code>Collection&lt;string&gt; selectedCol = new Collection&lt;string&gt; {
        ///     { "Id" }, { "Name" }, { "Address" }
        /// };
        /// Collection&lt;OrderRule&gt; orderRules = new Collection&lt;OrderRule&gt; {
        ///     new OrderRule("Name"), // Sort by default ASC
        ///     new OrderRule("Name", SortBy.DESC)
        /// };
        /// 
        /// Request request = new("TableName", selectedCol, orderRules);</code>
        /// </example>
        /// </summary>
        public Collection<OrderRule> OrderRules { get; init; }
    }
}
