using System;
using NUnit.Framework;

namespace CSharpExamples.A_Part1
{
    class HelloWorld
    {
        public static void SayHelloWorld()
        {
            Console.WriteLine("hello world");
        }

    }

    [TestFixture]
    public class HelloWorldTest
    {
        [Test]
        public void TestHelloWorld()
        {
            HelloWorld.SayHelloWorld();
        }

    }

}
