using System;

namespace CsWebApiExample.DomainModels
{
    public class DomainMessage
    {
        enum Tag
        {
            CustomerIsRequired
           ,
            CustomerIdMustBePositive
                ,
            FirstNameIsRequired
                ,
            FirstNameMustNotBeMoreThan10Chars
                ,
            LastNameIsRequired
                ,
            LastNameMustNotBeMoreThan10Chars
                ,
            EmailIsRequired
                ,
            EmailMustNotBeMoreThan20Chars
                ,
            EmailMustContainAtSign

                // Events
                ,
            EmailAddressChanged

                // Exposed errors
                ,
            CustomerNotFound

                // Internal errors
                ,
            SqlCustomerIsInvalid
                ,
            DatabaseTimeout
                , DatabaseError
        }

        private DomainMessage() { }

        private Tag TagValue { get; set; }
        private object DataValue { get; set; }

        public static DomainMessage CustomerIsRequired()
        {
            return new DomainMessage { TagValue = Tag.CustomerIsRequired };
        }

        public static DomainMessage CustomerIdMustBePositive()
        {
            return new DomainMessage { TagValue = Tag.CustomerIdMustBePositive };
        }

        public static DomainMessage FirstNameIsRequired()
        {
            return new DomainMessage { TagValue = Tag.FirstNameIsRequired };
        }

        public static DomainMessage FirstNameMustNotBeMoreThan10Chars()
        {
            return new DomainMessage { TagValue = Tag.FirstNameMustNotBeMoreThan10Chars };
        }

        public static DomainMessage LastNameIsRequired()
        {
            return new DomainMessage { TagValue = Tag.LastNameIsRequired };
        }

        public static DomainMessage LastNameMustNotBeMoreThan10Chars()
        {
            return new DomainMessage { TagValue = Tag.LastNameMustNotBeMoreThan10Chars };
        }

        public static DomainMessage EmailIsRequired()
        {
            return new DomainMessage { TagValue = Tag.EmailIsRequired };
        }

        public static DomainMessage EmailMustNotBeMoreThan20Chars()
        {
            return new DomainMessage { TagValue = Tag.EmailMustNotBeMoreThan20Chars };
        }

        public static DomainMessage EmailMustContainAtSign()
        {
            return new DomainMessage { TagValue = Tag.EmailMustContainAtSign };
        }

        public static DomainMessage EmailAddressChanged(EmailAddress oldEmail, EmailAddress newEmail)
        {
            return new DomainMessage { TagValue = Tag.EmailAddressChanged, DataValue = Tuple.Create(oldEmail, newEmail) };
        }

        public static DomainMessage CustomerNotFound()
        {
            return new DomainMessage { TagValue = Tag.CustomerNotFound };
        }

        public static DomainMessage SqlCustomerIsInvalid()
        {
            return new DomainMessage { TagValue = Tag.SqlCustomerIsInvalid };
        }

        public static DomainMessage DatabaseTimeout()
        {
            return new DomainMessage { TagValue = Tag.DatabaseTimeout };
        }

        public static DomainMessage DatabaseError(string error)
        {
            return new DomainMessage { TagValue = Tag.DatabaseError, DataValue = error};
        }

        public T Match<T>(
            Func<T> customerIsRequired,
            Func<T> customerIdMustBePositive,
            Func<T> firstNameIsRequired,
            Func<T> firstNameMustNotBeMoreThan10Chars,
            Func<T> lastNameIsRequired,
            Func<T> lastNameMustNotBeMoreThan10Chars,
            Func<T> emailIsRequired,
            Func<T> emailMustNotBeMoreThan20Chars,
            Func<T> emailMustContainAtSign,
            Func<EmailAddress, EmailAddress, T> emailAddressChanged,
            Func<T> customerNotFound,
            Func<T> sqlCustomerIsInvalid,
            Func<T> databaseTimeout,
            Func<string,T> databaseError
            )
        {
            switch (TagValue)
            {
                case Tag.CustomerIsRequired:
                    return customerIsRequired();
                case Tag.CustomerIdMustBePositive:
                    return customerIdMustBePositive();
                case Tag.FirstNameIsRequired:
                    return firstNameIsRequired();
                case Tag.FirstNameMustNotBeMoreThan10Chars:
                    return firstNameMustNotBeMoreThan10Chars();
                case Tag.LastNameIsRequired:
                    return lastNameIsRequired();
                case Tag.LastNameMustNotBeMoreThan10Chars:
                    return lastNameMustNotBeMoreThan10Chars();
                case Tag.EmailIsRequired:
                    return emailIsRequired();
                case Tag.EmailMustNotBeMoreThan20Chars:
                    return emailMustNotBeMoreThan20Chars();
                case Tag.EmailMustContainAtSign:
                    return emailMustContainAtSign();
                case Tag.EmailAddressChanged:
                    var tuple = (Tuple<EmailAddress, EmailAddress>)DataValue;
                    return emailAddressChanged(tuple.Item1, tuple.Item2);
                case Tag.CustomerNotFound:
                    return customerNotFound();
                case Tag.SqlCustomerIsInvalid:
                    return sqlCustomerIsInvalid();
                case Tag.DatabaseTimeout:
                    return databaseTimeout();
                case Tag.DatabaseError:
                    var error = (string)DataValue;
                    return databaseError(error);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
