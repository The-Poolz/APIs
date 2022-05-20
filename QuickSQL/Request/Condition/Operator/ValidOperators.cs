using System.Collections.Generic;

namespace QuickSQL
{
    public static class ValidOperators
    {
        public static readonly Dictionary<OperatorName, string> Operators = new Dictionary<OperatorName, string>
        {
            { OperatorName.Equals, "=" },
            { OperatorName.NotEquals, "!=" },
            { OperatorName.Less, "<" },
            { OperatorName.LessOrEqual, "<=" },
            { OperatorName.More, ">" },
            { OperatorName.MoreOrEqual, ">=" }
        };
    }
}
