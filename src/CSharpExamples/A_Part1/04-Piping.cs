using System;
using NUnit.Framework;

using CSharpExamples.Utilities;

namespace CSharpExamples.A_Part1
{
    class Piping
    {
        public static int Add1_Double_Square(int n)
        {
            Func<int, int> add1 = x => x + 1;
            Func<int, int> doubl = x => x + x;
            Func<int, int> square = x => x * x;

            var result = n.Pipe(add1).Pipe(doubl).Pipe(square);
            return result;
        }
    }

    [TestFixture]
    public class PipingTest
    {
        [Test]
        public void Add1_Double_Square()
        {
            var actual = Piping.Add1_Double_Square(2);
            var expected = 36;
            Assert.AreEqual(expected, actual);
        }

    }

}
