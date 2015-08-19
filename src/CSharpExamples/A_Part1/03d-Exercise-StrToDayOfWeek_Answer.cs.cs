using System;
using NUnit.Framework;

using CSharpExamples.Utilities;

/*
Exercise for total functions

Create a function that converts a string 
(e.g "Sunday" or "Sun") into an DayOfWeek type

1) Define a DayOfWeek type with a case for each day
2) Define a StrToDayOfWeek function  which is total

*/

namespace CSharpExamples.A_Part1
{
    enum DayOfWeekAnswer
    {
        Sun, Mon, Tue, Wed, Thu, Fri, Sat
    }


    class StrToDayOfWeek_Answer
    {
        /// <summary>
        /// With extended output
        /// </summary>
        public static Option<DayOfWeekAnswer> StrToDayOfWeek(string input)
        {
            switch (input)
            {
                case "Sun":
                case "Sunday":
                    return Option.Some(DayOfWeekAnswer.Sun);
                case "Mon":
                case "Monday":
                    return Option.Some(DayOfWeekAnswer.Mon);
                case "Tue":
                case "Tuesday":
                    return Option.Some(DayOfWeekAnswer.Tue);
                    // etc
                default:
                    return Option.None<DayOfWeekAnswer>();

            }
        }

    }

    [TestFixture]
    public class StrToDayOfWeek_AnswerTest
    {
        [Test]
        public void StrToDayOfWeek()
        {
            var actual = StrToDayOfWeek_Answer.StrToDayOfWeek("Sun");
            Assert.IsTrue(actual.HasValue);
            var expected = DayOfWeekAnswer.Sun;
            Assert.AreEqual(expected, actual.Value);
        }

    }

}
