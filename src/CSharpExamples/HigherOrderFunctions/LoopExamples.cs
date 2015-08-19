namespace CSharpExamples.HigherOrderFunctions
{
    static class LoopExamples
    {
        public static int Product(int n)
        {
            int product = 1;
            for (int i = 1; i <= n; i++)
            {
                product *= i;
            }
            return product;
        }

        public static int SumOfOdds(int n)
        {
            int sum = 0;
            for (int i = 1; i <= n; i++)
            {
                if (i % 2 != 0) { sum += i; }
            }
            return sum;
        }

        public static int AlternatingSum(int n)
        {
            int sum = 0;
            bool isNeg = true;
            for (int i = 1; i <= n; i++)
            {
                if (isNeg)
                {
                    sum -= i;
                    isNeg = false;
                }
                else
                {
                    sum += i;
                    isNeg = true;
                }
            }
            return sum;
        }
    }
}
