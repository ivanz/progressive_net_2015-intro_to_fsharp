namespace CSharpExamples.IsCSharpPredictable
{
    class CustomerRepository
    {
        public Customer GetById(int id)
        {
            switch (id)
            {
                case 0: return null;
                default:
                    return new Customer(id, "J Smith");
            }
        }

        public CustomerOrError GetByIdWIthError(int id)
        {
            switch (id)
            {
                case 0:
                    return new CustomerOrError {ErrorMessage = "missing"};
                default:
                    var cust = new Customer(id, "J Smith");
                    return new CustomerOrError { Customer = cust };
            }
        }

        internal class CustomerOrError
        {
            public Customer Customer { get; set; }
            public string ErrorMessage { get; set; }

            public bool IsCustomer
            {
                get { return this.Customer != null; }

            }

            public bool IsError
            {
                get { return !this.IsCustomer; }

            }
        }
    }
}