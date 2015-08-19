using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsWebApiExample.Utilities
{
    public class Either<TLeft,TRight>
    {
        public Either(TLeft leftValue, TRight rightValue, bool hasLeftValue)
        {
            this.LeftValue = leftValue;
            this.RightValue = rightValue;
            this.HasLeftValue = hasLeftValue;
        }

        public TLeft LeftValue { get; private set; }
        public TRight RightValue { get; private set; }
        public bool HasLeftValue { get; private set; }
    }

    public static class Either
    {

        /// <summary>
        /// Construct "Left" case
        /// </summary>
        public static Either<TLeft, TRight> Left<TLeft, TRight>(TLeft value)
        {
            return new Either<TLeft, TRight>(value, default(TRight), true);
        }

        /// <summary>
        /// Construct "Right" case
        /// </summary>
        public static Either<TLeft, TRight> Right<TLeft, TRight>(TRight value)
        {
            return new Either<TLeft, TRight>(default(TLeft), value, false);
        }

        /// <summary>
        /// Pattern match by providing a function for each case
        /// </summary>
        public static TResult Match<TLeft, TRight, TResult>(Either<TLeft,TRight> choice, Func<TLeft, TResult> leftCase, Func<TRight,TResult> rightCase)
        {
            if (choice.HasLeftValue)
            {
                return leftCase(choice.LeftValue);
            }
            else
            {
                return rightCase(choice.RightValue);
            }
        }

        /// <summary>
        /// Map the "Right" case
        /// </summary>
        public static Either<TLeft, TResult> Map<TLeft, TRight, TResult>(Either<TLeft, TRight> choice, Func<TRight, TResult> map)
        {
            return Either.Match(
                choice,
                left => Either.Left<TLeft, TResult>(left),
                right => Either.Right<TLeft, TResult>(map(right))
                );
        }

        /// <summary>
        /// Select is a synonym for Map 
        /// </summary>
        public static Either<TLeft, TResult> Select<TLeft, TRight, TResult>(Either<TLeft, TRight> choice, Func<TRight, TResult> map)
        {
            return Either.Map(choice, map);
        }

        /// <summary>
        /// Bind the "Right" case
        /// </summary>
        public static Either<TLeft, TResult> Bind<TLeft, TRight, TResult>(Either<TLeft, TRight> choice, Func<TRight, Either<TLeft, TResult>> bind)
        {
            return Either.Match(
                choice,
                left => Either.Left<TLeft, TResult>(left),
                right => bind(right)
                );
        }

        /// <summary>
        /// SelectMany is a synonym for Bind 
        /// </summary>
        public static Either<TLeft, TResult> SelectMany<TLeft, TRight, TResult>(Either<TLeft, TRight> choice, Func<TRight, Either<TLeft, TResult>> bind)
        {
            return Either.Bind(choice, bind);
        }

    }
}
