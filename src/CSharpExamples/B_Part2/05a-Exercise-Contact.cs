using System;
using CSharpExamples.Utilities;

/*
REQUIREMENTS

The Contact management system stores Contacts

A Contact has 
* a personal name
* an optional email address
* an optional postal address
* Rule: a contact must have an email or a postal address

A Personal Name consists of a first name, middle initial, last name
* Rule: the first name and last name are required
* Rule: the middle initial is optional
* Rule: the first name and last name must not be more than 50 chars
* Rule: the middle initial is exactly 1 char, if present

A postal address consists of four address fields plus a country

Rule: An Email Address can be verified or unverified

*/

namespace CSharpExamples.B_Part2.Contact_Exercise
{

    // ----------------------------------------
    // Helper module
    // ----------------------------------------

    public class String1
    {
        /// <summary>
        /// Private constructor
        /// </summary>
        private String1(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        public static Option<String1> Create(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return Option.None<String1>();
            }
            else if (s.Length > 1)
            {
                return Option.None<String1>();
            }
            else
            {
                return Option.Some(new String1(s));
            }
        }

        public string Value { get; private set; }
    }


    public class String50
    {
        /// <summary>
        /// Private constructor
        /// </summary>
        private String50(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        public static Option<String50> Create(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return Option.None<String50>();
            }
            else if (s.Length > 50)
            {
                return Option.None<String50>();
            }
            else
            {
                return Option.Some(new String50(s));
            }
        }

        public string Value { get; private set; }
    }



    // ----------------------------------------
    // Main domain code
    // ----------------------------------------

}
