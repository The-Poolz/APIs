using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using QuickSQL.QueryCreator.Helpers;

namespace QuickSQL.QueryCreator
{
    /// <summary>
    /// An <see langword="abstract"/> class that abstracts away the creation of a SQL string.<br/>
    /// For your QueryCreator to work, you need to define the <see cref="OnCreateCommandQuery"/> method.
    /// </summary>
    public abstract class BaseQueryCreator
    {
        /// <summary>
        /// Validates the <paramref name="request"/> object.<br/>
        /// Calls the <see cref="CreateWhereCondition"/> method.
        /// </summary>
        /// <param name="request">Pass <see cref="Request"/> object to create a query string.</param>
        /// <returns>
        /// Return SQL command query string.<br/>
        /// The result is the result of <see cref="OnCreateCommandQuery"/>.<br/>
        /// Return <see langword="null"/> if <see cref="RequestValidator.IsValidRequest"/> return <see langword="false"/> .
        /// </returns>
        public string CreateCommandQuery(Request request)
        {
            if (!RequestValidator.IsValidRequest(request))
                return null;

            return OnCreateCommandQuery(request);
        }

        /// <summary>
        /// Creates the part of the query string responsible for the WHERE сondition.<br/>
        /// This is a helper function, you can write your own logic.
        /// </summary>
        /// <param name="conditions">Pass condition(s) collection.</param>
        /// <returns>
        /// Return part of the query string responsible for the WHERE сondition.
        /// </returns>
        protected static string CreateWhereCondition(Collection<Condition> conditions)
        {
            if (orderRules == null || orderRules.Count == 0)
                return string.Empty;

            List<string> formatConditions = new List<string>();
            foreach (var cond in conditions)
                {
                    ValidOperators.Operators.TryGetValue(cond.Operator, out string operatorSymbol);
                    var conditionString = $"{cond.ParamName} {operatorSymbol} {cond.ParamValue}";

                    formatConditions.Add(conditionString);
                }
                string condition = string.Join(" AND ", formatConditions);
                return $"WHERE {condition}";
        }

        protected static string CreateOrderByRules(Collection<OrderRule> orderRules)
        {
            if (orderRules == null || orderRules.Count == 0)
                return string.Empty;

            List<string> formatRules = new List<string>();
            foreach (var rule in orderRules.ToList())
                {
                    string ruleString = $"{rule.ColumnName} {rule.Sort}";
                    formatRules.Add(ruleString);
                }
            string rulesString = string.Join(", ", formatRules);
            return $"ORDER BY {rulesString}";
        }

        protected abstract string OnCreateCommandQuery(Request request);
    }
}
