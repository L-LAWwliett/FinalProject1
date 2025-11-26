namespace HangGame.Game
{
    public class HangmanGame
    {
        private readonly string _word;
        private readonly HashSet<char> _revealed;
        private readonly HashSet<char> _guessedLetters;

        public int MaxLetterGuesses { get; } = 6;

        public HangmanGame(string word)
        {
            if (word == null)
                throw new ArgumentNullException(nameof(word));

            _word = word;
            _revealed = new HashSet<char>();
            _guessedLetters = new HashSet<char>();
        }

       
        public string MaskedWord
        {
            get
            {
                string result = "";

                for (int i = 0; i < _word.Length; i++)
                {
                    char c = _word[i];

                    if (_revealed.Contains(c))
                        result += c;
                    else
                        result += "_";
                }

                return result;
            }
        }

       
        public bool IsFullyRevealed
        {
            get
            {
                for (int i = 0; i < _word.Length; i++)
                {
                    if (!_revealed.Contains(_word[i]))
                        return false;
                }
                return true;
            }
        }

        
        public int WrongUniqueGuesses
        {
            get
            {
                int count = 0;

                foreach (char guessed in _guessedLetters)
                {
                    bool found = false;

                   
                    for (int i = 0; i < _word.Length; i++)
                    {
                        if (_word[i] == guessed)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                        count++;
                }

                return count;
            }
        }

       
        public IEnumerable<char> GuessedLetters
        {
            get
            {
                List<char> list = _guessedLetters.ToList();

                list.Sort(); 

                return list;
            }
        }

        public List<int> GuessLetter(char letter)
        {
            letter = char.ToLower(letter);

            _guessedLetters.Add(letter);

            List<int> positions = new List<int>();

            for (int i = 0; i < _word.Length; i++)
            {
                if (_word[i] == letter)
                {
                    positions.Add(i);
                    _revealed.Add(letter);
                }
            }

            return positions;
        }

        public bool GuessWord(string attempt)
        {
            if (attempt == null)
                return false;

            attempt = attempt.Trim();

            return string.Equals(attempt, _word, StringComparison.OrdinalIgnoreCase);
        }

        public int CalculateScore()
        {
            
            int revealed = _revealed.Count;

            
            int wrong = WrongUniqueGuesses;

            int remaining = MaxLetterGuesses - wrong;

            return revealed + remaining;
        }
    }
}
