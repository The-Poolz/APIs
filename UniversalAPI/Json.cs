using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace UniversalApi
{
    public class Json<TKey, TValue> : IEnumerable
    {
        private ConcurrentDictionary<TKey, List<TValue>> _keyValue = new ConcurrentDictionary<TKey, List<TValue>>();
        private ConcurrentDictionary<TValue, List<TKey>> _valueKey = new ConcurrentDictionary<TValue, List<TKey>>();
        public Json() { }
        public Json(TKey key, TValue value)
        {
            Add(key, value);
        }

        public ICollection<TKey> Keys
        {
            get { return _keyValue.Keys; }
        }
        public ICollection<TValue> Values
        {
            get { return _valueKey.Keys; }
        }
        public int Count
        {
            get { return _keyValue.Count; }
        }
        public bool IsReadOnly
        {
            get { return false; }
        }

        public List<TValue> this[TKey index]
        {
            get { return _keyValue[index]; }
            set { _keyValue[index] = value; }
        }
        public List<TKey> this[TValue index]
        {
            get { return _valueKey[index]; }
            set { _valueKey[index] = value; }
        }
        public void Add(TKey key, TValue value)
        {
            lock (this)
            {
                if (!_keyValue.TryGetValue(key, out List<TValue> result))
                    _keyValue.TryAdd(key, new List<TValue>() { value });
                else if (!result.Contains(value))
                    result.Add(value);

                if (!_valueKey.TryGetValue(value, out List<TKey> resulTValue))
                    _valueKey.TryAdd(value, new List<TKey>() { key });
                else if (!resulTValue.Contains(key))
                    resulTValue.Add(key);
            }
        }
        public bool TryGetValues(TKey key, out List<TValue> value)
        {
            return _keyValue.TryGetValue(key, out value);
        }
        public bool TryGetKeys(TValue value, out List<TKey> key)
        {
            return _valueKey.TryGetValue(value, out key);
        }
        public bool ContainsKey(TKey key)
        {
            return _keyValue.ContainsKey(key);
        }
        public bool ContainsValue(TValue value)
        {
            return _valueKey.ContainsKey(value);
        }
        public void Remove(TKey key)
        {
            lock (this)
            {
                if (_keyValue.TryRemove(key, out List<TValue> values))
                {
                    foreach (var item in values)
                    {
                        var remove = _valueKey.TryRemove(item, out List<TKey> keys);
                    }
                }
            }
        }
        public void Remove(TValue value)
        {
            lock (this)
            {
                if (_valueKey.TryRemove(value, out List<TKey> keys))
                {
                    foreach (var item in keys)
                    {
                        var remove = _keyValue.TryRemove(item, out List<TValue> values);
                    }
                }
            }
        }
        public void Clear()
        {
            _keyValue.Clear();
            _valueKey.Clear();
        }

        public string GetJsonString()
        {
            string jsonString = _valueKey.Count == 1 ? "" : "[";

            foreach (var key in _keyValue.Keys)
            {
                if (key.ToString() == "TableName")
                {
                    Remove(key);
                }
                else
                {
                    var values = _keyValue.GetValueOrDefault(key);
                    if (values.Count == 1)
                    {
                        jsonString += "{" + $"\"{key}\":\"{values.ToArray()[0]}\"" + "},";
                    }
                    else
                    {
                        foreach (var value in values)
                        {
                            jsonString += "{" + $"\"{key}\":\"{value}\"" + "},";
                        }
                    }
                }
            }
            jsonString = jsonString.Remove(jsonString.LastIndexOf(','), 1);
            jsonString += _valueKey.Count == 1 ? "" : "]";

            return jsonString;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return _keyValue.GetEnumerator();
        }
    }
}
