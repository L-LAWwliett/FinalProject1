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

   
    /// Inserts a new player score or updates existing one.
    /// Keeps only the higher score.
    
    public void UpsertScore(Score score)
    {
        try
        {
            var existingScore = ReadAllScores()
                .FirstOrDefault(s => s.Name == score.Name);

            // Keep only better score
            if (existingScore != null && existingScore.HighScore >= score.HighScore)
                return;

            // Rewrite updated list
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

    
    /// Returns top 10 scores.
    
    public List<Score> GetTopTenScores()
    {
        return ReadAllScores()
            .OrderByDescending(s => s.HighScore)
            .Take(10)
            .ToList();
    }

 
    /// Streams scores lazily using yield return.
    

    public IEnumerable<Score> ReadAllScores()
    {
        IEnumerable<string> lines;

        try
        {
            // Read lines before yield block
            lines = File.ReadLines(filePath).Skip(1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading scores: {ex.Message}");
            yield break;
        }

        // Iterator block (legal yield usage)
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length != 2)
                continue;

            if (int.TryParse(parts[1], out int score))
                yield return new Score(parts[0], score);
        }
    }

    
    /// Writes a list of scores to the CSV file.
  
    private void WriteAllScores(IEnumerable<Score> scores)
    {
        using StreamWriter writer = new StreamWriter(filePath);
        writer.WriteLine("Name,Score");

        foreach (var s in scores)
            writer.WriteLine($"{s.Name},{s.HighScore}");
    }
}
