using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpExamples.Utilities
{
    public static class Fun
    {

        /// <summary>
        /// Curry a two parameter function
        /// </summary>
        public static Func<T1, Func<T2, T3>> Curry<T1, T2, T3>(Func<T1, T2, T3> func)
        {
            return arg1 => arg2 => func(arg1, arg2);
        }

        /// <summary>
        /// Curry a three parameter function
        /// </summary>
        public static Func<T1, Func<T2, Func<T3, T4>>> Curry<T1, T2, T3, T4>(Func<T1, T2, T3, T4> func)
        {
            return arg1 => arg2 => (arg3 => func(arg1, arg2, arg3));
        }

        /// <summary>
        /// Uncurry a two parameter function
        /// </summary>
        public static Func<T1, T2, T3> Uncurry<T1, T2, T3>(Func<T1, Func<T2, T3>> func)
        {
            return (arg1, arg2) => func(arg1)(arg2);
        }

        /// <summary>
        /// Uncurry a three parameter function
        /// </summary>
        public static Func<T1, T2, T3, T4> Uncurry<T1, T2, T3, T4>(Func<T1, Func<T2, Func<T3, T4>>> func)
        {
            return (arg1, arg2, arg3) => func(arg1)(arg2)(arg3);
        }


        /// <summary>
        /// Partially apply the first arg to a two parameter function
        /// </summary>
        public static Func<T2, T3> Apply<T1, T2, T3>(Func<T1, T2, T3> func, T1 arg1)
        {
            return Curry(func)(arg1);
        }


        /// <summary>
        /// Partially apply the first arg to a three parameter function
        /// </summary>
        public static Func<T2, Func<T3, T4>> Apply<T1, T2, T3, T4>(Func<T1, T2, T3, T4> func, T1 arg1)
        {
            return Curry(func)(arg1);
        }


        /// <summary>
        /// Compose
        /// </summary>
        public static Func<T1, T3> Compose<T1, T2, T3>(Func<T1, T2> func1, Func<T2, T3> func2)
        {
            return source => func2(func1(source));
        }


        /// <summary>
        /// Fold the items in a list
        /// </summary>
        public static T2 Fold<T1, T2>(Func<T2, T1, T2> action, T2 initialValue, List<T1> input)
        {
            return input.Aggregate(initialValue, action);
        }

        /// <summary>
        /// Map the items in a list
        /// </summary>
        public static Func<List<T1>, List<T2>> Map<T1, T2>(Func<T1, T2> func1)
        {
            return input => input.Select(func1).ToList();
        }

        /// <summary>
        /// Filter the items in a list
        /// </summary>
        public static Func<List<T1>, List<T1>> Filter<T1>(Func<T1, bool> func1)
        {
            return input => input.Where(func1).ToList();
        }

    }
}
