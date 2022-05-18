using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using QuickSQL.QueryCreator.Helpers;

namespace QuickSQL.QueryCreator
{
    public abstract class BaseQueryCreator
    {
        public string CreateCommandQuery(Request request)
        {
            if (!RequestValidator.IsValidRequest(request))
                return null;

            return OnCreateCommandQuery(request);
        }

        protected static string CreateWhereCondition(Collection<Condition> conditions)
        {
            string whereCondition = string.Empty;
            if (conditions != null)
            {
                List<string> formatConditions = new List<string>();
                foreach (var cond in conditions)
                {
                    ValidOperators.Operators.TryGetValue(cond.Operator, out string operatorSymbol);
                    var conditionString = $"{cond.ParamName} {operatorSymbol} {cond.ParamValue}";

                    formatConditions.Add(conditionString);
                }
                string condition = string.Join(" AND ", formatConditions);
                whereCondition = ($"WHERE {condition}");
            }
            return whereCondition;
        }

        protected abstract string OnCreateCommandQuery(Request request);
    }
}
