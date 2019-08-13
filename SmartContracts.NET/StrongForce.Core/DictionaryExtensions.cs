using System;
using System.Collections.Generic;
using System.Linq;

namespace StrongForce.Core
{
	public static class DictionaryExtensions
	{
		public static TV GetOrElse<TK, TV>(this IDictionary<TK, TV> dictionary, TK key, TV defaultValue = default(TV))
		{
			return dictionary.TryGetValue(key, out TV value) ? value : defaultValue;
		}

		public static Address AsAddress(this string value)
		{
			return value != null ? Address.FromBase64String(value) : null;
		}

		public static string AsString(this Address value)
		{
			return value?.ToBase64String();
		}

		public static T GetOrNull<T>(this IDictionary<string, object> dictionary, string key)
		{
			var value = dictionary.GetOrElse(key, null);

			if (value is T valueT)
			{
				return valueT;
			}

			return default(T);
		}

		public static IDictionary<TK, object> AddState<TK>(this IDictionary<TK, object> dictionary, TK key, ValueType value)
		{
			dictionary.Add(key, value);
			return dictionary;
		}

		public static IDictionary<TK, object> AddState<TK>(this IDictionary<TK, object> dictionary, TK key, string value)
		{
			dictionary.Add(key, value);
			return dictionary;
		}

		public static IDictionary<TK, object> AddState<TK>(this IDictionary<TK, object> dictionary, TK key, Address value)
		{
			return dictionary.AddState(key, value.ToBase64String());
		}
	}
}