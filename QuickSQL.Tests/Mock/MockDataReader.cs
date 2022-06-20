using Moq;
using System.Data;
using System.Text.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QuickSQL.Tests.Mock
{
    public class MockDataReader<T> where T : class
    {
        public MockDataReader(Collection<T> data) 
        {
            Data = data;
        }

        private IReadOnlyList<T> Data { get; }

        private string GetJsonValue(int row)
        {
            return JsonSerializer.Serialize(Data[row]);
        }
        public void SetupDataReader(Mock<IDataReader> mock)
        {
            int row = -1;

            mock.Setup(r => r.Read())
              .Returns(() => row < Data.Count - 1)
              .Callback(() =>
              {
                  row++;
              });

            mock.Setup(r => r.GetValue(It.IsAny<int>())).Returns<int>(col => GetJsonValue(row));
        }
    }
}