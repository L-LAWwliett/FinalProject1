namespace HangGame.Game
{
    public static class WordsPool
    {
        public static List<string> GetDefaultWords() => new()
        {
            "apple", "banana", "orange", "grape", "kiwi",
            "strawberry", "pineapple", "blueberry", "peach", "watermelon"
        };
    }
}
