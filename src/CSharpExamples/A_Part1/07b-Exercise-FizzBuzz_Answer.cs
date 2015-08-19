using NUnit.Framework;

using CSharpExamples.Utilities;

/*
Given a number
If it is divisible by 3, return "Fizz"
If it is divisible by 5, return "Buzz"
If it is divisible by 3 and 5, return "FizzBuzz"
Otherwise return the number as a string
Do NOT print anything

Challenge, write this using a piping model.

*/


namespace CSharpExamples.A_Part1
{
    public class FizzBuzz_Answer
    {
        public class Data
        {
            public string Carbonated;
            public int Number;
        }

        public static Data Test3(Data data)
        {
            // unprocessed
            if (data.Carbonated == "")
            {
                if (data.Number % 3 == 0)
                {
                    return new Data { Carbonated = "Fizz", Number = -1 };
                }
                else
                {
                    return data; // leave alone
                }
            }
            else
            {
                return data; // leave alone
            }
        }

        public static Data Test5(Data data)
        {
            // unprocessed
            if (data.Carbonated == "")
            {
                if (data.Number % 5 == 0)
                {
                    return new Data { Carbonated = "Buzz", Number = -1 };
                }
                else
                {
                    return data; // leave alone
                }
            }
            else
            {
                return data; // leave alone
            }
        }

        public static Data Test15(Data data)
        {
            // unprocessed
            if (data.Carbonated == "")
            {
                if (data.Number % 15 == 0)
                {
                    return new Data { Carbonated = "FizzBuzz", Number = -1 };
                }
                else
                {
                    return data; // leave alone
                }
            }
            else
            {
                return data; // leave alone
            }
        }

        public static string FinalResult(Data data)
        {
            // unprocessed
            if (data.Carbonated == "")
            {
                return string.Format("{0}", data.Number);
            }
            else
            {
                return data.Carbonated;
            }
        }

        public static string FizzBuzz(int aNumber)
        {
            var data = new Data { Carbonated = "", Number = aNumber };
            return data 
            .Pipe(Test15)
            .Pipe(Test3)
            .Pipe(Test5)
            .Pipe(FinalResult);
        }
    }

    [TestFixture]
    public class FizzBuzz_AnswerTest
    {

        [Test]
        public void Test()
        {
            var actual = FizzBuzz_Answer.FizzBuzz(15);
            var expected = "FizzBuzz";
            Assert.AreEqual(expected, actual);

            actual = FizzBuzz_Answer.FizzBuzz(16);
            expected = "16";
            Assert.AreEqual(expected, actual);

            actual = FizzBuzz_Answer.FizzBuzz(5);
            expected = "Buzz";
            Assert.AreEqual(expected, actual);

        }

    }

}
