using System;

public static class InputValidator
{
    public static int SafeInput(string message, int min, int max)
    {
        while (true)
        {
            try
            {
                Console.Write(message);
                int value = Convert.ToInt32(Console.ReadLine());

                if (value < min || value > max)
                    throw new Exception($"Number must be between {min} and {max}!");

                return value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid input: {ex.Message}");
            }
        }
    }
}
