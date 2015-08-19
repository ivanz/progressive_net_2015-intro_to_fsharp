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
    public class RomanNumber_Answer
    {
        public enum RomanDigit
        {
            I, V, X
        }

        public class RomanNumber
        {
            public IList<RomanDigit> Digits { get; set; }

            public RomanNumber(IList<RomanDigit> digits)
            {
                this.Digits = digits;
            }
        }

        /// <summary>
        /// DigitToInt
        /// </summary>
        public static int DigitToInt(RomanDigit digit)
        {
            switch (digit)
            {
                case RomanDigit.I:
                    return 1;
                case RomanDigit.V:
                    return 5;
                case RomanDigit.X:
                    return 10;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// RomanNumberToInt
        /// </summary>
        public static int RomanNumberToInt(RomanNumber number)
        {
            return number.Digits.Select(DigitToInt).Sum();
        }

        [Test]
        public void TestRomanNumberToInt()
        {
            var roman = new RomanNumber(new[]
            {
                RomanDigit.X,
                RomanDigit.V,
                RomanDigit.X
            });

            var actual = RomanNumberToInt(roman);
            var expected = 16;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// CharToDigit
        /// </summary>
        public static RomanDigit CharToDigit(char ch)
        {
            switch (ch)
            {
                case 'I': return RomanDigit.I;
                case 'V': return RomanDigit.V;
                case 'X': return RomanDigit.X;
                default: throw new Exception();
            }
        }

        /// <summary>
        /// StrToRomanNumber
        /// </summary>
        public static RomanNumber StrToRomanNumber(string str)
        {
            var digits = str
                    .ToCharArray()
                    .Select(CharToDigit)
                    .ToList();
            return new RomanNumber(digits);
        }


        /// <summary>
        /// CharToRomanDigitWithError
        /// </summary>
        public static RopResult<RomanDigit, string> CharToRomanDigitWithError(char ch)
        {
            switch (ch)
            {
                case 'I': return Rop.Succeed<RomanDigit, string>(RomanDigit.I);
                case 'V': return Rop.Succeed<RomanDigit, string>(RomanDigit.V);
                case 'X': return Rop.Succeed<RomanDigit, string>(RomanDigit.X);
                default:
                    var msg = String.Format("{0} is not a valid char", ch);
                    return Rop.Fail<RomanDigit, string>(msg);
            }
        }

        /// <summary>
        /// StrToRomanNumberWithError
        /// </summary>
        public static RopResult<RomanNumber, string> StrToRomanNumberWithError(string input)
        {
            var digitsWithErrs =
                input
                    .ToCharArray()
                    .Select(CharToRomanDigitWithError);
            var errors =
                digitsWithErrs
                    .Where(r => !r.IsSuccess)
                    .Select(r => r.FailureValues.First());
            if (errors.Any())
            {
                return Rop.Fail<RomanNumber, string>(errors);
            }
            else
            {
                var digits =
                    digitsWithErrs
                        .Where(r => r.IsSuccess)
                        .Select(r => r.SuccessValue)
                        .ToList();
                var romanNumber = new RomanNumber(digits);
                return Rop.Succeed<RomanNumber, string>(romanNumber);
            }
        }
    }

}

