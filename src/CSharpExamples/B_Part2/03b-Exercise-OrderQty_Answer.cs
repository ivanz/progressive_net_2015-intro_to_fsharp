using NUnit.Framework;

using CSharpExamples.Utilities;

// Write a function that adds two OrderLineQtys

namespace CSharpExamples.B_Part2.OrderLineQty_Answer
{
 
    public class OrderLineQty
    {
        /// <summary>
        /// Private constructor
        /// </summary>
        private OrderLineQty(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        public static Option<OrderLineQty> Create(int qty)
        {
            if (qty < 1)
            {
                return Option.None<OrderLineQty>();
            }
            else if (qty > 99)
            {
                return Option.None<OrderLineQty>();
            }
            else
            {
                return Option.Some(new OrderLineQty(qty));
            }
        }

        public int Value { get; private set; }
    }

    [TestFixture]
    public class OrderQty_Answer
    {

        public static Option<OrderLineQty> AddOrderQty(OrderLineQty x, OrderLineQty y)
        {
            // replace void with correct return value
            var z = x.Value + y.Value;
            return OrderLineQty.Create(z);
        }

        [Test]
        public void Test()
        {
            var x = OrderLineQty.Create(98).Value;   // only use "Value" for testing! It is not safe!
            var y1 = OrderLineQty.Create(1).Value;
            var y2 = OrderLineQty.Create(3).Value;
            var goodResult = AddOrderQty(x, y1);
            var badResult = AddOrderQty(x, y2);

            // write a test to check the results
            Option.Match(goodResult,
                some => { Assert.Pass(); return 1; },  // OK
                () => { Assert.Fail(); return 0; }  // fail
                );

            Option.Match(badResult,
                some => { Assert.Fail(); return 0; },  // fail
                () => { Assert.Pass(); return 1; }  // OK
                );


        }

    }
}

