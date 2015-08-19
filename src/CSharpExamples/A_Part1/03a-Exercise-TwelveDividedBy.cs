using System;
using NUnit.Framework;

using CSharpExamples.Utilities;

/*
Exercise for total functions

Create a function that divides 12 by an int.
The input will be in the range 12 to -12

1) Make the function total by constraining the input
2) Make the function total by extending the output

*/

namespace CSharpExamples.A_Part1
{
    class NonZeroInteger
    {
        public static NonZeroInteger Create(int value)
        {
            throw new Exception();
        }
    }


    class TwelveDividedBy
    {
        /// <summary>
        /// With constrained input
        /// </summary>
        public static int TwelveDividedBy_V1(NonZeroInteger input)
        {
            throw new Exception();
        }

        /// <summary>
        /// With extended output
        /// </summary>
        public static Option<int> TwelveDividedBy_V2(int input)
        {
            throw new Exception();
        }

    }

    [TestFixture]
    public class TwelveDividedByTest
    {
        [Test]
        public void TwelveDividedBy_V1()
        {
            var nz = NonZeroInteger.Create(2);
            var actual = TwelveDividedBy.TwelveDividedBy_V1(nz);
            const int expected = 6;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TwelveDividedBy_V2()
        {
            var actual = TwelveDividedBy.TwelveDividedBy_V2(2);
            Assert.IsTrue(actual.HasValue);
            const int expected = 6;
            Assert.AreEqual(expected, actual.Value);
        }

    }

}
