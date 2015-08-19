using NUnit.Framework;

using CSharpExamples.Utilities;

namespace CSharpExamples.C_Part3.TotalFunctions
{
    public class NonZeroInteger
    {
        /// <summary>
        /// Private constructor
        /// </summary>
        private NonZeroInteger(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        public static Option<NonZeroInteger> Create(int i)
        {
            if (i == 0)
            {
                return Option.None<NonZeroInteger>();
            }
            return Option.Some(new NonZeroInteger(i));
        }

        public int Value { get; private set; }
    }


    [TestFixture]
    public class Test
    {

        static int TwelveDividedBy_Exn(int n)
        {
            return 12 / n;
        }

        static Option<int> TwelveDividedBy_Opt(int n)
        {
            if (n == 0)
            {
                return Option.None<int>();
            }
            return Option.Some(12 / n);
        }

        static int TwelveDividedBy_NZ(NonZeroInteger n)
        {
            return 12 / n.Value;
        }

        [Test]
        public void Test_Exn()
        {

            var n = 0;
            var result = TwelveDividedBy_Exn(n);

        }

        [Test]
        public void Test_Opt()
        {

            var n0 = 0;
            var result0 = TwelveDividedBy_Opt(n0);
            Assert.IsFalse(result0.HasValue);

            var n1 = 1;
            var result1 = TwelveDividedBy_Opt(n1);
            Assert.IsTrue(result1.HasValue);

        }

        [Test]
        public void Test_NZ()
        {

            var n0 = NonZeroInteger.Create(0);
            Option.Match(n0,
                some =>
                {
                    var result0 = TwelveDividedBy_NZ(some);
                    Assert.Fail("not expected");
                    return 0;
                },
                () => { Assert.Pass(); return 0; }
                );

            var n1 = NonZeroInteger.Create(1);
            Option.Match(n1,
                some =>
                {
                    var result1 = TwelveDividedBy_NZ(some);
                    Assert.AreEqual(12, result1);
                    return 0;
                },
                () =>
                {
                    Assert.Fail("not expected");
                    return 0;
                });
        }

    }
}



