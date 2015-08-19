namespace CsWebApiExample.DomainModels
{
    /// <summary>
    /// Represents a Customer in the domain. 
    /// </summary>
    public class Customer
    {
        // private constructor to force use of static
        private Customer(CustomerId id, PersonalName name, EmailAddress email)
        {
            Id = id;
            Name = name;
            EmailAddress = email;
        }

        /// <summary>
        /// Create a new customer from the parameters. If not valid, return null
        /// </summary>
        public static Customer Create(CustomerId id, PersonalName name, EmailAddress email)
        {
            if (id == null) { return null; }
            if (name == null) { return null; }
            if (email == null) { return null; }

            // Compare this with the F# version, where the domain object
            // doesn't need to check for nulls

            return new Customer(id, name, email);
        }


        public CustomerId Id { get; private set; }
        public PersonalName Name { get; private set; }
        public EmailAddress EmailAddress { get; private set; }
    }
}
