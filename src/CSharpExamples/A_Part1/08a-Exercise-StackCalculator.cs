using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using CSharpExamples.Utilities;

/*
Write a stack based calculator

I have included functions for "pop" "push" and "dup", EMPTY amd PRINT

1) Write functions for pushing 1,2,3 on the stack -- call them ONE, TWO, THREE
2) Write functions for adding and multiplying the top two items on the stack -- call them ADD, TIMES
3) Write functions for doubling and squaring the top item on the stack -- call them DOUBLE and SQUARE - use composition to create them

I should then be able to run this code

EMPTY
| THREE
| TWO
| ONE
| ADD
| PRINT
| TIMES
| PRINT
| DOUBLE
| PRINT
| SQUARE
| PRINT

Should print
    Top=3
    Top=9
    Top=18
    Top=324
*/

namespace CSharpExamples.A_Part1
{

    public class StackCalculator_Exercise
    {
        public class Stack
        {
            public List<int> Contents;
        }

        /// <summary>
        /// return a new stack with the new value on top
        /// </summary>
        public static Stack Push(int item, Stack stack)
        {
            var newContents = new List<int>(stack.Contents);
            newContents.Insert(0, item);
            return new Stack { Contents = newContents };
        }

        /// <summary>
        /// return top value and new stack as a tuple
        /// </summary>
        public static Tuple<int, Stack> Pop(Stack stack)
        {
            if (!stack.Contents.Any())
            {
                throw new Exception("Stack underflow");
            }

            var top = stack.Contents[0];
            var newContents = new List<int>(stack.Contents);
            newContents.RemoveAt(0);
            var newStack = new Stack { Contents = newContents };
            return Tuple.Create(top, newStack);
        }

        /// <summary>
        /// duplicate the top value 
        /// </summary>
        public static Func<Stack, Stack> Dup()
        {
            return stack =>
            {
                if (!stack.Contents.Any())
                {
                    return stack;
                }

                var top = stack.Contents[0];
                return Push(top, stack);
            };
        }

        /// <summary>
        /// define an empty stack
        /// </summary>
        public static Stack EMPTY()
        {
            var contents = new List<int>();
            return new Stack { Contents = contents };
        }

        /// <summary>
        /// print the top element and return the original stack
        /// </summary>
        public static Func<Stack, Stack> PRINT
        {
            get
            {
                return stack =>
                {
                    if (!stack.Contents.Any())
                    {
                        Console.WriteLine("(empty)");
                    }
                    else
                    {
                        var top = stack.Contents[0];
                        Console.WriteLine("Top={0}", top);
                    }

                    return stack;
                };
            }
        }

        /// <summary>
        /// Add the top two elements
        /// </summary>
        public static Func<Stack, Stack> ADD
        {
            get
            {
                return stack =>
                {
                    // ??
                    return stack;
                };
            }
        }


        /// <summary>
        /// Multiply the top two elements
        /// </summary>
        public static Func<Stack, Stack> TIMES
        {
            get
            {

                return stack =>
                {
                    // ??
                    return stack;
                };
            }
        }

        /// <summary>
        /// Push 1 on the stack
        /// </summary>
        public static Func<Stack, Stack> ONE
        {
            get
            {
                // ??
                return stack => stack;
            }
        }

        /// <summary>
        /// Push 2 on the stack
        /// </summary>
        public static Func<Stack, Stack> TWO
        {
            get
            {
                // ??
                return stack => stack;
            }
        }

        /// <summary>
        /// Push 3 on the stack
        /// </summary>
        public static Func<Stack, Stack> THREE
        {
            get
            {
                // ??
                return stack => stack;
            }
        }

        /// <summary>
        /// Push 4 on the stack
        /// </summary>
        public static Func<Stack, Stack> FOUR
        {
            get
            {
                // ??
                return stack => stack;
            }
        }

        /// <summary>
        /// Double the top element (use composition)
        /// </summary>
        public static Func<Stack, Stack> DOUBLE
        {
            get
            {
                // ??
                return stack => stack;
            }
        }

        /// <summary>
        /// Square the top element (use composition)
        /// </summary>
        public static Func<Stack, Stack> SQUARE
        {
            get
            {
                // ??
                return stack => stack;
            }
        }
    }

    [TestFixture]
    public class StackCalculator_ExerciseTest
    {

        [Test]
        public void Test()
        {

            StackCalculator_Exercise.EMPTY()
            .Pipe(StackCalculator_Exercise.THREE)
            .Pipe(StackCalculator_Exercise.TWO)
            .Pipe(StackCalculator_Exercise.ONE)
            .Pipe(StackCalculator_Exercise.ADD)
            .Pipe(StackCalculator_Exercise.PRINT)
            .Pipe(StackCalculator_Exercise.TIMES)
            .Pipe(StackCalculator_Exercise.PRINT)
            .Pipe(StackCalculator_Exercise.DOUBLE)
            .Pipe(StackCalculator_Exercise.PRINT)
            .Pipe(StackCalculator_Exercise.SQUARE)
            .Pipe(StackCalculator_Exercise.PRINT);

            //Should print
            //    Top=3
            //    Top=9
            //    Top=18
            //    Top=324

        }

    }

}
