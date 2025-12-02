using NumberGuessingGame;
using System;

public class Game
{
    private readonly int min;
    private readonly int max;
    
    private readonly int secretNumber;
    private const int maxAttempts = 10;

    public Game(string name, int min, int max)
    {
        
        this.min = min;
        this.max = max;

        Random random = new Random();
        secretNumber = random.Next(min, max + 1);
    }

    public int Start()
    {
        Console.WriteLine($"\nGuess the number ({min}-{max})! You have {maxAttempts} attempts.");

        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            int guess = InputValidator.SafeInput($"Attempt {attempt}: ", min, max);

            if (guess == secretNumber)
            {
                Console.WriteLine(" Correct! You win!");
                return (maxAttempts - attempt + 1) * 10; 
            }

            Console.WriteLine(guess > secretNumber ? "Too high!" : "Too low!");
        }

        Console.WriteLine($"\nYou lost! The number was {secretNumber}.");
        return 0;
    }
}
