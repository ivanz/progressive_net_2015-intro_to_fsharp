using System;
using System.Collections.Generic;
using System.Linq;
using CsWebApiExample.DomainModels;
using CsWebApiExample.SqlDatabase;
using CsWebApiExample.Utilities;

namespace CsWebApiExample.DataAccessLayer
{
    /// <summary>
    /// This is a data access wrapper around a SQL database
    /// </summary>
    public class CustomerDao : ICustomerDao
    {
        /// <summary>
        /// Return all customers
        /// </summary>
        public RopResult<IEnumerable<Customer>, DomainMessage> GetAll()
        {
            var db = new DbContext();
            var ropResults = db.Customers().Select(FromDbCustomer);
            if (ropResults.Any(r => !r.IsSuccess))
            {
                return Rop.Fail<IEnumerable<Customer>, DomainMessage>(DomainMessage.SqlCustomerIsInvalid());
            }
            else
            {
                var goodResults = ropResults.Select(r => r.SuccessValue);
                return Rop.Succeed<IEnumerable<Customer>, DomainMessage>(goodResults);
            }
        }

        /// <summary>
        /// Return the customer with the given CustomerId, or null if not found
        /// </summary>
        public RopResult<Customer, DomainMessage> GetById(CustomerId id)
        {
            var db = new DbContext();

            // Note that this code does not trap exceptions coming from the database. What would it do with them?
            // Compare with the F# version, where errors are returned along with the Customer
            var result = db.Customers().Where(c => c.Id == id.Value).Select(FromDbCustomer).FirstOrDefault();
            if (result == null)
            {
                return Rop.Fail<Customer, DomainMessage>(DomainMessage.CustomerNotFound());
            }

            return result;
        }

        /// <summary>
        /// Insert/update the customer 
        /// If it already exists, update it, otherwise insert it.
        /// If the email address has changed, raise a EmailAddressChanged event on DomainEvents
        /// </summary>
        public RopResult<Unit, DomainMessage> Upsert(Customer customer)
        {
            if (customer == null) { throw new ArgumentNullException("customer"); }

            var db = new DbContext();
            var existingDbCust = GetById(customer.Id);
            var newDbCust = ToDbCustomer(customer);
            return Rop.Match(existingDbCust,
                success =>
                {
                    // update
                    db.Update(newDbCust);

                    // check for changed email
                    if (!customer.EmailAddress.Equals(success.EmailAddress))
                    {
                        // return a Success with the extra event
                        var msg = DomainMessage.EmailAddressChanged(success.EmailAddress, customer.EmailAddress);
                        return Rop.SucceedWithMsg<Unit, DomainMessage>(Unit.Instance, msg);
                    }
                    else
                    {
                        // return a Success with no extra event
                        return Rop.Succeed<Unit, DomainMessage>(Unit.Instance);
                    }
                },
                failure =>
                {
                    // insert
                    db.Insert(newDbCust);

                    // return a Success
                    return Rop.Succeed<Unit, DomainMessage>(Unit.Instance);
                });
        }


        /// <summary>
        /// Convert a DbCustomer into a domain Customer
        /// </summary>
        public static RopResult<Customer, DomainMessage> FromDbCustomer(DbCustomer sqlCustomer)
        {
            if (sqlCustomer == null)
            {
                return Rop.Fail<Customer, DomainMessage>(DomainMessage.CustomerNotFound());
            }

            var firstName = DomainUtilities.CreateFirstName(sqlCustomer.FirstName);
            var lastName = DomainUtilities.CreateLastName(sqlCustomer.LastName);
            var createName = Rop.Lift2<String10, String10, PersonalName, DomainMessage>(PersonalName.Create);
            var name = createName(firstName, lastName);

            var id = DomainUtilities.CreateCustomerId(sqlCustomer.Id);
            var email = DomainUtilities.CreateEmail(sqlCustomer.Email);
            var createCust = Rop.Lift3<CustomerId, PersonalName, EmailAddress, Customer, DomainMessage>(Customer.Create);
            var cust = createCust(id, name, email);
            return cust;
        }


        /// <summary>
        /// Convert a domain Customer into a DbCustomer
        /// </summary>
        public static DbCustomer ToDbCustomer(Customer customer)
        {
            return new DbCustomer
            {
                Id = customer.Id.Value,
                FirstName = customer.Name.First.Value,
                LastName = customer.Name.Last.Value,
                Email = customer.EmailAddress.Value
            };
        }
    }
}
