using System;

namespace CSharpExamples.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// Curry a two parameter function
        /// </summary>
        public static Func<T1, Func<T2, T3>> Curry<T1, T2, T3>(this Func<T1, T2, T3> func)
        {
            return Fun.Curry(func);
        }

        /// <summary>
        /// Curry a three parameter function
        /// </summary>
        public static Func<T1, Func<T2, Func<T3, T4>>> Curry<T1, T2, T3, T4>(this Func<T1, T2, T3, T4> func)
        {
            return Fun.Curry(func);
        }

        /// <summary>
        /// Uncurry a two parameter function
        /// </summary>
        public static Func<T1, T2, T3> Uncurry<T1, T2, T3>(this Func<T1, Func<T2, T3>> func)
        {
            return Fun.Uncurry(func);
        }

        /// <summary>
        /// Uncurry a three parameter function
        /// </summary>
        public static Func<T1, T2, T3, T4> Uncurry<T1, T2, T3, T4>(this Func<T1, Func<T2, Func<T3, T4>>> func)
        {
            return Fun.Uncurry(func);
        }

        /// <summary>
        /// Partially apply the first arg to a two parameter function
        /// </summary>
        public static Func<T2, T3> Apply<T1, T2, T3>(this Func<T1, T2, T3> func, T1 arg1)
        {
            return Fun.Apply(func, arg1);
        }

        /// <summary>
        /// Partially apply the first arg to a three parameter function
        /// </summary>
        public static Func<T2, Func<T3, T4>> Apply<T1, T2, T3, T4>(this Func<T1, T2, T3, T4> func, T1 arg1)
        {
            return Fun.Apply(func, arg1);
        }

        /// <summary>
        /// Compose
        /// </summary>
        public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T1, T2> func1, Func<T2, T3> func2)
        {
            return Fun.Compose(func1, func2);
        }

        /// <summary>
        /// Turn an action into a function
        /// </summary>
        public static Func<T1, Unit> ToFunc<T1>(this Action<T1> action)
        {
            return arg =>
            {
                action(arg);
                return Unit.Instance;
            };
        }

        /// <summary>
        /// Pipe an argument through a function
        /// </summary>
        public static T2 Pipe<T1, T2>(this T1 arg1, Func<T1, T2> func)
        {
            return func(arg1);
        }

    }
}
