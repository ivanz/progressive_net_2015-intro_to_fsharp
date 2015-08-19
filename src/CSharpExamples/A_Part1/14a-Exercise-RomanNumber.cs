using System;
using System.Collections.Generic;
using System.Linq;
using CsWebApiExample.Utilities;
using NUnit.Framework;
using System.Data;

using CSharpExamples.Utilities;

/*
Given 
a) a function that gets data from a database
b) a function that gets data from an in-memory list
 
Create versions of these two functions that have the SAME abstract "interface" 
so that either can passed to a client who does not care about the implementation.
*/

namespace CSharpExamples.A_Part1
{


    [TestFixture]
    public class RomanNumberExercise
    {
        public enum RomanDigit
        {
            I, V, X
        }

        public class RomanNumber
        {

            public RomanNumber()
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// DigitToInt
        /// </summary>
        public static int DigitToInt(RomanDigit digit)
        {
            throw new Exception();
        }

        /// <summary>
        /// RomanNumberToInt
        /// </summary>
        public static int RomanNumberToInt(RomanNumber number)
        {
            throw new Exception();
        }

        [Test]
        public void TestRomanNumberToInt()
        {
            //var roman = new RomanNumber(new[]
            //{
            //    RomanDigit.X,
            //    RomanDigit.V,
            //    RomanDigit.X
            //});

            //var actual = RomanNumberToInt(roman);
            //var expected = 16;
            //Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// CharToDigit
        /// </summary>
        public static RomanDigit CharToDigit(char ch)
        {
            throw new Exception();
        }

        /// <summary>
        /// StrToRomanNumber
        /// </summary>
        public static RomanNumber StrToRomanNumber(string str)
        {
            throw new Exception();
        }


        /// <summary>
        /// CharToRomanDigitWithError
        /// </summary>
        public static RopResult<RomanDigit, string> CharToRomanDigitWithError(char ch)
        {
            throw new Exception();
        }

        /// <summary>
        /// StrToRomanNumberWithError
        /// </summary>
        public static RopResult<RomanNumber, string> StrToRomanNumberWithError(string input)
        {
            throw new Exception();
        }
    }

}

