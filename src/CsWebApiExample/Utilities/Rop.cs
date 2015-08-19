using System;
using System.Collections.Generic;
using System.Linq;

namespace CsWebApiExample.Utilities
{
    public class RopResult<TSuccess, TFailure> : Either<List<TFailure>, TSuccess>
    {
        public RopResult(List<TFailure> leftValue, TSuccess rightValue, bool hasLeftValue)
            : base(leftValue, rightValue, hasLeftValue)
        {
        }

        public TSuccess SuccessValue
        {
            get { return this.RightValue; }
        }

        public List<TFailure> FailureValues
        {
            get { return this.LeftValue; }
        }

        public bool IsSuccess
        {
            get { return !this.HasLeftValue; }
        }
    }

    public static class Rop
    {
        /// <summary>
        /// Succeed with a value
        /// </summary>
        public static RopResult<TSuccess, TFailure> Succeed<TSuccess, TFailure>(TSuccess successValue)
        {
            return new RopResult<TSuccess, TFailure>(null, successValue, false);
        }

        /// <summary>
        /// Succeed with a value and one message
        /// </summary>
        public static RopResult<TSuccess, TFailure> SucceedWithMsg<TSuccess, TFailure>(TSuccess successValue, TFailure msg)
        {
            // TODO
            return new RopResult<TSuccess, TFailure>(null, successValue, false);
        }

        /// <summary>
        /// Fail with many messages
        /// </summary>
        public static RopResult<TSuccess, TFailure> Fail<TSuccess, TFailure>(IEnumerable<TFailure> failureValues)
        {
            return new RopResult<TSuccess, TFailure>(failureValues.ToList(), default(TSuccess), true);
        }

        /// <summary>
        /// Fail with one message
        /// </summary>
        public static RopResult<TSuccess, TFailure> Fail<TSuccess, TFailure>(TFailure failureValue)
        {
            return Fail<TSuccess, TFailure>(new[] { failureValue });
        }

        /// <summary>
        /// Pattern match by providing a function for each case
        /// </summary>
        public static TResult Match<TSuccess, TFailure, TResult>(RopResult<TSuccess, TFailure> ropResult, Func<TSuccess, TResult> successCase, Func<List<TFailure>, TResult> failureCase)
        {
            if (ropResult.IsSuccess)
            {
                return successCase(ropResult.SuccessValue);
            }
            return failureCase(ropResult.FailureValues);
        }

        /// <summary>
        /// Convert a T->RopResult function to a RopResult->RopResult function
        /// </summary>
        public static Func<RopResult<T1, TFailure>, RopResult<T2, TFailure>> Bind<T1, T2, TFailure>(Func<T1, RopResult<T2, TFailure>> func)
        {
            return arg =>
                Match(arg,
                    successValue => func(successValue),
                    Fail<T2, TFailure>
                    );
        }

        /// <summary>
        /// Lift a function to a function on wrapped values
        /// </summary>
        public static Func<RopResult<T1, TFailure>, RopResult<T2, TFailure>> Lift<T1, T2, TFailure>(Func<T1, T2> func)
        {
            return arg =>
                Match(arg,
                    successValue => Succeed<T2, TFailure>(func(successValue)),
                    Fail<T2, TFailure>
                    );
        }

        /// <summary>
        /// Apply a wrapped function to a wrapped value
        /// </summary>
        public static Func<RopResult<Func<T1, T2>, TFailure>, RopResult<T1, TFailure>, RopResult<T2, TFailure>> Apply<T1, T2, TFailure>()
        {
            return (func, arg) =>
            {
                if (func.IsSuccess && arg.IsSuccess)
                {
                    return Succeed<T2, TFailure>(func.SuccessValue(arg.SuccessValue));
                }
                if (arg.IsSuccess)
                {
                    return Fail<T2, TFailure>(func.FailureValues);
                }
                if (func.IsSuccess)
                {
                    return Fail<T2, TFailure>(arg.FailureValues);
                }

                var allErrors = new List<TFailure>();
                allErrors.AddRange(func.FailureValues);
                allErrors.AddRange(arg.FailureValues);
                return Fail<T2, TFailure>(allErrors);
            };
        }


        /// <summary>
        /// Lift a two argument function to a wrapped version
        /// </summary>
        public static Func<RopResult<T1, TFailure>, RopResult<T2, TFailure>, RopResult<T3, TFailure>> Lift2<T1, T2, T3, TFailure>(Func<T1, T2, T3> func)
        {
            var lifted = Succeed<Func<T1, Func<T2, T3>>, TFailure>(func.Curry());
            return (arg1, arg2) =>
            {
                var r1 = Apply<T1, Func<T2, T3>, TFailure>()(lifted, arg1);
                var r2 = Apply<T2, T3, TFailure>()(r1, arg2);
                return r2;
            };
        }

        /// <summary>
        /// Lift a three argument function to a wrapped version
        /// </summary>
        public static Func<RopResult<T1, TFailure>, RopResult<T2, TFailure>, RopResult<T3, TFailure>, RopResult<T4, TFailure>> Lift3<T1, T2, T3, T4, TFailure>(Func<T1, T2, T3, T4> func)
        {
            var lifted = Succeed<Func<T1, Func<T2, Func<T3, T4>>>, TFailure>(func.Curry());
            return (arg1, arg2, arg3) =>
            {
                var r1 = Apply<T1, Func<T2, Func<T3, T4>>, TFailure>()(lifted, arg1);
                var r2 = Apply<T2, Func<T3, T4>, TFailure>()(r1, arg2);
                var r3 = Apply<T3, T4, TFailure>()(r2, arg3);
                return r3;
            };
        }

        /// <summary>
        /// Execute a unit function on the success branch
        /// </summary>
        public static Func<RopResult<T1, TFailure>, RopResult<T1, TFailure>> SuccessTee<T1, TFailure>(Func<T1, Unit> func)
        {
            return arg =>
            {
                if (arg.IsSuccess)
                {
                    func(arg.SuccessValue);
                    return arg;
                }
                return arg;
            };
        }

        /// <summary>
        /// Execute a unit function on the success branch
        /// </summary>
        public static Func<RopResult<T1, TFailure>, RopResult<T1, TFailure>> SuccessTee<T1, TFailure>(Action<T1> action)
        {
            return SuccessTee<T1, TFailure>(action.ToFunc());
        }

        /// <summary>
        /// Execute a unit function on the failure branch
        /// </summary>
        public static Func<RopResult<T1, TFailure>, RopResult<T1, TFailure>> FailureTee<T1, TFailure>(Func<List<TFailure>, Unit> func)
        {
            return arg =>
            {
                if (!arg.IsSuccess)
                {
                    func(arg.FailureValues);
                    return arg;
                }
                return arg;
            };
        }

        /// <summary>
        /// Execute a unit function on the failure branch
        /// </summary>
        public static Func<RopResult<T1, TFailure>, RopResult<T1, TFailure>> FailureTee<T1, TFailure>(Action<List<TFailure>> action)
        {
            return FailureTee<T1, TFailure>(action.ToFunc());
        }


    }
}
