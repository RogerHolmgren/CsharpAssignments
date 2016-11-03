using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    /// <summary>
    /// Your assignment is to create a basic Console-based calculator using C#. 
    /// 
    /// It should be able to handle basic mathematical operations(addition, subtraction, multiplication, 
    /// division), and be able to present the results in a consistent way.
    /// 
    /// Required Features:
    /// • The program should be able to perform basic mathematical operations(Math has methods for more 
    /// advanced operations if you include them)
    ///  ○ Addition, Subtraction, Division, Multiplication, etc…
    /// • The program should keep running until the user chooses to end it.
    /// 
    /// Optional:
    /// • Addition and Subtraction should be able to handle any number of parameters
    /// 
    /// Code Requirements:
    /// • Each mathematical operation should be in its own method
    /// • Use a loop and a menu system to keep the program running.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type your equation and press enter (q to quit).");
            bool keepRunning = true;
            while (keepRunning)
            {
                try
                {
                    Console.Write("> ");
                    string input = Console.ReadLine().Trim();
                    if (input[0].Equals('q'))
                    {
                        keepRunning = false;
                    }
                    else if (isValidInput(input))
                    {
                        runCalculator(input);
                    }
                    else
                    {
                        ErrorMessage("Invalid input, please try again or (q to quit).");
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    ErrorMessage("You have to type something, please try again or (q to quit).");
                }
            }
            Console.WriteLine("Thank you for using this calculator. Bye!");
        }

        /// <summary>
        /// Checks if the input is valid by splitting on +, -, /. *, and then checks to make 
        /// sure that all items in the array are integers.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool isValidInput(string input)
        {
            char first = input.First();
            if (first.Equals('^') || first.Equals('*') || first.Equals('/') || first.Equals(')'))
            {
                ErrorMessage($"{first} is not a valid as the first character in input!");
                return false;
            }

            String[] inputArray = input.Split(new char[] { '+', '-', '*', '/', '(', ')' , '^'});
            foreach (var item in inputArray)
            {
                int value;
                if (!int.TryParse(item, out value))
                {
                    if (item.Equals(""))
                    {
                        // If empty string it means that first number is negative.
                    }
                    else
                    {
                        ErrorMessage($"{item} is not a valid input!");
                        return false;
                    }
                }
            }
            return true;
        }

        private static void runCalculator(string originalInput)
        {
            double answer = 0;


            string resolvedParantheses = resolveParanthesisBlock(originalInput);

            answer = splitInputOnce(resolvedParantheses);
            Console.WriteLine($"Answer is {answer}");
        }

        private static string resolveParanthesisBlock(string input)
        {
            if (input.Contains("("))
            {
                int start = input.IndexOf("(")+1;
                int end = input.IndexOf(")", start);
                string paranthesisBlock = input.Substring(start-1, end - start+2 );
                string paranthesisContent = input.Substring(start, end - start);
                return input.Replace(paranthesisBlock, resolveParanthesisBlock(paranthesisContent));  
            }
            else
            {
                return splitInputOnce(input).ToString();
            }
        }

        /// <summary>
        /// Sorts the inputs on the four aritmethic or if no aritmetic exists in the string returns the content as int.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static double splitInputOnce(string input)
        {
            if (input.Contains("+"))
            {
                return add(input);
            }
            else if (input.Contains("-"))
            {
                return subtract(input);
            }
            else if (input.Contains("*"))
            {
                return multiply(input);
            }
            else if (input.Contains("/"))
            {
                return divide(input);
            }
            else if (input.Contains("^"))
            {
                return pow(input);
            }
            else
            {
               return int.Parse(input);      
            }
        }

        private static double add(string values)
        {
            String[] addVariables = values.Split(new char[] { '+' }, 2);
            return splitInputOnce(addVariables[0]) + splitInputOnce(addVariables[1]); 
        }

        private static double subtract(string values)
        {
            String[] subtractVariables = values.Split(new char[] { '-' }, 2);
            if (subtractVariables[0].Equals(""))
            {
                return -int.Parse(subtractVariables[1]);
            }

            return splitInputOnce(subtractVariables[0]) - splitInputOnce(subtractVariables[1]);
        }

        private static double multiply(string input)
        {
            String[] addVariables = input.Split(new char[] { '*' }, 2);
            return splitInputOnce(addVariables[0]) * splitInputOnce(addVariables[1]);
        }

        private static double divide(string input)
        {
            String[] addVariables = input.Split(new char[] { '/' }, 2);
            return splitInputOnce(addVariables[0]) / splitInputOnce(addVariables[1]);
        }

        private static double pow(string input)
        {
            String[] addVariables = input.Split(new char[] { '^' }, 2);
            return Math.Pow(splitInputOnce(addVariables[0]), splitInputOnce(addVariables[1]));
        }


        /// <summary>
        /// Prints to console in red letters without affecting the color of future output.
        /// </summary>
        /// <param name="errorMessage"></param>
        private static void ErrorMessage(string errorMessage)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ForegroundColor = previousColor;
        }
    }


}