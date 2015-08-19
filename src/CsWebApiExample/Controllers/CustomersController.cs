using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using CsWebApiExample.DataAccessLayer;
using CsWebApiExample.DomainModels;
using CsWebApiExample.Dtos;
using CsWebApiExample.Utilities;

namespace CsWebApiExample.Controllers
{
    /// <summary>
    /// This is an example of a simple Controller for managing customers
    /// </summary>
    /// <remarks>
    /// There are two primary actions
    /// * GET retrieves a customer 
    /// * POST inserts or updates a customer
    /// </remarks>
    public class CustomersController : ApiController
    {

        readonly ICustomerDao _dao;

        /// <summary>
        /// We inject a DAO object into the object via the constructor
        /// </summary>
        public CustomersController(ICustomerDao dao)
        {
            _dao = dao;
        }

        //==============================================
        // helpers
        //==============================================

        // log values on the success path
        private Func<RopResult<T, DomainMessage>, RopResult<T, DomainMessage>> LogSuccessR<T>(string format)
        {
            return Rop.SuccessTee<T, DomainMessage>(v => Log(format, v));
        }

        // log errors on the failure path    
        private Func<RopResult<T, DomainMessage>, RopResult<T, DomainMessage>> LogFailureR<T>()
        {
            return Rop.FailureTee<T, DomainMessage>(errors =>
            {
                foreach (var err in errors)
                {
                    Log("Error: {0}", err);
                }
            });
        }

        // =================================
        // IHttpActionResults 
        // =================================
        // return OK
        private IHttpActionResult Ok(object content)
        {
            if (content == Unit.Instance)
            {
                // avoid converting unit to null!
                return new OkResult(this);
            }
            else
                return new NegotiatedContentResult<object>(HttpStatusCode.OK, content, this);
        }

        IHttpActionResult BadRequestResponse(string msg)
        {
            return new NegotiatedContentResult<string>(HttpStatusCode.BadRequest, msg, this);
        }

        IHttpActionResult NotFoundResponse(string msg)
        {
            return new NegotiatedContentResult<string>(HttpStatusCode.NotFound, msg, this);
        }

        IHttpActionResult InternalServerErrorResponse(string msg)
        {
            return new NegotiatedContentResult<string>(HttpStatusCode.InternalServerError, msg, this);
        }

        IHttpActionResult ToHttpResult(RopResult<IHttpActionResult, DomainMessage> result)
        {
            if (result.IsSuccess)
            {
                return result.SuccessValue;
            }
            else
            {
                var first = result.FailureValues[0];
                return first.Match(
                    () => this.BadRequestResponse("customerIsRequired"),
                    () => this.BadRequestResponse("customerIdMustBePositive"),
                    () => this.BadRequestResponse("firstNameIsRequired"),
                    () => this.BadRequestResponse("firstNameMustNotBeMoreThan10Chars"),
                    () => this.BadRequestResponse("lastNameIsRequired"),
                    () => this.BadRequestResponse("lastNameMustNotBeMoreThan10Chars"),
                    () => this.BadRequestResponse("emailIsRequired"),
                    () => this.BadRequestResponse("emailMustNotBeMoreThan20Chars"),
                    () => this.BadRequestResponse("emailMustContainAtSign"),
                    (oldEmail, newEmail) => this.Ok("email changed"),
                    () => this.NotFoundResponse("customerNotFound"),
                    () => this.InternalServerErrorResponse("sqlCustomerIsInvalid"),
                    () => this.InternalServerErrorResponse("databaseTimeout"),
                    (error) => this.InternalServerErrorResponse("databaseError: " + error)
                    );
            }
        }

        //==============================================
        // Get a customer, with error handling
        //==============================================

