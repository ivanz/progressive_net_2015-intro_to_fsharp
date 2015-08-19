using System;

namespace CsWebApiExample.DomainModels
{
    /// <summary>
    /// Represents a PersonalName in the domain. 
    /// </summary>
    public class PersonalName : IEquatable<PersonalName>
    {
        // No validation needed because types are already validated
        private PersonalName(String10 first, String10 last)
        {
            First = first;
            Last = last;
        }

        /// <summary>
        /// Create a new customer from the parameters. If not valid, return null
        /// </summary>
        public static PersonalName Create(String10 first, String10 last)
        {
            if (first == null) { return null; }
            if (last == null) { return null; }

            // Compare this with the F# version, where the domain object
            // doesn't need to check for nulls

            return new PersonalName(first,last);
        }
        /// <summary>
        /// First name property
        /// </summary>
        public String10 First { get; private set; }

        /// <summary>
        /// Last name property
        /// </summary>
        public String10 Last { get; private set; }

        public override string ToString()
        {
            return string.Format("PersonalName {0} {1}", this.First,this.Last);
        }

        // =====================================
        // code for equality
        // =====================================

        public override int GetHashCode()
        {
            return this.First.GetHashCode() ^ this.Last.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as PersonalName;
            return this.Equals(other);
        }

        public bool Equals(PersonalName other)
        {
            if (other == null)
            {
                return false;
            }
            return this.First.Equals(other.First) && this.Last.Equals(other.Last);
        }

    }
}
