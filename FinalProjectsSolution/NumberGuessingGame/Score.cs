public class Score
{
    public string Name { get; set; }
    public int HighScore { get; set; }

    public Score(string name, int highScore)
    {
        Name = name;
        HighScore = highScore;
    }
}