        /// <summary>
        /// Get one customer, with error handling
        /// </summary>
        /// <remarks>
        /// Extra features added:
        /// * logging
        /// * validate the id
        /// * handle customer not found
        /// * trap exceptions coming from the database
        /// </remarks>
        [Route("customers/{customerId}")]
        [HttpGet]
        public IHttpActionResult Get(int customerId)
        {
            var logSuccessR = LogSuccessR<int>("Get {0}");
            var createCustomerIdR = Rop.Bind<int, CustomerId, DomainMessage>(DomainUtilities.CreateCustomerId);
            var getByIdR = Rop.Bind<CustomerId, Customer, DomainMessage>(_dao.GetById);
            var customerToDtoR = Rop.Lift<Customer, CustomerDto, DomainMessage>(DtoConverter.CustomerToDto);
            var logFailureR = LogFailureR<CustomerDto>();
            var okR = Rop.Lift<CustomerDto, IHttpActionResult, DomainMessage>(this.Ok);

            var idR = Rop.Succeed<int, DomainMessage>(customerId);    // start with a success 
            return idR
                .Pipe(logSuccessR) // log the success branch
                .Pipe(createCustomerIdR) // convert the int into a CustomerId
                .Pipe(getByIdR) // get the Customer for that CustomerId
                .Pipe(customerToDtoR) // convert the Customer into a DTO
                .Pipe(logFailureR) // log any errors
                .Pipe(okR) // return OK on the happy path
                .Pipe(ToHttpResult);        // other errors returned as BadRequest, etc
        }

        //==============================================
        // Post a customer, with error handling
        //==============================================

        /// <summary>
        /// Upsert a customer, with error handling
        /// </summary>
        /// <remarks>
        /// Extra features added:
        /// * logging
        /// * validate the id
        /// * validate the Dto
        /// * handle case when domain Customer could not be created from the DTO
        /// * handle the EmailAddressChanged event
        /// * trap exceptions coming from the database
        /// </remarks>
        [Route("customers/{customerId}")]
        [HttpPost]
        public IHttpActionResult Post(int customerId, [FromBody] CustomerDto dto)
        {
            var logSuccessR = LogSuccessR<CustomerDto>("Post {0}");
            var dtoToCustomerR = Rop.Bind<CustomerDto, Customer, DomainMessage>(DtoConverter.DtoToCustomer);
            var upsertCustomerR = Rop.Bind<Customer, Unit, DomainMessage>(_dao.Upsert);
            var logFailureR = LogFailureR<Unit>();
            var okR = Rop.Lift<Unit, IHttpActionResult, DomainMessage>(this.Ok);

            dto.Id = customerId;           // set the DTO's CustomerId

            var dtoR = Rop.Succeed<CustomerDto, DomainMessage>(dto);    // start with a success 
            return dtoR // start with a success 
                .Pipe(logSuccessR) // log the success branch
                .Pipe(dtoToCustomerR) // convert the DTO to a Customer
                .Pipe(upsertCustomerR) // upsert the Customer
                .Pipe(logFailureR) // log any errors
                .Pipe(okR) // return OK on the happy path  
                .Pipe(ToHttpResult);                    // other errors returned as BadRequest, etc 
        }


        // =========================================
        // Debugging and helpers
        // =========================================

        /// <summary>
        /// Return an example DTO to model a POST on
        /// </summary>
        [Route("example")]
        [HttpGet]
        public IHttpActionResult GetExample()
        {
            var dto = new CustomerDto { FirstName = "Alice", LastName = "Adams", Email = "alice@example.com" };
            return this.Ok(dto);
        }

        /// <summary>
        /// Return all customers in the database
        /// </summary>
        [Route("customers/")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var results = _dao.GetAll();
            return Rop.Match(results,
                customers =>
                {
                    var dtos = customers.Select(DtoConverter.CustomerToDto);
                    return Ok(dtos);
                },
                failure => this.InternalServerErrorResponse("bad customers"));
        }

        /// <summary>
        /// Handler for the EmailAddressChanged event
        /// </summary>
        private void NotifyCustomerWhenEmailChanged(object sender, EmailAddressChangedEventArgs args)
        {
            // just log it for now.
            // a real version would put a message on a queue, for example
            Log("Email Address Changed from : {0} to {1}", args.OldAddress.Value, args.NewAddress.Value);
        }

        /// <summary>
        ///  A crude logger
        /// </summary>
        private static void Log(string format, params object[] objs)
        {
            Debug.WriteLine("[LOG]" + format, objs);
        }
    }
}
