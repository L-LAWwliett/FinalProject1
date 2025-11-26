using System;

namespace NumberGuessingGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Number Guessing Game";

            ScoreManager scoreManager = new ScoreManager("scores.csv");

            Console.WriteLine("=== NUMBER GUESSING GAME ===\n");

            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(playerName))
            {
                Console.Write("Name cannot be empty. Enter again: ");
                playerName = Console.ReadLine();
            }

            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== NUMBER GUESSING GAME ===");
                Console.WriteLine("1. Play Game");
                Console.WriteLine("2. Show TOP 10 Scores");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        PlayGame(playerName, scoreManager);
                        break;

                    case "2":
                        ShowTopTenScores(scoreManager);
                        break;

                    case "3":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option! Try again.");
                        Console.ReadKey();
                        break;
                }
            }

            Console.WriteLine("Goodbye!");
        }

        private static void PlayGame(string playerName, ScoreManager scoreManager)
        {
            Console.Clear();
            Console.WriteLine("Choose difficulty:");
            Console.WriteLine("1. Easy (1–15)");
            Console.WriteLine("2. Medium (1–25)");
            Console.WriteLine("3. Hard (1–50)");

            int choice = InputValidator.SafeInput("Select: ", 1, 3);
            Game game = choice switch
            {
                1 => new Game(playerName, 1, 15),
                2 => new Game(playerName, 1, 25),
                _ => new Game(playerName, 1, 50),
            };
            int score = game.Start();

            if (score > 0)
            {
                scoreManager.UpsertScore(new Score(playerName, score));
                Console.WriteLine($" Congratulations! Your score: {score}");
            }
            else
            {
                Console.WriteLine("Better luck next time :')");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void ShowTopTenScores(ScoreManager scoreManager)
        {
            Console.Clear();
            Console.WriteLine("=== TOP 10 PLAYERS ===\n");

            var scores = scoreManager.GetTopTenScores();

            if (scores.Count == 0)
            {
                Console.WriteLine("No scores recorded yet.");
            }
            else
            {
                int index = 1;
                foreach (var s in scores)
                {
                    Console.WriteLine($"{index}. {s.Name} — {s.HighScore}");
                    index++;
                }
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }
    }
}
