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
            _tableName = tableName;
            _selectedColumns = selectedColumns;
        }
        public Request(string tableName, string selectedColumns, Collection<Condition> whereConditions)
        {
            _tableName = tableName;
            _selectedColumns = selectedColumns;
            _whereConditions = whereConditions;
        }

        /// <summary>
        /// Pass a table from which to take data.<br/>
        /// This is a required parameter.
        /// </summary>
        /// <remarks>
        /// Example: "TableName"
        /// </remarks>
        private readonly string _tableName;

        /// <summary>
        /// Pass columns from which to take data.<br/>
        /// This is a required parameter.
        /// </summary>
        /// <remarks>
        /// Example: "Id, Name, Address"
        /// </remarks>
        private readonly string _selectedColumns;

        /// <summary>
        /// Enter condition for search tables.<br/>
        /// Remarks: String parameter must be in quotes(it is default SQL logic)
        /// </summary>
        /// <remarks>
        /// Example: new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
        /// </remarks>
        private readonly Collection<Condition> _whereConditions;

        public string TableName => _tableName;
        public string SelectedColumns => _selectedColumns;
        public Collection<Condition> WhereConditions => _whereConditions;
    }
}
