using System;

namespace CSharpExamples.Utilities
{
    public class Option<T>
    {
        public Option(T value, bool hasValue)
        {
            this.Value = value;
            this.HasValue = hasValue;
        }

        public T Value { get; private set; }
        public bool HasValue { get; private set; }
    }

    public static class Option
    {
        /// <summary>
        /// Construct "Some" case
        /// </summary>
        public static Option<T> Some<T>(T value)
        {
            return new Option<T>(value, true);
        }

        /// <summary>
        /// Construct "None" case
        /// </summary>
        public static Option<T> None<T>()
        {
            return new Option<T>(default(T), false);
        }

        /// <summary>
        /// Pattern match by providing a function for each case
        /// </summary>
        public static TResult Match<T, TResult>(Option<T> option, Func<T, TResult> someCase, Func<TResult> noneCase)
        {
            if (option.HasValue)
            {
                return someCase(option.Value);
            }
            else
            {
                return noneCase();
            }
        }

        /// <summary>
        /// Map the "Some" case
        /// </summary>
        public static Option<U> Map<T, U>(Option<T> option, Func<T, U> map)
        {
            return Option.Match(
                option,
                v => Option.Some(map(v)),
                () => Option.None<U>()
                );
        }

        /// <summary>
        /// Select is a synonym for Map 
        /// </summary>
        public static Option<U> Select<T, U>(Option<T> option, Func<T, U> map)
        {
            return Option.Map(option, map);
        }

        /// <summary>
        /// Convert a T->Option function to a Option->Option function
        /// </summary>
        public static Func<Option<T>, Option<U>> Bind<T, U>(Func<T, Option<U>> bind)
        {
            return option => Option.Match(
                option,
                v => bind(v),
                () => Option.None<U>()
                );
        }

        /// <summary>
        /// SelectMany is a synonym for Bind 
        /// </summary>
        public static Option<U> SelectMany<T, U>(Option<T> option, Func<T, Option<U>> bind)
        {
            return Option.Bind(bind)(option);
        }

        /// <summary>
        /// Apply a wrapped function to a wrapped value
        /// </summary>
        public static Func<Option<Func<T1, T2>>, Option<T1>, Option<T2>> Apply<T1, T2>()
        {
            return (func, arg) =>
            {
                if (func.HasValue && arg.HasValue)
                {
                    return Some(func.Value(arg.Value));
                }
                return None<T2>();
            };
        }


        /// <summary>
        /// Lift a two argument function to a wrapped version
        /// </summary>
        public static Func<Option<T1>, Option<T2>, Option<T3>> Lift2<T1, T2, T3>(Func<T1, T2, T3> func)
        {
            var lifted = Some(func.Curry());
            return (arg1, arg2) =>
            {
                var r1 = Apply<T1, Func<T2, T3>>()(lifted, arg1);
                var r2 = Apply<T2, T3>()(r1, arg2);
                return r2;
            };
        }

        /// <summary>
        /// Lift a three argument function to a wrapped version
        /// </summary>
        public static Func<Option<T1>, Option<T2>, Option<T3>, Option<T4>> Lift3<T1, T2, T3, T4>(Func<T1, T2, T3, T4> func)
        {
            var lifted = Some(func.Curry());
            return (arg1, arg2, arg3) =>
            {
                var r1 = Apply<T1, Func<T2, Func<T3, T4>>>()(lifted, arg1);
                var r2 = Apply<T2, Func<T3, T4>>()(r1, arg2);
                var r3 = Apply<T3, T4>()(r2, arg3);
                return r3;
            };
        }

    }
}
