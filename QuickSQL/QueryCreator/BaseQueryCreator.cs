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
                    formatConditions.Add(GetConditionString(cond));
                }
                string condition = string.Join(" AND ", formatConditions);
                whereCondition = ($"WHERE {condition}");
            }
            return whereCondition;
        }
        protected static string GetConditionString(Condition condition)
        {
            if (ValidOperators.Operators.ContainsKey(condition.Operator))
            {
                ValidOperators.Operators.TryGetValue(condition.Operator, out string operatorSymbol);
                return $"{condition.ParamName} {operatorSymbol} {condition.ParamValue}";
            }
            throw new ArgumentException("Invalid condition operatior", "Operator");
        }

        protected abstract string OnCreateCommandQuery(Request request);
    }
}
