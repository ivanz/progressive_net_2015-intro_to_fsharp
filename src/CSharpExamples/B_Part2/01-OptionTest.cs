using NUnit.Framework;

using CSharpExamples.Utilities;

namespace CSharpExamples.B_Part2
{

    [TestFixture]
    public class OptionTest
    {
        public static string FormatOption(Option<int> opt)
        {
            return Option.Match(opt,
                some => string.Format("Some {0}", some),
                () => "None"
                );

        }
        [Test]
        public void Test()
        {
            var some1 = Option.Some(1);
            var none = Option.None<int>();

            var actual = FormatOption(some1);
            var expected = "Some 1";
            Assert.AreEqual(expected, actual);

            actual = FormatOption(none);
            expected = "None";
            Assert.AreEqual(expected, actual);

        }
    }
}

