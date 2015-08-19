using NUnit.Framework;

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
    public class FizzBuzz_Exercise
    {

        public static string FizzBuzz(int aNumber)
        {
            return "??";
        }
    }

    [TestFixture]
    public class FizzBuzz_ExerciseTest
    {

        [Test]
        public void Test()
        {
            var actual = FizzBuzz_Exercise.FizzBuzz(15);
            var expected = "FizzBuzz";
            Assert.AreEqual(expected, actual);

            actual = FizzBuzz_Exercise.FizzBuzz(16);
            expected = "16";
            Assert.AreEqual(expected, actual);

            actual = FizzBuzz_Exercise.FizzBuzz(5);
            expected = "Buzz";
            Assert.AreEqual(expected, actual);

        }

    }

}
