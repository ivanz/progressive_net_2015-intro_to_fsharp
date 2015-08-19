using System;
using NUnit.Framework;

using CSharpExamples.Utilities;

namespace CSharpExamples.C_Part3.Validation
{

    public class String10
    {
        /// <summary>
        /// Private constructor
        /// </summary>
        private String10(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        public static Option<String10> Create(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return Option.None<String10>();
            }
            if (s.Length > 10)
            {
                return Option.None<String10>();
            }
            return Option.Some(new String10(s));
        }

        public string Value { get; private set; }
    }


    public class PersonalName
    {
        public PersonalName(String10 first, String10 last)
        {
            this.First = first;
            this.Last = last;
        }

        public static PersonalName Create(String10 first, String10 last)
        {
            return new PersonalName(first, last);
        }

        public String10 First { get; private set; }
        public String10 Last { get; private set; }
    }

    [TestFixture]
    public class Test
    {
        [Test]
        public void CreateGood()
        {

            var firstO = String10.Create("Alice");
            var lastO = String10.Create("Adams");
            var createNameO = Option.Lift2<String10, String10, PersonalName>(PersonalName.Create);
            var nameO = createNameO(firstO, lastO);

            Assert.IsTrue(nameO.HasValue);
            Assert.AreEqual("Alice",nameO.Value.First.Value);
        }

        [Test]
        public void CreateBad()
        {

            var firstO = String10.Create("");
            var lastO = String10.Create("Adams");
            var createNameO = Option.Lift2<String10, String10, PersonalName>(PersonalName.Create);
            var nameO = createNameO(firstO, lastO);

            Assert.IsFalse(nameO.HasValue);
        }

    }
}



