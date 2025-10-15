using System;
using System.Collections.Generic;
using System.Linq;

namespace ClearMapperLibrary
{
    public partial class ClearMapper
    {

        public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source)
            where TSource : class
            where TDestination : class
        {

            var config = findConfig<TSource, TDestination>();
            var destination = source.Select(config);

            return destination;

        }

        public IQueryable<TDestination> Map<TSource, TDestination>(IQueryable<TSource> source)
             where TSource : class
             where TDestination : class
        {

            var config = findConfig<TSource, TDestination>();
            var destination = source.Select(x => config(x));

            return destination;

        }

        public static Action<TR> IgnoreResult<TR>(Delegate f)
        {
            return x => f.DynamicInvoke(x);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
            where TSource : class
            where TDestination : class, new()
        {

            var config = findConfig<TSource, TDestination>();
            var destination = new TDestination();

            config(source, destination);

            return destination;

        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {

            var config = findConfig<TSource, TDestination>();

            var action = ddd.IgnoreResult(config);

            action(source);

            return destination;

        }

    }

    public class ddd
    {
        public static Action<T1> IgnoreResult<T1, T2>(Func<T1, T2> func)
        {
            return x => func(x);
        }
    }
}


