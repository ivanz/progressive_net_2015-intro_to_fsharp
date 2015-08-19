using System;
using NUnit.Framework;

using CSharpExamples.Utilities;

namespace CSharpExamples.A_Part1
{
    class MultipleArguments
    {
        public static Func<int, int> Add1()
        {
            Func<int, int, int> add = (x, y) => x + y;

            var curriedAdd = add.Curry();
            var add1 = curriedAdd(1);
            return add1;
        }

        public static Func<int, Func<int, int>> AddThreeParam1()
        {
            Func<int, int, int, int> add = (x, y, z) => x + y + z;

            var curriedAdd = add.Curry();
            var add1 = curriedAdd(1);
            return add1;
        }

        public static Func<string, string, string> FormatMessage()
        {
            return (message, other) => String.Format("{0}: {1}", message, other);
        }

    }

    [TestFixture]
    public class MultipleArgumentsTest
    {
        [Test]
        public void Add1()
        {
            var add1 = MultipleArguments.Add1();
            var actual = add1(2);
            var expected = 3;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddThreeParam1()
        {
            var add1 = MultipleArguments.AddThreeParam1();
            var actual = add1(2)(3);
            var expected = 6;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PartialApplication_Inline()
        {
            Func<string, string, string> formatMessage =
                (message, other) => String.Format("{0}: {1}", message, other);

            var hello = formatMessage.Apply("Hello");
            hello("Scott");

            var goodbye = formatMessage.Apply("Goodbye");
            goodbye("Scott");

            var actual = hello("Scott");
            var expected = "Hello: Scott";
            Assert.AreEqual(expected, actual);

            actual = goodbye("Scott");
            expected = "Goodbye: Scott";
            Assert.AreEqual(expected, actual);


            Func<int, int, int> add = (x, y) => x + y;
            Func<int, int, int> multiply = (x, y) => x * y;

            var add1 = add.Apply(1);
            var doubl = multiply.Apply(2);

            var actual2 = 2.Pipe(add1).Pipe(doubl);
            var expected2 = 6;
            Assert.AreEqual(expected2, actual2);
        }

        [Test]
        public void PartialApplication_WithPipe()
        {
            Func<string, string, string> formatMessage =
                (message, other) => String.Format("{0}: {1}", message, other);

            var actual = "Scott".Pipe(formatMessage.Apply("Hello"));
            var expected = "Hello: Scott";
            Assert.AreEqual(expected, actual);

            Func<int, int, int> add = (x, y) => x + y;
            Func<int, int, int> multiply = (x, y) => x * y;

            var actual2 = 
                2
                .Pipe(add.Apply(1))
                .Pipe(multiply.Apply(2));

            var expected2 = 6;
            Assert.AreEqual(expected2, actual2);

        }

        [Test]
        public void PartialApplication_WithExternalFunction()
        {
            var hello = MultipleArguments.FormatMessage().Apply("Hello");
            var goodbye = MultipleArguments.FormatMessage().Apply("Goodbye");

            var actual = hello("Scott");
            var expected = "Hello: Scott";
            Assert.AreEqual(expected, actual);

            actual = goodbye("Scott");
            expected = "Goodbye: Scott";
            Assert.AreEqual(expected, actual);

        }

    }

}
