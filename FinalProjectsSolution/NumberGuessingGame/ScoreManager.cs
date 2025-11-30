using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ScoreManager
{
    private readonly string filePath;

    public ScoreManager(string filePath)
    {
        this.filePath = filePath;

        if (!File.Exists(filePath))
            File.WriteAllText(filePath, "Name,Score\n");
    }


    /// შეაქ ახალი მოთამაშის ქულა. ან ანახლებს არსებულს.
    /// იტოვებს მხოლოდ უკეთეს ქულას.

    public void UpsertScore(Score score)
    {
        try
        {
            var existingScore = ReadAllScores()
                .FirstOrDefault(s => s.Name == score.Name);

            //თუკი არსებული ქულა უკეთესია, არ აკეთებს არაფერს
            if (existingScore != null && existingScore.HighScore >= score.HighScore)
                return;

            // განახლებული ლისტის დამატება
            var allScores = ReadAllScores()
                .Where(s => s.Name != score.Name)
                .Append(score)
                .ToList();

            WriteAllScores(allScores);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving score: {ex.Message}");
        }
    }


    /// აბრუნებს საუკეთესო ათ ქულას.

    public List<Score> GetTopTenScores()
    {
        return ReadAllScores()
            .OrderByDescending(s => s.HighScore)
            .Take(10)
            .ToList();
    }


    // From CSV file.


    public IEnumerable<Score> ReadAllScores()
    {
        IEnumerable<string> lines;

        try
        {
            
            lines = File.ReadLines(filePath).Skip(1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading scores: {ex.Message}");
            yield break;
        }

       
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length != 2)
                continue;

            if (int.TryParse(parts[1], out int score))
                yield return new Score(parts[0], score);
        }
    }


    /// list to CSV file.

    private void WriteAllScores(IEnumerable<Score> scores)
    {
        using StreamWriter writer = new StreamWriter(filePath);
        writer.WriteLine("Name,Score");

        foreach (var s in scores)
            writer.WriteLine($"{s.Name},{s.HighScore}");
    }
}
