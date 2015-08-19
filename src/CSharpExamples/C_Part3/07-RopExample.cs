using System;
using CsWebApiExample.Utilities;
using NUnit.Framework;

using CSharpExamples.Utilities;

/*

Railway oriented programming

If some functions give bad results, 
how can you connect them together?

*/


namespace CSharpExamples.C_Part3.RopExample
{
    public class SimpleRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    [TestFixture]
    public class RopTest
    {
        private static RopResult<SimpleRequest, string> NameNotBlank(SimpleRequest input)
        {
            if (input.Name == "")
                return Rop.Fail<SimpleRequest, string>("Name must not be blank");
            else
                return Rop.Succeed<SimpleRequest, string>(input);
        }

        private static RopResult<SimpleRequest, string> Name50(SimpleRequest input)
        {
            if (input.Name.Length > 50)
                return Rop.Fail<SimpleRequest, string>("Name must not be longer than 50 chars");
            else
                return Rop.Succeed<SimpleRequest, string>(input);
        }

        private static RopResult<SimpleRequest, string> EmailNotBlank(SimpleRequest input)
        {
            if (input.Email == "")
                return Rop.Fail<SimpleRequest, string>("Email must not be blank");
            else
                return Rop.Succeed<SimpleRequest, string>(input);
        }

        private static RopResult<SimpleRequest, string> ValidateRequest(SimpleRequest input)
        {
            var name50R = Rop.Bind<SimpleRequest, SimpleRequest, string>(Name50);
            var emailNotBlankR = Rop.Bind<SimpleRequest, SimpleRequest, string>(EmailNotBlank);

            return input
                .Pipe(NameNotBlank)  // the first one does not need to be bound
                .Pipe(name50R)
                .Pipe(emailNotBlankR);

        }

        public static SimpleRequest BadRequest = new SimpleRequest { UserId = 0, Name = "", Email = "ABC@gmail.COM" };
        public static SimpleRequest GoodRequest = new SimpleRequest { UserId = 0, Name = "Alice", Email = "ABC@gmail.COM" };

        [Test]
        public void TestValidateBad()
        {
            var badRequest = BadRequest;
            var result = badRequest.Pipe(ValidateRequest);
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void TestValidateGood()
        {
            var goodRequest = GoodRequest;
            var result = goodRequest.Pipe(ValidateRequest);
            Assert.IsTrue(result.IsSuccess);
        }

        // ------------------------
        // Add another step
        // ------------------------

        // trim spaces and lowercase
        public static SimpleRequest CanonicalizeEmail(SimpleRequest input)
        {
            return new SimpleRequest { UserId = input.UserId, Name = input.Name, Email = input.Email.Trim().ToLower() };
        }

        [Test]
        public void TestCanonicaliseGood()
        {
            var canonicalizeEmailR = Rop.Lift<SimpleRequest, SimpleRequest, string>(CanonicalizeEmail);

            var goodRequest = GoodRequest;
            var result = goodRequest
                .Pipe(ValidateRequest)
                .Pipe(canonicalizeEmailR);
            Assert.IsTrue(result.IsSuccess);
        }


        // ------------------------
        // Update the database
        // ------------------------


        public static void UpdateDatabase(SimpleRequest input)
        {
            // do something
            // return nothing at all
            Console.WriteLine("Database updated with userId={0} email={1}", input.UserId, input.Email);
        }

        [Test]
        public void TestUpdateDbGood()
        {
            var canonicalizeEmailR = Rop.Lift<SimpleRequest, SimpleRequest, string>(CanonicalizeEmail);
            var updateDbR = Rop.SuccessTee<SimpleRequest, string>(input => UpdateDatabase(input));

            var goodRequest = GoodRequest;
            var result = goodRequest
                .Pipe(ValidateRequest)
                .Pipe(canonicalizeEmailR)
                .Pipe(updateDbR);
            Assert.IsTrue(result.IsSuccess);
        }


        // ------------------------
        // Send an email
        // ------------------------

        public static SimpleRequest SendEmail(SimpleRequest input)
        {
            if (input.Email.EndsWith("example.com"))
                throw new Exception(string.Format("Can't send email to {0}", input.Email));
            else
                Console.WriteLine("Sending email={0}", input.Email);
            return input; // return request for processing by next step        }
        }

    }

}

