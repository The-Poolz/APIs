using System.Collections.Generic;

namespace QuickSQL
{
    public static class ValidOperators
    {
        public static readonly Dictionary<OperatorNames, string> Operators = new Dictionary<OperatorNames, string>
        {
            { OperatorNames.Equals, "=" },
            { OperatorNames.NotEquals, "!=" },
            { OperatorNames.Less, "<" },
            { OperatorNames.LessOrEqual, "<=" },
            { OperatorNames.More, ">" },
            { OperatorNames.MoreOrEqual, ">=" }
        };
    }
}
