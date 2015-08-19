using System;
using NUnit.Framework;

using CSharpExamples.Utilities;

/// =============================================
/// Think of a number
/// =============================================

/*

Think of a number.
Add one to it.
Square it.
Subtract one.
Divide by the number you first thought of.
Subtract the number you first thought of.
The answer is TWO!

Challenge, write this using a piping model.
Use the code below as a starting point 

TIP: 
you will probably need to define an intermediate data structure to pipe around

*/

namespace CSharpExamples.A_Part1
{
    class ThinkOfANumber_Answer
    {
        public static int ThinkOfANumber(int numberYouThoughtOf)
        {
            Func<int, int> addOne = x => x + 1;
            Func<int, int> squareIt = x => x * x;
            Func<int, int> subtractOne = x => x - 1;
            Func<int, int> divideByTheNumberYouFirstThoughtOf = x => x / numberYouThoughtOf;
            Func<int, int> subtractTheNumberYouFirstThoughtOf = x => x - numberYouThoughtOf;

            // define these functions
            // then combine them using piping

            return numberYouThoughtOf
            .Pipe(addOne)
            .Pipe(squareIt)
            .Pipe(subtractOne)
            .Pipe(divideByTheNumberYouFirstThoughtOf)
            .Pipe(subtractTheNumberYouFirstThoughtOf);

        }
    }

    [TestFixture]
    public class ThinkOfANumber_AnswerTest
    {

        [Test]
        public void Test()
        {
            var actual = ThinkOfANumber_Answer.ThinkOfANumber(99);
            var expected = 2;
            Assert.AreEqual(expected, actual);
        }

    }


}
