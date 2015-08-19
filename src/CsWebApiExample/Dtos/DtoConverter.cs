using CsWebApiExample.DomainModels;
using CsWebApiExample.Utilities;

namespace CsWebApiExample.Dtos
{
    public static class DtoConverter
    {

        /// <summary>
        /// Create a domain customer from a DTO or null if not valid.
        /// </summary>
        public static RopResult<Customer,DomainMessage> DtoToCustomer(CustomerDto dto)
        {
            if (dto == null)
            {
                // dto can be null if deserialization fails
                return Rop.Fail<Customer,DomainMessage>(DomainMessage.CustomerIsRequired());
            }

            var firstName = DomainUtilities.CreateFirstName(dto.FirstName);
            var lastName = DomainUtilities.CreateLastName(dto.LastName);
            var createName = Rop.Lift2<String10, String10, PersonalName, DomainMessage>(PersonalName.Create);
            var name = createName(firstName, lastName);

            var id = DomainUtilities.CreateCustomerId(dto.Id);
            var email = DomainUtilities.CreateEmail(dto.Email);
            var createCust = Rop.Lift3<CustomerId, PersonalName, EmailAddress, Customer, DomainMessage>(Customer.Create);
            var cust = createCust(id, name, email);
            return cust;
        }


        /// <summary>
        /// Create a DTO from a domain customer or null if the customer is null
        /// </summary>
        public static CustomerDto CustomerToDto(Customer customer)
        {
            // we should never try to convert a null customer
            if (customer == null)
            {
                return null;
            }

            return new CustomerDto
            {
                Id = customer.Id.Value,
                FirstName = customer.Name.First.Value,
                LastName = customer.Name.Last.Value,
                Email = customer.EmailAddress.Value
            };
        }

    }
}
