using Moq;
using System;
using System.Linq;
using System.Data;
using System.Dynamic;
using System.Text.Json;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QuickSQL.Tests.DataAccess
{
    public class TestMock
    {
        public static IDataReader MockIDataReader<T>(T[] objectsToEmulate, Request request) where T : class
        {
            var moq = new Mock<IDataReader>();
            // This var stores current position in 'objectsToEmulate' list
            int currentItem = -1;

            moq.Setup(r => r.Read())
                .Returns(() => currentItem < objectsToEmulate.Length)
                .Callback(() =>
                {
                    currentItem++;

                    //onRead?.Invoke(row);
                });

            moq.Setup(x => x.Read())
                // Return 'True' while list still has an item
                .Returns(() => currentItem < objectsToEmulate.Length - 1)
                // Go to next position
                .Callback(() => currentItem++);

            moq.Setup(x => x.GetValue(0)).Returns(
                ReturnReadData(objectsToEmulate[currentItem+1], request) == null ? 
                "" : JsonSerializer.Serialize(ReturnReadData(objectsToEmulate[currentItem+1], request)));

            return moq.Object;
        }

        public static bool HasReadData<T>(T emulateObject) where T : class
            => emulateObject != null;
        public static bool HasCondition(Collection<Condition> conditions)
            => conditions != null && conditions.Count > 0;
        public static bool HasOrderRules(Collection<OrderRule> orderRules)
            => orderRules != null && orderRules.Count > 0;

        public static ExpandoObject ReturnReadData<T>(T emulateObject, Request request) where T : class
        {
            if (!HasReadData(emulateObject))
                return null;

            if (request.SelectedColumns == null && request.SelectedColumns.Count > 0)
                return null;
            
            Type emulateType = emulateObject.GetType();
            PropertyInfo[] props = emulateType.GetProperties();
            ExpandoObject expando = CreateExpando(props, emulateObject);

            // Emulate SelectedColumns
            expando = EmulateSelectedColumns(props, expando, request.SelectedColumns);

            // Emulate WhereConditions
            PropertyInfo[] expandoProps = expando.GetType().GetProperties();
            if (HasCondition(request.WhereConditions))
            {
                foreach (var cond in request.WhereConditions)
                {
                    // Получить нужное свойство
                    var prop = props.Where(x => x.Name == cond.ParamName).First();
                    var propValue = prop.GetValue(emulateObject);
                    if (propValue.ToString() == "2")
                    {
                        Console.WriteLine("Done");
                    }

                    // Проверить значение
                    switch (cond.Operator)
                    {
                        case OperatorName.Equals:
                            return propValue.ToString() == cond.ParamValue ? expando : null;
                        case OperatorName.NotEquals:
                            return propValue.ToString() != cond.ParamValue ? expando : null;
                        default:
                            break;
                    }
                }
            }
            return null;
        }

        public static ExpandoObject EmulateSelectedColumns(PropertyInfo[] props, ExpandoObject expando, Collection<string> selectedColumns)
        {
            foreach (var col in selectedColumns)
            {
                var property = props.Where(x => x.Name == col);
                if (property == null || property.Count() <= 0)
                {
                    expando.Remove(property.First().Name, out object obj);
                }
            }
            return expando;
        }

        public static ExpandoObject CreateExpando<T>(PropertyInfo[] props, T emulateObject) where T : class
        {
            ExpandoObject expando = new();
            foreach (var property in props)
            {
                AddProperty(expando, property.Name, property.GetValue(emulateObject));
            }
            return expando;
        }

        private static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
    }
}
