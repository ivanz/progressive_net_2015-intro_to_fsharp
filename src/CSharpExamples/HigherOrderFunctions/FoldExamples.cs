using System;
using System.Linq;

namespace CSharpExamples.HigherOrderFunctions
{
    static class FoldExamples
    {
        public static int ProductWithAggregate(int n)
        {
            var initialValue = 1;
            Func<int, int, int> action = (productSoFar, x) =>
                productSoFar * x;
            return Enumerable.Range(1, n)
                    .Aggregate(initialValue, action);
        }

        public static int SumOfOddsWithAggregate(int n)
        {
            var initialValue = 0;
            Func<int, int, int> action = (sumSoFar, x) =>
                (x % 2 == 0) ? sumSoFar : sumSoFar + x;
            return Enumerable.Range(1, n)
                .Aggregate(initialValue, action);
        }

        public static int AlternatingSumsWithAggregate(int n)
        {
            var initialValue = Tuple.Create(true, 0);
            Func<Tuple<bool, int>, int, Tuple<bool, int>> action =
                (t, x) => t.Item1
                    ? Tuple.Create(false, t.Item2 - x)
                    : Tuple.Create(true, t.Item2 + x);
            return Enumerable.Range(1, n)
                .Aggregate(initialValue, action)
                .Item2;
        }
    }
}
