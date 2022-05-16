using Xunit;
using System.Collections.Generic;

using QuickSQL.QueryCreator.Helpers;

namespace QuickSQL.Tests.QueryCreators.Helpers
{
    public class ConditionsValidatorTests
    {
        [Fact]
        public static void IsValidWhereConditionDefault()
        {
            var conditions = new List<Condition>
            {
                new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" },
                new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "2" }
            };

            var result = ConditionsValidator.IsValidWhereCondition(conditions);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void IsValidWhereConditionWithoutCondition()
        {
            var conditions = new List<Condition>
            {
                new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" },
                new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "2" }
            };

            var result = ConditionsValidator.IsValidWhereCondition(conditions);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void NotNullParamNameDefault()
        {
            var condition = new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" };

            var result = ConditionsValidator.NotNullParamName(condition);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void NotNullParamNameEmptyParam()
        {
            var condition = new Condition { ParamName = "    ", Operator = OperatorNames.Equals, ParamValue = "1" };

            var result = ConditionsValidator.NotNullParamName(condition);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public static void NotNullParamValueDefault()
        {
            var condition = new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" };

            var result = ConditionsValidator.NotNullParamValue(condition);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void NotNullParamValueEmptyParam()
        {
            var condition = new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "    " };

            var result = ConditionsValidator.NotNullParamValue(condition);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public static void IsValidOperatorDefault()
        {
            var condition = new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" };

            var result = ConditionsValidator.IsValidOperator(condition);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void IsValidOperatorInvalidParam()
        {
            var condition = new Condition { ParamName = "Id", Operator = (OperatorNames)88, ParamValue = "1" };

            var result = ConditionsValidator.IsValidOperator(condition);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }
    }
}
