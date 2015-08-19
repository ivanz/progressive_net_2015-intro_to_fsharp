using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using CSharpExamples.Utilities;

/*
You are given a list of takeaway foods

1) Create a function "getBestFood" that uses a selection 
   strategy passed in to determine which food to get
2) Create a function "cheapestFood" that accepts two foods
   and returns -1 for the one with the lowest price.
   (e.g. it should use the built-in "compareTo" method)
3) Create a function "fastestDelivery" that accepts two foods
   and returns -1 for the one with the lowest delivery time.
   (e.g. it should use the built-in "compareTo" method)
*/

namespace CSharpExamples.A_Part1
{
    [TestFixture]
    public class Strategy
    {
        public class TakeawayFood
        {
            public string Name;
            public int Price;
            public int DeliveryTime;
        }

        private static List<TakeawayFood> _availableFoods = new List<TakeawayFood>
        {
            new TakeawayFood {Name = "Indian", Price = 20, DeliveryTime = 20},
            new TakeawayFood {Name = "Pizza", Price = 17, DeliveryTime = 15},
            new TakeawayFood {Name = "Gourmet", Price = 30, DeliveryTime = 25}
        };

        public static TakeawayFood GetBestFood(Func<TakeawayFood, TakeawayFood, int> strategy)
        {
            var comparer = Comparer<TakeawayFood>.Create((food1, food2) => strategy(food1, food2));
            // sort something
            throw new Exception();
        }

        public static Func<TakeawayFood, TakeawayFood, int> CheapestFoodStrategy()
        {
            throw new Exception();
        }

        public static Func<TakeawayFood, TakeawayFood, int> FastestDeliveryStrategy()
        {
            throw new Exception();
        }

        [Test]
        public void TestCheapestFoodStrategy()
        {
            var actual = GetBestFood(CheapestFoodStrategy()).Name;
            var expected = "Indian";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestFastestDeliveryStrategy()
        {
            var actual = GetBestFood(FastestDeliveryStrategy()).Name;
            var expected = "Pizza";
            Assert.AreEqual(expected, actual);
        }

    }

}
