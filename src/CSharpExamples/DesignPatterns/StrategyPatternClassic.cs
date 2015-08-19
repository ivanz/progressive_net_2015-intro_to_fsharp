using System;

namespace CSharpExamples.DesignPatterns
{
    interface ICalculationStrategy
    {
        int Calculate(int input);
    }

    class ClassicCalculator
    {
        private readonly ICalculationStrategy _strategy;

        public ClassicCalculator(ICalculationStrategy strategy)
        {
            this._strategy = strategy;
        }

        public int ApplyStrategy(int input)
        {
            var output = _strategy.Calculate(input);
            Console.WriteLine("Input={0}; Output={1}",input,output);
            return output;
        }
    }

    class Add1Strategy : ICalculationStrategy
    {
        public int Calculate(int input)
        {
            return input + 1;
        }
    }

    class Times2Strategy : ICalculationStrategy
    {
        public int Calculate(int input)
        {
            return input * 1;
        }
    }

}
