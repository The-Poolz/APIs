using Xunit;
using System.Collections.ObjectModel;

using QuickSQL.QueryCreator.Helpers;

namespace QuickSQL.Tests.QueryCreators.Helpers
{
    public static class ConditionsValidatorTests
    {
        [Fact]
        public static void IsValidWhereConditionDefault()
        {
            var conditions = new Collection<Condition>
            {
                new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" },
                new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "2" }
            };

            var result = ConditionsValidator.IsValidWhereCondition(conditions);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void IsValidWhereConditionWithoutCondition()
        {
            var conditions = new Collection<Condition>
            {
                new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" },
                new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "2" }
            };

            var result = ConditionsValidator.IsValidWhereCondition(conditions);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void IsValidWhereConditionInvalid()
        {
            var conditions = new Collection<Condition>
            {
                new Condition { ParamName = "   ", Operator = OperatorName.Equals, ParamValue = "1" },
                new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "2" }
            };

            var result = ConditionsValidator.IsValidWhereCondition(conditions);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public static void NotNullParamNameDefault()
        {
            var condition = new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" };

            var result = ConditionsValidator.NotNullParamName(condition);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void NotNullParamNameEmptyParam()
        {
            var condition = new Condition { ParamName = "    ", Operator = OperatorName.Equals, ParamValue = "1" };

            var result = ConditionsValidator.NotNullParamName(condition);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public static void NotNullParamValueDefault()
        {
            var condition = new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" };

            var result = ConditionsValidator.NotNullParamValue(condition);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void NotNullParamValueEmptyParam()
        {
            var condition = new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "    " };

            var result = ConditionsValidator.NotNullParamValue(condition);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public static void IsValidOperatorDefault()
        {
            var condition = new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" };

            var result = ConditionsValidator.IsValidOperator(condition);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void IsValidOperatorInvalidParam()
        {
            var condition = new Condition { ParamName = "Id", Operator = (OperatorName)88, ParamValue = "1" };

            var result = ConditionsValidator.IsValidOperator(condition);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }
    }
}
