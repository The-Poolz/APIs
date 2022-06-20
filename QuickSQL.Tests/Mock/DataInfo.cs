using System;
using System.Text.Json;
using System.Reflection;
using System.Collections.Generic;

namespace QuickSQL.Tests.Mock
{
    internal class DataInfo<T>
    {
        public IReadOnlyList<T> Data { get; }
        public int FieldCount => _properties.Length;
        public bool Closed { get; set; }

        private readonly PropertyInfo[] _properties;
        private readonly string[] _fieldNames;

        public DataInfo(IReadOnlyList<T> data, string[] fieldNames)
        {
            Data = data;
            Closed = false;
            _properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            _fieldNames = fieldNames;

            if (_fieldNames != null && _fieldNames.Length != _properties.Length)
                throw new ArgumentException($"{nameof(fieldNames)}.Length != number of properties");
        }

        public string GetValue<U>(int r, int c)
        {
            ThrowIfOutOfRange(c);
            return JsonSerializer.Serialize(Data[r]);
        }

        private void ThrowIfOutOfRange(int c)
        {
            if (Closed)
                throw new InvalidOperationException();

            if (c < 0 || c >= _properties.Length)
                throw new IndexOutOfRangeException();
        }
    }
}