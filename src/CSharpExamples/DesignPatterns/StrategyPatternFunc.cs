using System;

namespace CSharpExamples.DesignPatterns
{
    
    class FuncCalculator
    {
        private readonly Func<int, int> _strategy;

        public FuncCalculator(Func<int, int> strategy)
        {
            this._strategy = strategy;
        }

        public int ApplyStrategy(int input)
        {
            var output = _strategy(input);
            Console.WriteLine("Input={0}; Output={1}", input, output);
            return output;
        }
    }

    static class StrategyFactory
    {
        public static Func<int, int> Add1Strategy()
        {
            return input => input + 1;
        }

        public static Func<int, int> Times2Strategy()
        {
            return input => input * 1;
        }
    }

}
