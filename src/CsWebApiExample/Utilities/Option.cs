using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsWebApiExample.Utilities
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
            return Option.Map(option,map);
        }

        /// <summary>
        /// Bind the "Some" case
        /// </summary>
        public static Option<U> Bind<T, U>(Option<T> option, Func<T, Option<U>> bind)
        {
            return Option.Match(
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
            return Option.Bind(option, bind);
        }

    }
}
