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
    class NonZeroIntegerAnswer
    {
        public int Value { get; private set; }

        private NonZeroIntegerAnswer(int value)
        {
            this.Value = value;
        }

        public static NonZeroIntegerAnswer Create(int value)
        {
            return new NonZeroIntegerAnswer(value);
        }
    }


    class TwelveDividedBy_Answer
    {
        /// <summary>
        /// With constrained input
        /// </summary>
        public static int TwelveDividedBy_V1(NonZeroIntegerAnswer input)
        {
            var nz = input.Value;
            return 12/nz;
        }

        /// <summary>
        /// With extended output
        /// </summary>
        public static Option<int> TwelveDividedBy_V2(int input)
        {
            if (input == 0)
            {
                return Option.None<int>();
            }
            else
            {
                return Option.Some(12/input);
            }
        }

    }

    [TestFixture]
    public class TwelveDividedBy_AnswerTest
    {
        [Test]
        public void TwelveDividedBy_V1()
        {
            var nz = NonZeroIntegerAnswer.Create(2);
            var actual = TwelveDividedBy_Answer.TwelveDividedBy_V1(nz);
            const int expected = 6;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TwelveDividedBy_V2()
        {
            var actual = TwelveDividedBy_Answer.TwelveDividedBy_V2(2);
            Assert.IsTrue(actual.HasValue);
            const int expected = 6;
            Assert.AreEqual(expected, actual.Value);
        }

    }

}
