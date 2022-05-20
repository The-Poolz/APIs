using System.Collections.ObjectModel;

namespace QuickSQL.QueryCreator.Helpers
{
    public static class ConditionsValidator
    {
        public static bool IsValidWhereCondition(Collection<Condition> conditions)
        {
            if (conditions == null || conditions.Count == 0)
                return true;

            foreach (var condition in conditions)
            {
                if (!NotNullParamName(condition)
                    || !NotNullParamValue(condition)
                    || !IsValidOperator(condition))
                    return false;
            }
            return true;
        }

        public static bool NotNullParamName(Condition condition)
            => condition.ParamName != null && !string.IsNullOrEmpty(condition.ParamName.Trim());
        public static bool NotNullParamValue(Condition condition)
            => condition.ParamValue != null && !string.IsNullOrEmpty(condition.ParamValue.Trim());
        public static bool IsValidOperator(Condition condition)
        {
            foreach (var @operator in ValidOperators.Operators)
            {
                if (condition.Operator == @operator.Key)
                    return true;
            }
            return false;
        }
    }
}
