using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Data;

using CSharpExamples.Utilities;

/*
Given 
a) a function that gets data from a database
b) a function that gets data from an in-memory list
 
Create versions of these two functions that have the SAME abstract "interface" 
so that either can passed to a client who does not care about the implementation.
*/

namespace CSharpExamples.A_Part1
{


    [TestFixture]
    public class DependencyInjection_Answer
    {

        public class CustomerId
        {
            public CustomerId(int id)
            {
                this.Id = id;
            }

            public int Id { get; private set; }
        }

        public class Customer
        {
            public Customer(CustomerId id, string name)
            {
                this.Id = id;
                this.Name = name;
            }
            public CustomerId Id { get; private set; }
            public string Name { get; private set; }
        }

        /// <summary>
        /// Storage in database
        /// </summary>
        public static Func<IDbConnection, CustomerId, Customer> GetCustomerFromDatabase()
        {
            return (connection, id) =>
            {
                // from connection    
                // select customer   
                // where customerId = customerId 
                return new Customer(id, "Alice"); // dummy 
            };

        }

        /// <summary>
        /// Storage in memory
        /// </summary>
        public static Func<List<Customer>, CustomerId, Customer> GetCustomerFromMemory()
        {
            return (connection, id) =>
            {
                // find in list of customers
                return new Customer(id, "Alice");  // dummy 
            };

        }

        /// <summary>
        /// Client with dependency injection
        /// </summary>
        public static Customer Client(Func<CustomerId, Customer> customerRepository)
        {
            var id = new CustomerId(1);
            return customerRepository(id);
        }

        [Test]
        public void TestDatabase()
        {
            IDbConnection connection = null;
            var customerRepository = GetCustomerFromDatabase().Apply(connection);

            var customer = Client(customerRepository);

            var actual = customer.Name; 
            var expected = "Alice";
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void TestInMemory()
        {
            List<Customer> memoryList = null;
            var customerRepository = GetCustomerFromMemory().Apply(memoryList);

            var customer = Client(customerRepository);

            var actual = customer.Name;
            var expected = "Alice";
            Assert.AreEqual(expected, actual);
        }

    }

}
