using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FavoritesSurvey.Core
{
    public static class Error
    {
        private static ConcurrentDictionary<int, string> _errorMapping;

        static Error()
        {
            _errorMapping = new ConcurrentDictionary<int, string>();
        }

        public static void Register<T>(T errorCode, string message) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Type must be enum");

            int key = Convert.ToInt32(errorCode);
            if (!_errorMapping.ContainsKey(key))
                _errorMapping.TryAdd(key, message);
            else
                throw new ArgumentException("Error is already registered");
        }

        public static string For<T>(T errorCode) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Type must be enum");

            string message;
            int key = Convert.ToInt32(errorCode);

            if (_errorMapping.TryGetValue(key, out message))
                return message;
            else
                throw new ArgumentException("Error is not registered");
        }

        public static string For<T>(T errorCode, params string[] args) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Type must be enum");

            string message;
            int key = Convert.ToInt32(errorCode);

            var newArgs = args.Concat(new string[] { " ", " ", " ", " ", " ", }).ToArray();

            if (_errorMapping.TryGetValue(key, out message))
                return string.Format(message, newArgs);
            else
                throw new ArgumentException("Error is not registered");
        }
    }
}
