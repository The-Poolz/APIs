namespace QuickSQL.QueryCreator.Helpers
{
    public static class ConditionsValidator
    {
        public static bool IsValidWhereCondition(Request request)
        {
            if (request.WhereConditions == null || request.WhereConditions.Count == 0)
                return true;

            foreach (var condition in request.WhereConditions)
            {
                if (!NotNullParamName(condition)
                    && !NotNullParamValue(condition)
                    && !IsValidOperator(condition))
                    return false;
            }
            return true;
        }

        public static bool NotNullParamName(Condition condition)
            => condition.ParamName != null && string.IsNullOrEmpty(condition.ParamName.Trim());
        public static bool NotNullParamValue(Condition condition)
            => condition.ParamValue != null && string.IsNullOrEmpty(condition.ParamValue.Trim());
        public static bool IsValidOperator(Condition condition)
        {
            foreach (var @operator in Operators.operators)
            {
                if (condition.Operator == @operator.Name)
                    return true;
            }
            return false;
        }
    }
}
