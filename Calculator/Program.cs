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
                Console.Write("> ");
                string input = Console.ReadLine().Trim();
                if (input[0].Equals('q'))
                {
                    keepRunning = false;
                }
                else if (isValidInput(input))
                {
                    runCalculator(input);
                    Console.Write("> ");
                }
                else
                {
                    ErrorMessage("Invalid input, please try again or (q to quit).");
                }
            }
            Console.WriteLine("Thank you for using this calculator. Bye!");
        }



        private static bool isValidInput(string input)
        {
            String[] inputArray = input.Split(new char[] { '+', '-', '*', '/' });
            foreach (var item in inputArray)
            {
                int value;
                if (!int.TryParse(item, out value))
                {
                    if (item.Equals("") && input.First().Equals('-'))
                    {
                        // If empty string and leading '-' let it slide.
                        // fixes when first number is negative.
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

        private static void runCalculator(string input)
        {
            int answer = add(input);
            Console.WriteLine($"Answer is {answer}");
            
        }

        private static int add(string input)
        {
            if (input.Contains("+"))
            {
                String[] addVariables = input.Split(new char[] { '+' }, 2);
                return add(addVariables[0]) + add(addVariables[1]); // sum
            }
            else if (input.Contains("-"))
            {
                String[] subtractVariables = input.Split(new char[] { '-' }, 2);
                if (subtractVariables[0].Equals(""))
                {
                    return -int.Parse(subtractVariables[1]);
                }

                return add(subtractVariables[0]) - add(subtractVariables[1]);
            }
            else
            {
               return int.Parse(input);      
            }
        }





        private static void ErrorMessage(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ForegroundColor = ConsoleColor.Gray;
        }




        /*
        private static int subtract(string values)
        {
            String[] subtractVariables = values.Split(new char[] { '-' }, 2);
            return add(subtractVariables[0]) - add(subtractVariables[1]);
        }

        private static int multiply(int multiplicand, int multiplier)
        {
            return multiplicand * multiplier;
        }

        private static int divide(int dividend, int divisor)
        {
            return dividend; // quotient  // remainder, int or double?
        }
        */
    }


}
