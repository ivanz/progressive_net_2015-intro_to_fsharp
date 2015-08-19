using System;
using System.Collections.Generic;

namespace CSharpExamples.IsCSharpPredictable
{
    class QaEquality
    {
        /// <summary>
        /// Rule 1) Objects with the same values should be equal by default.
        /// </summary>
        public void TestEqualityBetweenCustomers()
        {
            var cust1 = new Customer(99, "J Smith");
            var cust2 = new Customer(99, "J Smith");
            var areEqual = (cust1 == cust2);
        }

        /// <summary>
        /// Rule 2) Comparing objects of different types is a compile-time error.
        /// </summary>
        public void TestEqualityBetweenCustomerAndOrder()
        {
            var cust = new Customer(99, "J Smith");
            var order = new Customer(99, "J Smith");
            var areEqual = cust.Equals(order);
        }

        /// <summary>
        /// Rule 3) Objects must be initialized to a valid state. Not doing so is a compile-time error.
        /// </summary>
        public void TestObjectsMustBeInitializedToAValidState()
        {
            // create a customer
            var cust = new Customer();

            // what is the expected output?
            Console.WriteLine(cust.Address.Country);
        }

        /// <summary>
        /// Rule 4) Once created,  objects and collections must be immutable (by default)
        /// </summary>
        public void TestImmutability1()
        {
            // create a customer
            var cust = new Customer(99, "J Smith");

            // add it to a set
            var processedCustomers = new HashSet<Customer>();
            processedCustomers.Add(cust);

            // process it
            this.ProcessCustomer(cust);

            // true or false?
            processedCustomers.Contains(cust);

        }

        /// <summary>
        /// Rule 4) Once created,  objects and collections must be immutable (by default)
        /// </summary>
        public void TestImmutability2()
        {
            // create a customer
            var cust = new Customer(99, "J Smith");

            // add it to a set
            var processedCustomers = new HashSet<Customer>();
            processedCustomers.Add(cust);

            // process it and return the changes
            var changedCustomer = this.ProcessCustomer(cust);

            // true or false?
            processedCustomers.Contains(cust);

        }


        private Customer ProcessCustomer(Customer cust)
        {
            return new Customer(cust.Id, "new name");
        }

        /// <summary>
        /// Rule 5a) Missing data or errors must be made explicit.
        /// </summary>
        public void TestThatErrorsAreExplicit()
        {
            // create a repository
            var repo = new CustomerRepository();

            // find a customer by id
            var customer = repo.GetById(42);

            // what is the expected output?
            Console.WriteLine(customer.Id);

        }

        /// <summary>
        /// Rule 5a) Missing data or errors must be made explicit.
        /// </summary>
        public void TestThatErrorsAreExplicit2()
        {
            // create a repository
            var repo = new CustomerRepository();

            // find a customer by id
            var customerOrError = repo.GetByIdWIthError(42);

            // handle both cases
            if (customerOrError.IsCustomer)
                Console.WriteLine(customerOrError.Customer.Id);

            if (customerOrError.IsError)
                Console.WriteLine(customerOrError.ErrorMessage);

        }

    }
}
