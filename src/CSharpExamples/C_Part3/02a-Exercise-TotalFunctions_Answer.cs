using System;
using NUnit.Framework;

using CSharpExamples.Utilities;

/*
Exercise for total functions

Create a function that converts a string (e.g "Sunday") into an DayOfWeek type

1) Define a DayOfWeek type with a case for each day
2) Define a strToDayOfWeek function  which is total

*/


namespace CSharpExamples.C_Part3.TotalFunctions_Answer
{
    public enum DayOfWeek { Sun, Mon, Tue, Wed, Thu, Fri, Sat }

    public static class DayOfWeekUtil
    {
        /// <summary>
        /// Public constructor
        /// </summary>
        public static Option<DayOfWeek> StrToDayOfWeek(string s)
        {
            switch (s)
            {
                case "Sunday":
                case "Sun":
                    return Option.Some(DayOfWeek.Sun);
                case "Monday":
                case "Mon":
                    return Option.Some(DayOfWeek.Mon);
                case "Tuesday":
                case "Tue":
                    return Option.Some(DayOfWeek.Tue);
                case "Wednesday":
                case "Wed":
                    return Option.Some(DayOfWeek.Wed);
                case "Thursday":
                case "Thu":
                    return Option.Some(DayOfWeek.Thu);
                case "Friday":
                case "Fri":
                    return Option.Some(DayOfWeek.Fri);
                case "Saturday":
                case "Sat":
                    return Option.Some(DayOfWeek.Sat);
                default:
                    return Option.None<DayOfWeek>();
            }
        }
    }
}



