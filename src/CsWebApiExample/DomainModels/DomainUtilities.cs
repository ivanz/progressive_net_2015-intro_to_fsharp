using System;
using CsWebApiExample.Utilities;

namespace CsWebApiExample.DomainModels
{
    public static class DomainUtilities
    {
        public static RopResult<String10, DomainMessage> CreateFirstName(string firstName)
        {
            var opt = String10.Create(firstName);
            return Option.Match(
                opt,
                Rop.Succeed<String10, DomainMessage>,
                () => Rop.Fail<String10, DomainMessage>(DomainMessage.FirstNameIsRequired())
                );
        }

        public static RopResult<String10, DomainMessage> CreateLastName(string lastName)
        {
            var opt = String10.Create(lastName);
            return Option.Match(
                opt,
                Rop.Succeed<String10, DomainMessage>,
                () => Rop.Fail<String10, DomainMessage>(DomainMessage.LastNameIsRequired())
                );
        }

        public static RopResult<EmailAddress, DomainMessage> CreateEmail(string email)
        {
            var opt = EmailAddress.Create(email);
            return Option.Match(
                opt,
                Rop.Succeed<EmailAddress, DomainMessage>,
                () => Rop.Fail<EmailAddress, DomainMessage>(DomainMessage.EmailIsRequired())
                );
        }

        public static RopResult<CustomerId, DomainMessage> CreateCustomerId(int customerId)
        {
            var opt = CustomerId.Create(customerId);
            return Option.Match(
                opt,
                Rop.Succeed<CustomerId, DomainMessage>,
                () => Rop.Fail<CustomerId, DomainMessage>(DomainMessage.CustomerIdMustBePositive())
                );
        }

    }
}