﻿using System;
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
    class AllDigitStringAnswer
    {
        public string Value { get; private set; }

        private AllDigitStringAnswer(string value)
        {
            this.Value = value;
        }

        public static AllDigitStringAnswer Create(string value)
        {
            return new AllDigitStringAnswer(value);
        }
    }


    class ConvertStrToInt_Answer
    {
        /// <summary>
        /// With constrained input
        /// </summary>
        public static int ConvertStrToInt_V1(AllDigitStringAnswer input)
        {
            var str = input.Value;
            return int.Parse(str);
        }

        /// <summary>
        /// With extended output
        /// </summary>
        public static Option<int> ConvertStrToInt_V2(string input)
        {
            int result = 0;
            if (!int.TryParse(input,out result))
            {
                return Option.None<int>();
            }
            else
            {
                return Option.Some(result);
            }
        }
    }

    [TestFixture]
    public class ConvertStrToInt_AnswerTest
    {
        [Test]
        public void ConvertStrToInt_V1()
        {
            var allDigitStr = AllDigitStringAnswer.Create("123");
            var actual = ConvertStrToInt_Answer.ConvertStrToInt_V1(allDigitStr);
            const int expected = 123;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ConvertStrToInt_V2()
        {
            var actual = ConvertStrToInt_Answer.ConvertStrToInt_V2("123");
            Assert.IsTrue(actual.HasValue);
            const int expected = 123;
            Assert.AreEqual(expected, actual.Value);
        }

    }

}
