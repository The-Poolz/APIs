using System.Collections.Generic;

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

        protected string CreateWhereCondition(List<Condition> conditions)
        {
            string whereCondition = string.Empty;
            if (conditions != null)
            {
                List<string> formatConditions = new List<string>();
                foreach (var cond in conditions)
                {
                    formatConditions.Add(cond.GetConditionString());
                }
                string condition = string.Join(" AND ", formatConditions);
                whereCondition = ($"WHERE {condition}");
            }
            return whereCondition;
        }

        protected abstract string OnCreateCommandQuery(Request request);
    }
}
