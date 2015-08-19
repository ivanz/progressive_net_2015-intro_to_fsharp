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
namespace CSharpExamples.C_Part3.Validation_Answer
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
            this.Value = value;
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        public static Option<Age> Create(int i)
        {
            if (i < 0)
            {
                return Option.None<Age>();
            }
            if (i > 130)
            {
                return Option.None<Age>();
            }
            return Option.Some(new Age(i));
        }

        public int Value { get; private set; }
    }

    public class Email
    {
        /// <summary>
        /// Private constructor
        /// </summary>
        private Email(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        public static Option<Email> Create(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return Option.None<Email>();
            }
            if (!s.Contains("@"))
            {
                return Option.None<Email>();
            }
            return Option.Some(new Email(s));
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

    public class Person
    {
        public Person(PersonalName name, Age age, Email email)
        {
            this.Name = name;
            this.Age = age;
            this.Email = email;
        }

        public static Person Create(PersonalName name, Age age, Email email)
        {
            return new Person(name, age, email);
        }

        public PersonalName Name { get; private set; }
        public Age Age { get; private set; }
        public Email Email { get; private set; }

    }

    public class PersonDto
    {

        public string First { get; set; }
        public string Last { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

    }

    public static class PersonUtils
    {
        public static PersonDto ToDto(Person person)
        {
            return new PersonDto
            {
                First = person.Name.First.Value,
                Last = person.Name.Last.Value,
                Age = person.Age.Value,
                Email = person.Email.Value,
            };
        }


        public static Option<Person> FromDto(PersonDto dto)
        {
            var firstO = String10.Create(dto.First);
            var lastO = String10.Create(dto.Last);
            var ageO = Age.Create(dto.Age);
            var emailO = Email.Create(dto.Email);
            var createNameO = Option.Lift2<String10, String10, PersonalName>(PersonalName.Create);
            var nameO = createNameO(firstO, lastO);
            var createPersonO = Option.Lift3<PersonalName, Age, Email, Person>(Person.Create);
            var personO = createPersonO(nameO, ageO, emailO);
            return personO;
        }

    }
}


