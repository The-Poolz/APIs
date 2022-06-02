namespace QuickSQL
{
    /// <summary>
    /// The object on the basis of which the SQL command condition is created.<br/>
    /// <example>Example:
    /// <code>
    /// Condition condition = new Condition {
    ///     ParamName = "Name",
    ///     Operator = OperatorName.Equals,
    ///     ParamValue = "'Alex'"
    /// };</code>
    /// </example>
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// Enter a parameter name for the condition.<br/>
        /// <example>Example:
        /// <code>
        /// Condition condition = new Condition {
        ///     ParamName = "Name"
        /// };</code>
        /// </example>
        /// </summary>
        public string ParamName { get; set; }
        /// <summary>
        /// Enter a <see cref="OperatorName"/> for the condition.<br/>
        /// <example>Example:
        /// <code>
        /// Condition condition = new Condition {
        ///     Operator = OperatorName.NotEquals
        /// };</code>
        /// </example>
        /// </summary>
        public OperatorName Operator { get; set; }
        /// <summary>
        /// Enter a parameter value for the condition.<br/>
        /// String parametr must be in single quotes.
        /// <example>Example:
        /// <code>
        /// Condition condition = new Condition {
        ///     ParamName = "City",
        ///     Operator = OperatorName.Equals,
        ///     ParamValue = "'Compton'"
        /// };</code>
        /// </example>
        /// </summary>
        public string ParamValue { get; set; }
    }
}
