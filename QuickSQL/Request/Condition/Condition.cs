using System.Linq;

namespace QuickSQL
{
    public class Condition
    {
        public string ParamName { get; set; }
        public OperatorNames Operator { get; set; }
        public string ParamValue { get; set; }

        public string GetConditionString()
        {
            string operatorSymbol = Operators.operators.FirstOrDefault(op => op.Name == Operator).Value;
            return $"{ParamName} {operatorSymbol} {ParamValue}";
        }
    }
}
