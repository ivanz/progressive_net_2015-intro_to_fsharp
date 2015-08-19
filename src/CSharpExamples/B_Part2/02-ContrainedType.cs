using System;
using NUnit.Framework;

using CSharpExamples.Utilities;

namespace CSharpExamples.B_Part2.ConstrainedType
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
            else if (s.Length > 10)
            {
                return Option.None<String10>();
            }
            else
            {
                return Option.Some(new String10(s));
            }
        }

        public string Value { get; private set; }
    }

    public class EmailAddress
    {
        /// <summary>
        /// Private constructor
        /// </summary>
        private EmailAddress(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        public static Option<EmailAddress> Create(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return Option.None<EmailAddress>();
            }
            else if (!s.Contains("@"))
            {
                return Option.None<EmailAddress>();
            }
            else
            {
                return Option.Some(new EmailAddress(s));
            }
        }

        public string Value { get; private set; }
    }

  
    [TestFixture]
    public class ConstrainedType
    {
        [Test]
        public void TestString10()
        {
            var valid = String10.Create("1234567890");
            var invalid = String10.Create("12345678901");

            Option.Match(valid,
                some => { Assert.Pass(); return 1; },  // OK
                () => { Assert.Fail(); return 0; }  // fail
                );

            Option.Match(invalid,
                some => { Assert.Fail(); return 0; },  // fail
                () => { Assert.Pass(); return 1; }  // OK
                );

        }

        [Test]
        public void TestEmail()
        {
            var valid = EmailAddress.Create("a@example.com");
            var invalid = EmailAddress.Create("example.com");

            Option.Match(valid,
                some => { Assert.Pass(); return 1; },  // OK
                () => { Assert.Fail(); return 0; }  // fail
                );

            Option.Match(invalid,
                some => { Assert.Fail(); return 0; },  // fail
                () => { Assert.Pass(); return 1; }  // OK
                );


        }

    }
}

