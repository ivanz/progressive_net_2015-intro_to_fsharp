using System;
using NUnit.Framework;

using CSharpExamples.Utilities;

// Create an input log function and an output log function
// and then use them to create "logged" version of add1

namespace CSharpExamples.A_Part1
{
    [TestFixture]
    public class Decorator_Answer
    {
        [Test]
        public void Test()
        {
            Func<int, int> add1 = x => x + 1;
            Func<int, int> inLog = x => {
                Console.Write("In={0}; ",x);
                return x;
                };
            Func<int, int> outLog = x =>
            {
                Console.Write("Out={0}; ", x);
                return x;
            };

            var add1Logged = inLog.Compose(add1).Compose(outLog);

            var five = add1Logged(4);
        }


    }

}
