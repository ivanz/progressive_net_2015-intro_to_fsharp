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

    public class StackCalculator_Answer
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
                    var t1 = Pop(stack);
                    var x = t1.Item1;
                    var newStack = t1.Item2;
                    var t2 = Pop(newStack);
                    var y = t2.Item1;
                    var newStack2 = t2.Item2;


                    var newTop = x + y;
                    return Push(newTop, newStack2);
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
                    var t1 = Pop(stack);
                    var x = t1.Item1;
                    var newStack = t1.Item2;
                    var t2 = Pop(newStack);
                    var y = t2.Item1;
                    var newStack2 = t2.Item2;


                    var newTop = x * y;
                    return Push(newTop, newStack2);
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

                return stack => Push(1, stack);
            }
        }

        /// <summary>
        /// Push 2 on the stack
        /// </summary>
        public static Func<Stack, Stack> TWO
        {
            get
            {

                return stack => Push(2, stack);
            }
        }

        /// <summary>
        /// Push 3 on the stack
        /// </summary>
        public static Func<Stack, Stack> THREE
        {
            get
            {

                return stack => Push(3, stack);
            }
        }

        /// <summary>
        /// Push 4 on the stack
        /// </summary>
        public static Func<Stack, Stack> FOUR
        {
            get
            {

                return stack => Push(4, stack);
            }
        }

        /// <summary>
        /// Double the top element (use composition)
        /// </summary>
        public static Func<Stack, Stack> DOUBLE
        {
            get
            {

                return Fun.Compose(TWO, TIMES);
            }
        }

        /// <summary>
        /// Square the top element (use composition)
        /// </summary>
        public static Func<Stack, Stack> SQUARE
        {
            get
            {

                return Fun.Compose(Dup(), TIMES);
            }
        }
    }

    [TestFixture]
    public class StackCalculator_AnswerTest
    {

        [Test]
        public void Test()
        {
            StackCalculator_Answer.EMPTY()
            .Pipe(StackCalculator_Answer.THREE)
            .Pipe(StackCalculator_Answer.TWO)
            .Pipe(StackCalculator_Answer.ONE)
            .Pipe(StackCalculator_Answer.ADD)
            .Pipe(StackCalculator_Answer.PRINT)
            .Pipe(StackCalculator_Answer.TIMES)
            .Pipe(StackCalculator_Answer.PRINT)
            .Pipe(StackCalculator_Answer.DOUBLE)
            .Pipe(StackCalculator_Answer.PRINT)
            .Pipe(StackCalculator_Answer.SQUARE)
            .Pipe(StackCalculator_Answer.PRINT);

            //Should print
            //    Top=3
            //    Top=9
            //    Top=18
            //    Top=324

        }

    }

}
