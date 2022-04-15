using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace QuickSQL
{
    /// <summary>
    /// Set up an object for work <see cref="QuickSql.InvokeRequest(Request, DbContext)"/>
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Pass a table(s) from which to take data.<br/>
        /// You can pass two table names to join tables on the <see cref="JoinCondition"/>.<br/>
        /// This is a required parameter.
        /// </summary>
        /// <remarks>
        /// Example: "FirstTableName" or "FirstTableName, SecondTableName"
        /// </remarks>
        [NotNull]
        public string SelectedTables { get; set; }

        /// <summary>
        /// Pass columns from which to take data.<br/>
        /// Default value is "*" which means select all data from table(s).<br/>
        /// This is a required parameter.
        /// </summary>
        /// <remarks>
        /// Example (If one table): "Id, Name, Address" or "FirstTableName.Id, FirstTableName.Name, ..."<br/>
        /// Example (If two tables): "FirstTableName.Id, FirstTableName.Name, SecondTableName.Address" 
        /// </remarks>
        [NotNull]
        public string SelectedColumns { get; set; } = "*";

        /// <summary>
        /// Enter condition for search tables.
        /// </summary>
        /// <remarks>
        /// Example (condition): "FirstTableName.Id = SecondTableName.Rank"<br/>
        /// Example (conditions): "FirstTableName.Id = SecondTableName.Rank, FirstTableName.Name = SecondTableName.UserName" 
        /// </remarks>
        public string WhereCondition { get; set; }

        /// <summary>
        /// Enter condition for joining tables.
        /// </summary>
        /// <remarks>
        /// Example (condition): "FirstTableName.Id = SecondTableName.Rank"<br/>
        /// Example (conditions): "FirstTableName.Id = SecondTableName.Rank, FirstTableName.Name = SecondTableName.UserName" 
        /// </remarks>
        public string JoinCondition { get; set; }
    }
}
