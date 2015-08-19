using System;
using CsWebApiExample.Utilities;

namespace CsWebApiExample.DomainModels
{
    public class String10 : IEquatable<String10>
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

        // =====================================
        // code for equality
        // =====================================

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as String10;
            return this.Equals(other);
        }

        public bool Equals(String10 other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Value.Equals(other.Value);
        }
    }
}