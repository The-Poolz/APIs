using Moq;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace QuickSQL.Tests.Mock
{
    public static class MockDataReader
    {
        /// <summary>
        /// Mock a IDataReader using a list
        /// </summary>
        /// <typeparam name="T">Model representing table schema</typeparam>
        /// <param name="mock"></param>
        /// <param name="data">List respresenting a result set</param>
        /// <param name="fieldNames">Optional. Column names. Default is <see cref="T"/>'s properties names</param>
        public static void SetupDataReader<T>(this Mock<IDataReader> mock, IReadOnlyList<T> data, string[] fieldNames = null)
        {
            var dataInfo = new DataInfo<T>(data, fieldNames);
            SetupDataReader(mock, dataInfo);
        }

        private static void SetupDataReader<T>(this Mock<IDataReader> mock, DataInfo<T> dataInfo, Action<int> onRead = null)
        {
            int row = -1;

            mock.Setup(r => r.Read())
              .Returns(() => row < dataInfo.Data.Count - 1)
              .Callback(() =>
              {
                  row++;
                  onRead?.Invoke(row);
              });

            mock.Setup(r => r.GetValue(It.IsAny<int>())).Returns<int>(col => dataInfo.GetValue<object>(row, col));
            Expression<Func<int, bool>> outOfRange = i => i < 0 || i >= dataInfo.FieldCount;
            mock.Setup(r => r.GetValue(It.Is(outOfRange))).Throws<IndexOutOfRangeException>();
        }
    }
}