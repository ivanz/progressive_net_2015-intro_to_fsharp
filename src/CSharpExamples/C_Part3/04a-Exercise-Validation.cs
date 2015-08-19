using System;
using CSharpExamples.Utilities;

/*
Exercise

1) create a constrained type for Age (age must be between 0 and 130) 
2) create a constrained type for Email where it must contain an @ sign
3) reuse the PersonalName from the previous example
4) create a type Person with 
    * property Name of type PersonalName
    * property Age of type Age
    * property Email of type Email
4) create a type PersonDto with same properties as primitive types (string,int)
   (This will be used for XML or JSON serialization)
5) create a function "toDto" that converts a Person into a DTO
6) create a function "fromDto" that converts a DTO into Person 
*/
namespace CSharpExamples.C_Part3.Validation_Exercise
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

    public class Age
    {
        /// <summary>
        /// Private constructor
        /// </summary>
        private Age(int value)
        {
            throw new NotImplementedException();
        }

    }

    public class Email
    {
        /// <summary>
        /// Private constructor
        /// </summary>
        private Email(string value)
        {
            throw new NotImplementedException();
        }
    }

    public class PersonalName
    {
        public PersonalName(String10 first, String10 last)
        {
            this.First = first;
            this.Last = last;
        }

        public String10 First { get; private set; }
        public String10 Last { get; private set; }
    }

    public class Person
    {
    }

    public class PersonDto
    {
    }

    public static class PersonUtils
    {
        public static PersonDto ToDto(Person person)
        {
            throw new NotImplementedException();
        }


        public static Option<Person> FromDto(PersonDto dto)
        {
            throw new NotImplementedException();
        }

    }
}


