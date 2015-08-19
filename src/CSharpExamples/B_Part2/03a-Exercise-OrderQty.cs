using NUnit.Framework;

using CSharpExamples.Utilities;

// Write a function that adds two OrderLineQtys

namespace CSharpExamples.B_Part2.OrderLineQty_Exercise
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

        public static int AddOrderQty(OrderLineQty x,OrderLineQty y)
        {
            // replace void with correct return value
            return 0;
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
        }

    }
}

