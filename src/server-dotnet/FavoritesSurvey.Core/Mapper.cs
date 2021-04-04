using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace FavoritesSurvey.Core
{
    public static class Mapper
    {
        private static ConcurrentDictionary<Tuple<Type, Type>, Delegate> _mapperMappingDelegate;
        private static ConcurrentDictionary<Tuple<Type, Type>, IMapper> _mapperMappingImplementation;
        static Mapper()
        {
            _mapperMappingDelegate = new ConcurrentDictionary<Tuple<Type, Type>, Delegate>();
            _mapperMappingImplementation = new ConcurrentDictionary<Tuple<Type, Type>, IMapper>();
        }

        public static void Register<InitialType, ConvertedType>(Func<InitialType, ConvertedType> mapperDelegate)
        {
            Type initial = typeof(InitialType);
            Type converted = typeof(ConvertedType);
            var key = new Tuple<Type, Type>(initial, converted);
            if (!_mapperMappingDelegate.ContainsKey(key))
                _mapperMappingDelegate.TryAdd(key, mapperDelegate);
            else
                throw new ArgumentException("Type is already registered.");
        }

        public static void Register<InitialType, ConvertedType>(IMapper<InitialType, ConvertedType> mapperImplementation)
        {
            Type initial = typeof(InitialType);
            Type converted = typeof(ConvertedType);
            var key = new Tuple<Type, Type>(initial, converted);
            if (!_mapperMappingImplementation.ContainsKey(key))
                _mapperMappingImplementation.TryAdd(key, mapperImplementation);
            else
                throw new ArgumentException("Type is already registered.");
        }

        public static IEnumerable<ConvertedType> Map<InitialType, ConvertedType>(IEnumerable<InitialType> value)
        {
            if (value == null)
                return null;

            List<ConvertedType> result = new List<ConvertedType>();
            foreach (var item in value)
                result.Add(Map<InitialType, ConvertedType>(item));
            return result;
        }

        public static ConvertedType Map<InitialType, ConvertedType>(InitialType value)
        {
            if (EqualityComparer<InitialType>.Default.Equals(value, default(InitialType)))
                return default(ConvertedType);

            Type initial = typeof(InitialType);
            Type converted = typeof(ConvertedType);
            var key = new Tuple<Type, Type>(initial, converted);

            Delegate mapperDelegate = null;
            IMapper mapperImplementation = null;

            if (EqualityComparer<InitialType>.Default.Equals(value, default(InitialType)))
                return default(ConvertedType);

            if (_mapperMappingDelegate.TryGetValue(key, out mapperDelegate))
                return (mapperDelegate as Func<InitialType, ConvertedType>).Invoke(value);
            else if (_mapperMappingImplementation.TryGetValue(key, out mapperImplementation))
                return (mapperImplementation as IMapper<InitialType, ConvertedType>).Map(value);
            else
                throw new ArgumentException("Type is not registered.");
        }
    }

    public interface IMapper<InitialType, ConvertedType> : IMapper
    {
        ConvertedType Map(InitialType target);
    }

    public interface IMapper
    {

    }
}
