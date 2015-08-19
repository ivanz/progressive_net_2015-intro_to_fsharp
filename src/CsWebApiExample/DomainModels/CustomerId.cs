using System;
using CsWebApiExample.Utilities;

namespace CsWebApiExample.DomainModels
{
    /// <summary>
    /// Represents a CustomerId in the domain. 
    /// A special class is used to avoid primitive obsession and to ensure valid data
    /// </summary>
    public class CustomerId : IEquatable<CustomerId>
    {
        // private constructor to force use of static
        private CustomerId(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Create a new CustomerId from an int. If not valid, return null
        /// </summary>
        public static Option<CustomerId> Create(int id)
        {
            if (id < 1) return Option.None<CustomerId>();

            return Option.Some(new CustomerId(id));
        }

        /// <summary>
        /// Value property
        /// </summary>
        public int Value { get; private set; }

        public override string ToString()
        {
            return string.Format("CustomerId {0}", this.Value);
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
            var other = obj as CustomerId;
            return this.Equals(other);
        }

        public bool Equals(CustomerId other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Value.Equals(other.Value);
        }

    }
}
