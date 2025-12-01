using System.Xml.Serialization;
using HangGame.Models;

namespace HangGame.Services
{
    public class XmlStatsRepository
    {
        private readonly string _filePath = "players.xml";
        private readonly XmlSerializer _serializer;

        public XmlStatsRepository()
        {
            _serializer = new XmlSerializer(typeof(List<PlayerRecord>));
        }

        // აბრუნებს მოთამაშეების სტატისტიკას XML ფაილიდან ლისტის სახით
        public List<PlayerRecord> Load()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<PlayerRecord>();

                using var stream = File.OpenRead(_filePath);
                return (List<PlayerRecord>)_serializer.Deserialize(stream);
            }
            catch
            {
                return new List<PlayerRecord>();
            }
        }

        // ინახავს მოთამაშეების სტატისტიკას XML ფაილში
        public void Save(List<PlayerRecord> records)
        {
            try
            {
                using var stream = File.Create(_filePath);
                _serializer.Serialize(stream, records);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save stats: {ex.Message}");
            }
        }
        // ანახლებს მოთამაშის რეკორდს ან ქმნის ახალს
        public void UpdatePlayerRecord(string playerName, int newScore)
        {
            var records = Load();

            var player = records.FirstOrDefault(p =>
                p.Name.Equals(playerName, StringComparison.OrdinalIgnoreCase));

            if (player == null)
            {
                records.Add(new PlayerRecord { Name = playerName, HighestScore = newScore });
            }
            else if (newScore > player.HighestScore)
            {
                player.HighestScore = newScore;
            }

            Save(records);
        }

        // აბრუნებს ტოპ N(ამ შემთხვევაში 10) მოთამაშეს მათი საუკეთესო ქულებით
        public List<PlayerRecord> GetTopPlayers(int top = 10)
        {
            return Load()
                .OrderByDescending(r => r.HighestScore)
                .ThenBy(r => r.Name)
                .Take(top)
                .ToList();
        }
    }
}
