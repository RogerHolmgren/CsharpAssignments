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
            Console.Write("Type your equation and press enter (q to quit).\n> ");
            bool keepRunning = true;
            while (keepRunning)
            {
                string input = Console.ReadLine().Trim();
                if (input.Length == 1)
                {
                    keepRunning = !input[0].Equals('q');
                }
                else if (true)
                {
                    runCalculator(input);
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("Invalid input, please try again.\n> ");
                }
            }
            Console.WriteLine("Thank you for using this calculator. Bye!");
        }

        private static void runCalculator(string input)
        {
            String[] numbersArray = input.Split('*');
            foreach (var item in numbersArray)
            {
                Console.WriteLine(item);
            }
        }

        private static int add(int addend1, int addend2)
        {
            return addend1 + addend2; // sum
        }

        private static int subtract(int minuend, int subtrahend)
        {
            return minuend - subtrahend; //difference
        }

        private static int multiply(int multiplicand, int multiplier)
        {
            return multiplicand * multiplier;
        }

        private static int divide(int dividend, int divisor)
        {
            return dividend; // quotient  // remainder, int or double?
        }
    }


}
