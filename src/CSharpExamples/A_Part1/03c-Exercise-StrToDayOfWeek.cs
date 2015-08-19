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
    enum DayOfWeek
    {
        Sun, Mon, Tue, Wed, Thu, Fri, Sat
    }


    class StrToDayOfWeekHelper
    {
        /// <summary>
        /// With extended output
        /// </summary>
        public static Option<DayOfWeek> StrToDayOfWeek(string input)
        {
            switch (input)
            {
                default: throw new Exception();
            }
        }

    }

    [TestFixture]
    public class StrToDayOfWeek_Test
    {
        [Test]
        public void StrToDayOfWeek()
        {
            var actual = StrToDayOfWeekHelper.StrToDayOfWeek("Sun");
            Assert.IsTrue(actual.HasValue);
            var expected = DayOfWeek.Sun;
            Assert.AreEqual(expected, actual.Value);
        }

    }

}
