using System;
using CsWebApiExample.Utilities;

namespace CsWebApiExample.DomainModels
{
    /// <summary>
    /// Represents a EmailAddress in the domain. 
    /// A special class is used to avoid primitive obsession and to ensure valid data
    /// </summary>
    /// <remarks>
    /// I have just used a private ctor and a static method to create an instance.
    /// 
    /// You could also provide implicit or explicit operators as documented here:
    /// http://lostechies.com/jimmybogard/2007/12/03/dealing-with-primitive-obsession/
    /// </remarks>
    public class EmailAddress : IEquatable<EmailAddress>
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
            else if (s.Length > 20)   // make short for testing!
            {
                return Option.None<EmailAddress>();
            }
            else
            {
                return Option.Some(new EmailAddress(s));
            }
        }

        public string Value { get; private set; }

        public override string ToString()
        {
            return string.Format("EmailAddress {0}", this.Value);
        }

        // =====================================
        // code for equality
        // =====================================

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as EmailAddress;
            return this.Equals(other);
        }

        public bool Equals(EmailAddress other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Value.Equals(other.Value);
        }

    }
}

