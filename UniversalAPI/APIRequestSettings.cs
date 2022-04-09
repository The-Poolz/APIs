using System.Diagnostics.CodeAnalysis;

namespace UniversalAPI
{
    /// <summary>
    /// API request settings.
    /// </summary>
    /// 
    public class APIRequestSettings
    {

        /// <summary>
        /// Pass a table(s) from which to take data.<br/>
        /// You can pass two table names to join tables on the <see cref="JoinCondition"/>.
        /// </summary>
        /// <remarks>
        /// Example: "FirstTableName" or "FirstTableName, SecondTableName"
        /// </remarks>
        [NotNull]
        public string SelectedTables { get; set; }

        /// <summary>
        /// Pass columns from which to take data.
        /// Default value is "*" which means select all data from table(s).
        /// </summary>
        /// <remarks>
        /// Example (If one table): "Id, Name, Address" or "FirstTableName.Id, FirstTableName.Name, ..."<br/>
        /// Example (If two tables): "FirstTableName.Id, FirstTableName.Name, SecondTableName.Address" 
        /// </remarks>
        [NotNull]
        public string SelectedColumns { get; set; } = "*";

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
