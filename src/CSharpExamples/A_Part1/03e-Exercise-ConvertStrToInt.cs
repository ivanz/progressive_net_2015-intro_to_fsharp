using System;
using NUnit.Framework;

using CSharpExamples.Utilities;

/*
Exercise for total functions

Create TWO functions that converts a string (e.g "123") 
into an int.

V1) Make the function total by constraining the input
V2) Make the function total by extending the output

*/

namespace CSharpExamples.A_Part1
{
    class ConstrainedSomething
    {
        public static ConstrainedSomething Create(string value)
        {
            throw new Exception();
        }
    }


    class ConvertStrToInt
    {
        /// <summary>
        /// With constrained input
        /// </summary>
        public static int ConvertStrToInt_V1(ConstrainedSomething input)
        {
            throw new Exception();
        }

        /// <summary>
        /// With extended output
        /// </summary>
        public static Option<int> ConvertStrToInt_V2(string input)
        {
            throw new Exception();
        }
    }

    [TestFixture]
    public class ConvertStrToIntTest
    {
        [Test]
        public void ConvertStrToInt_V1()
        {
            var allDigitStr = ConstrainedSomething.Create("123");
            var actual = ConvertStrToInt.ConvertStrToInt_V1(allDigitStr);
            const int expected = 123;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ConvertStrToInt_V2()
        {
            var actual = ConvertStrToInt.ConvertStrToInt_V2("123");
            Assert.IsTrue(actual.HasValue);
            const int expected = 123;
            Assert.AreEqual(expected, actual.Value);
        }

    }

}
