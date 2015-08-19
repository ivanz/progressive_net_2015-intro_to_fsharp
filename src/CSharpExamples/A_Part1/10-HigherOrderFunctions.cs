using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using CSharpExamples.Utilities;

namespace CSharpExamples.A_Part1
{
    [TestFixture]
    public class HigherOrderFunctionsTest
    {
        [Test]
        public void Fold()
        {
            var list = new List<int> { 1, 2, 3, 4 };

            Func<int, int, int> sumAction = (sumSoFar, x) => sumSoFar + x;
            var sum = Fun.Fold(sumAction, 0, list);

            var actual = sum;
            var expected = 10;
            Assert.AreEqual(expected, actual);


            Func<int, int, int> productAction = (productSoFar, x) => productSoFar * x;
            var product = Fun.Fold(productAction, 1, list);

            actual = product;
            expected = 24;
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void Map()
        {
            Func<int, int> add1 = x => x + 1;
            Func<int, int> doubl = x => x + x;
            Func<int, int> square = x => x * x;

            var add1ToEveryElement = Fun.Map(add1);
            var doubleEveryElement = Fun.Map(doubl);
            var squareEveryElement = Fun.Map(square);

            var list = new List<int> { 1, 2, 3 };

            var actual = add1ToEveryElement(list);
            var expected = new List<int> { 2, 3, 4 };
            Assert.That(actual, Is.EquivalentTo(expected));

            actual = doubleEveryElement(list);
            expected = new List<int> { 2, 4, 6 }.ToList();
            Assert.That(actual, Is.EquivalentTo(expected));

        }

        [Test]
        public void InlineMap()
        {
            Func<int, int> add1 = x => x + 1;
            Func<int, int> doubl = x => x + x;
            Func<int, int> square = x => x * x;

            var add1ToEveryElement = Fun.Map(add1);
            var doubleEveryElement = Fun.Map(doubl);
            var squareEveryElement = Fun.Map(square);

            var list = new List<int> { 1, 2, 3 };

            var actual = list
                .Pipe(Fun.Map(add1))
                .Pipe(Fun.Map(doubl))
                .Pipe(Fun.Map(square));

            var expected = new List<int> { 16, 36, 64 };
            Assert.That(actual, Is.EquivalentTo(expected));

            // doesn't compile 
            // actual = list
            //    .Pipe(Fun.Map(x => x + 1))
            //    .Pipe(Fun.Map(x => x + x))
            //    .Pipe(Fun.Map(x => x * x));

        }

        [Test]
        public void Filter()
        {

            Func<int, bool> isEven = x => (x % 2 == 0);
            Func<int, bool> isPositive = x => (x > 0);

            var onlyEvenElements = Fun.Filter(isEven);
            var onlyPositiveElements = Fun.Filter(isPositive);

            var list = new List<int> { -1, 0, 1, 2, 3 };

            var actual = onlyEvenElements(list);
            var expected = new List<int> { 0, 2 };
            Assert.That(actual, Is.EquivalentTo(expected));

            actual = onlyPositiveElements(list);
            expected = new List<int> { 1, 2, 3 };
            Assert.That(actual, Is.EquivalentTo(expected));

            actual = list
                .Pipe(Fun.Filter(isEven))
                .Pipe(Fun.Filter(isPositive));

        }

    }

}
