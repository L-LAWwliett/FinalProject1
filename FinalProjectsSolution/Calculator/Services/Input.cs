using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services
{
    public  static class Input
    {
        public static double GetInput(string prompt)
        {
            double result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (double.TryParse(input, out result))
                {
                    return result;
                }
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        public static string GetOperation()
        {
            while (true)
            {
                Console.Write("Enter operation (+, -, *, /): ");
                string operation = Console.ReadLine();
                if (operation == "+" || operation == "-" || operation == "*" || operation == "/")
                {
                    return operation;
                }
                Console.WriteLine("Invalid operation. Please enter one of these: +, -, *, /.");
            }
        }
    }
}
