namespace Calculator
{
    using Calculator.Services;
    

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Simple Calculator");
            while (true)
            {
                try
                {
                    PerformCalculation();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                Console.WriteLine("Do you want to perform another calculation? (y/n): ");
                string? cont = Console.ReadLine();
                if (cont?.ToLower() != "y")
                {
                    break;
                }
            }
        }

        private static void PerformCalculation()
        {
            double num1 = Input.GetInput("Enter first number: ");
            double num2 = Input.GetInput("Enter second number: ");
            string operation = Input.GetOperation();
            double result = operation switch
            {
                "+" => Calculator.Add(num1, num2),
                "-" => Calculator.Subtract(num1, num2),
                "*" => Calculator.Multiply(num1, num2),
                "/" => Calculator.Divide(num1, num2),
                _ => throw new InvalidOperationException("Invalid operation.")
            };
            Console.WriteLine($"Result: {result}");
        }
    }
}