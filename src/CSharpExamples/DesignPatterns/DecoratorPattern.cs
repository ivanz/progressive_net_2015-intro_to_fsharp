using System;

namespace CSharpExamples.DesignPatterns
{
    interface IDoSomething
    {
        int Add1(int input);
    }

    /// <summary>
    /// A basic DoSomething without logging
    /// </summary>
    class BasicDoSomething : IDoSomething
    {
        public int Add1(int input)
        {
            return input + 1;
        }
    }

    /// <summary>
    /// Takes a DoSomething and decorates it by adding logging
    /// </summary>
    class LoggedDoSomething : IDoSomething
    {
        private readonly IDoSomething _innerDoSomething;

        public LoggedDoSomething(IDoSomething innerDoSomething)
        {
            this._innerDoSomething = innerDoSomething;
        }

        public int Add1(int input)
        {
            var output = this._innerDoSomething.Add1(input);
            Console.WriteLine("Input={0}; Output={1}", input, output);
            return output;
        }
    }

}
