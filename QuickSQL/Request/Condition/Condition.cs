using System;

namespace QuickSQL
{
    public class Condition
    {
        public string ParamName { get; set; }
        public OperatorNames Operator { get; set; }
        public string ParamValue { get; set; }

        public string GetConditionString()
        {
            if (ValidOperators.Operators.ContainsKey(Operator))
            {
                ValidOperators.Operators.TryGetValue(Operator, out string operatorSymbol);
                return $"{ParamName} {operatorSymbol} {ParamValue}";
            }
            throw new ArgumentException("Invalid condition operatior", "Operator");
        }
    }
}
