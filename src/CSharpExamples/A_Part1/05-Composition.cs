using System;
using NUnit.Framework;

using CSharpExamples.Utilities;

namespace CSharpExamples.A_Part1
{
    class Composition
    {
        public static Func<int, int> Add1_Double_Square()
        {
            Func<int, int> add1 = x => x + 1;
            Func<int, int> doubl = x => x + x;
            Func<int, int> square = x => x * x;

            var composed = add1.Compose(doubl).Compose(square);
            return composed;
        }

    }

    [TestFixture]
    public class CompositionTest
    {
        [Test]
        public void Add1_Double_Square()
        {
            var composed = Composition.Add1_Double_Square();
            var actual = composed(2);
            var expected = 36;
            Assert.AreEqual(expected, actual);
        }


    }

}
