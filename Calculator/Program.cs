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
                    if (input[0].Equals('q') || input[0].Equals('Q'))
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
                catch (IndexOutOfRangeException)
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
            // Check if the first character is ok
            char first = input.First();
            if (first.Equals('^') || first.Equals('*') || first.Equals('/') || first.Equals(')'))
            {
                ErrorMessage($"{first} is not a valid as the first character in input!");
                return false;
            }

            // Check if the last character is ok
            char last = input.Last();
            if (last.Equals('^') || last.Equals('*') || last.Equals('/') || last.Equals(')'))
            {
                ErrorMessage($"{last} is not a valid as the last character in input!");
                return false;
            }

            // check for illegal double-assignments
            if (input.Contains("**") || input.Contains("++") || input.Contains("//"))
            {
                ErrorMessage("Input contains illegal mathematical expressions!");
                return false;
            }

            // check for each element between mathematical operators.
            String[] inputArray = input.Split(new char[] { '+', '-', '*', '/', '(', ')' , '^'});
            foreach (var item in inputArray)
            {
                double value;
                if (!double.TryParse(item, out value))
                {
                    if (item.Equals(""))
                    {
                        // If empty string it means that number is negative.
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

        /// <summary>
        /// Run the calculator with any input
        /// </summary>
        /// <param name="originalInput"></param>
        private static void runCalculator(string originalInput)
        {
            double answer = 0;

            string resolvedParantheses = resolveParanthesisBlock(originalInput);

            answer = inputFilter(resolvedParantheses);
            Console.WriteLine($"Answer is {answer}");
        }

        /// <summary>
        /// <para>
        /// Fully solves the expression within a parantheses.
        /// </para>
        /// <para>
        /// Warning: Does not handle nested parantheses.
        /// </para>
        /// </summary>
        /// <param name="input"></param>
        /// <returns>A string with the paranthesis replaced by a</returns>
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
                return inputFilter(input).ToString();
            }
        }

        /// <summary>
        /// inputFilter is a parent function of the the mathematical expression functions (add, subtract...),
        /// which uses this function recursivly to solve each expression in the correct order of operations.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static double inputFilter(string input)
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
               return double.Parse(input);      
            }
        }

        private static double add(string values)
        {
            String[] addVariables = values.Split(new char[] { '+' }, 2);
            return inputFilter(addVariables[0]) + inputFilter(addVariables[1]); 
        }

        private static double subtract(string values)
        {
            String[] subtractVariables = values.Split(new char[] { '-' }, 2);
            if (subtractVariables[0].Equals(""))
            {
                return -int.Parse(subtractVariables[1]);
            }

            return inputFilter(subtractVariables[0]) - inputFilter(subtractVariables[1]);
        }

        private static double multiply(string input)
        {
            String[] addVariables = input.Split(new char[] { '*' }, 2);
            return inputFilter(addVariables[0]) * inputFilter(addVariables[1]);
        }

        private static double divide(string input)
        {
            String[] addVariables = input.Split(new char[] { '/' }, 2);
            return inputFilter(addVariables[0]) / inputFilter(addVariables[1]);
        }

        private static double pow(string input)
        {
            String[] addVariables = input.Split(new char[] { '^' }, 2);
            return Math.Pow(inputFilter(addVariables[0]), inputFilter(addVariables[1]));
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