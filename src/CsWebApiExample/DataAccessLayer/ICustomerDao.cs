using System.Collections.Generic;
using CsWebApiExample.DomainModels;
using CsWebApiExample.Utilities;

namespace CsWebApiExample.DataAccessLayer
{
    /// <summary>
    /// This is a data access wrapper around a SQL database
    /// </summary>
    public interface ICustomerDao
    {
        /// <summary>
        /// Return all customers
        /// </summary>
        RopResult<IEnumerable<Customer>,DomainMessage> GetAll();

        /// <summary>
        /// Return the customer with the given CustomerId, or null if not found
        /// </summary>
        RopResult<Customer,DomainMessage> GetById(CustomerId id);

        /// <summary>
        /// Insert/update the customer 
        /// If it already exists, update it, otherwise insert it.
        /// If the email address has changed, raise a EmailAddressChanged event on DomainEvents
        /// </summary>
        RopResult<Unit,DomainMessage> Upsert(Customer customer);
    }
}