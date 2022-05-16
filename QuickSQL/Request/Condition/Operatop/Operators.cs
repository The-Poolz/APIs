using System.Collections.Generic;

namespace QuickSQL
{
    public static class Operators
    {
        public static readonly List<Operator> operators = new List<Operator>
        {
            new Operator(OperatorNames.Equals, "="),
            new Operator(OperatorNames.NotEquals, "!="),
            new Operator(OperatorNames.Less, "<"),
            new Operator(OperatorNames.LessOrEqual, "<="),
            new Operator(OperatorNames.More, ">"),
            new Operator(OperatorNames.MoreOrEqual, ">=")
        };
    }
}
