using System;
using NUnit.Framework;

namespace CSharpExamples.A_Part1
{
    class Values
    {
        public static int X
        {
            get
            {
                return 1;
            }
        }

        public static Func<int, int> Add1
        {
            get
            {
                Func<int, int> add1 = x => x + 1;
                return add1;
            }
        }
    }

    [TestFixture]
    public class ValuesTest
    {
        [Test]
        public void TestAdd1()
        {
            var actual = Values.Add1.Invoke(2);
            var expected = 3;
            Assert.AreEqual(expected, actual);
        }

    }

}
