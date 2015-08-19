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

// ReSharper disable once CheckNamespace
namespace CSharpExamples.B_Part2.Contact_Answer
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
            if (s.Length > 1)
            {
                return Option.None<String1>();
            }
            return Option.Some(new String1(s));
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
            if (s.Length > 50)
            {
                return Option.None<String50>();
            }
            return Option.Some(new String50(s));
        }

        public string Value { get; private set; }
    }



    // ----------------------------------------
    // Main domain code
    // ----------------------------------------

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
            if (!s.Contains("@"))
            {
                return Option.None<EmailAddress>();
            }
            return Option.Some(new EmailAddress(s));
        }

        public string Value { get; private set; }
    }



    public class VerifiedEmail
    {
        public VerifiedEmail(EmailAddress value)
        {
            this.Value = value;
        }

        public EmailAddress Value { get; private set; }
    }


    public class EmailContactInfo
    {
        private object _value;

        /// <summary>
        /// private ctor
        /// </summary>
        private EmailContactInfo() { }

        /// <summary>
        /// Inner case class
        /// </summary>
        private class UnverifiedCase
        {
            public UnverifiedCase(EmailAddress value) { this.Value = value; }
            public EmailAddress Value { get; private set; }
        }

        /// <summary>
        /// Inner case class
        /// </summary>
        private class VerifiedCase
        {
            public VerifiedCase(VerifiedEmail value) { this.Value = value; }
            public VerifiedEmail Value { get; private set; }
        }

        /// <summary>
        /// Construct an Unverified case
        /// </summary>
        public static EmailContactInfo Unverified(EmailAddress value)
        {
            return new EmailContactInfo { _value = new UnverifiedCase(value) };
        }

        /// <summary>
        /// Construct a Verified case
        /// </summary>
        public static EmailContactInfo Verified(VerifiedEmail value)
        {
            return new EmailContactInfo { _value = new VerifiedCase(value) };
        }


        /// <summary>
        /// Match on the two cases
        /// </summary>
        public T Match<T>(Func<EmailAddress, T> unverifiedCase, Func<VerifiedEmail, T> verifiedCase)
        {
            if (this._value is UnverifiedCase)
            {
                var obj = (UnverifiedCase)this._value;
                return unverifiedCase(obj.Value);
            }
            if (this._value is VerifiedCase)
            {
                var obj = (VerifiedCase)this._value;
                return verifiedCase(obj.Value);
            }
            throw new Exception("should never happen");
        }
    }


    public class PostalContactInfo
    {
        public PostalContactInfo(String50 address1, String50 address2, String50 address3, String50 address4, String50 country)
        {
            this.Address1 = address1;
            this.Address2 = address2;
            this.Address3 = address3;
            this.Address4 = address4;
            this.Country = country;
        }
        public String50 Address1 { get; private set; }
        public String50 Address2 { get; private set; }
        public String50 Address3 { get; private set; }
        public String50 Address4 { get; private set; }
        public String50 Country { get; private set; }
    }


    public class ContactInfo
    {
        private object _value;

        /// <summary>
        /// private ctor
        /// </summary>
        private ContactInfo() { }

        /// <summary>
        /// Inner case class
        /// </summary>
        private class EmailOnlyCase
        {
            public EmailOnlyCase(EmailContactInfo value) { this.Value = value; }
            public EmailContactInfo Value { get; private set; }
        }

        /// <summary>
        /// Inner case class
        /// </summary>
        private class AddrOnlyCase
        {
            public AddrOnlyCase(PostalContactInfo value) { this.Value = value; }
            public PostalContactInfo Value { get; private set; }
        }

        /// <summary>
        /// Inner case class
        /// </summary>
        private class EmailAndAddrCase
        {
            public EmailAndAddrCase(EmailContactInfo value1, PostalContactInfo value2) { this.Value1 = value1; this.Value2 = value2;}
            public EmailContactInfo Value1 { get; private set; }
            public PostalContactInfo Value2 { get; private set; }
        }

        /// <summary>
        /// Construct an EmailOnlyCase case
        /// </summary>
        public static ContactInfo EmailOnly(EmailContactInfo value)
        {
            return new ContactInfo { _value = new EmailOnlyCase(value) };
        }

        /// <summary>
        /// Construct a AddrOnly case
        /// </summary>
        public static ContactInfo AddrOnly(PostalContactInfo value)
        {
            return new ContactInfo { _value = new AddrOnlyCase(value) };
        }

        /// <summary>
        /// Construct a EmailAndAddr case
        /// </summary>
        public static ContactInfo EmailAndAddr(EmailContactInfo value1, PostalContactInfo value2)
        {
            return new ContactInfo { _value = new EmailAndAddrCase(value1, value2) };
        }

        /// <summary>
        /// Match on the three cases
        /// </summary>
        public T Match<T>(Func<EmailContactInfo, T> emailOnlyCase, Func<PostalContactInfo, T> addrOnlyCase, Func<EmailContactInfo, PostalContactInfo, T> emailAndAddrCase)
        {
            if (this._value is EmailOnlyCase)
            {
                var obj = (EmailOnlyCase)this._value;
                return emailOnlyCase(obj.Value);
            }
            if (this._value is AddrOnlyCase)
            {
                var obj = (AddrOnlyCase)this._value;
                return addrOnlyCase(obj.Value);
            }
            if (this._value is EmailAndAddrCase)
            {
                var obj = (EmailAndAddrCase)this._value;
                return emailAndAddrCase(obj.Value1, obj.Value2);
            }
            throw new Exception("should never happen");
        }
    }


    public class PersonalName
    {
        public PersonalName(String50 firstName, Option<String1> middleInitial, String50 lastName)
        {
            this.FirstName = firstName;
            this.MiddleInitial = middleInitial;
            this.LastName = lastName;
        }
        public String50 FirstName { get; private set; }
        public Option<String1> MiddleInitial { get; private set; }
        public String50 LastName { get; private set; }
    }

    public class Contact
    {
        public Contact(PersonalName name, ContactInfo contactInfo)
        {
            this.Name = name;
            this.ContactInfo = contactInfo;
        }
        public PersonalName Name { get; private set; }
        public ContactInfo ContactInfo { get; private set; }
    }

}
