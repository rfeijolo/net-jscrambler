using System.Collections.Generic;

namespace JScrambler.Client
{
    internal static class DictionaryExtensions
    {
        public static void Replace<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
            }

            dictionary.Add(key, value);
        }

        public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> first, IDictionary<TKey, TValue> second)
        {
            if (second == null)
            {
                return;
            }

            if (first == null)
            {
                first = new Dictionary<TKey, TValue>();
            }

            foreach (var item in second)
            {
                if (!first.ContainsKey(item.Key))
                {
                    first.Add(item.Key, item.Value);
                }
            }
        }
    }
}