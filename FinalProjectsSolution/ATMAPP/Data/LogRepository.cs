using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using ATMApp.Models;

namespace ATMApp.Data
{
    public class LogRepository
    {
        private readonly string _filePath = "log.json";
        private readonly JsonSerializerOptions _opts = new JsonSerializerOptions { WriteIndented = true };

        // იტვირთავს ლოგებს JSON ფაილიდან
        public async IAsyncEnumerable<LogEntry> LoadLogsAsync()
        {
            if (!File.Exists(_filePath)) yield break;

            string json = await File.ReadAllTextAsync(_filePath).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(json)) yield break;

            var logs = JsonSerializer.Deserialize<List<LogEntry>>(json, _opts);
            if (logs == null) yield break;

            foreach (var l in logs) yield return l;
        }

        // ამატებს ახალ ლოგს JSON ფაილში
        public async Task AddLogAsync(LogEntry entry)
        {
            var list = new List<LogEntry>();
            await foreach (var l in LoadLogsAsync().ConfigureAwait(false))
                list.Add(l);

            list.Add(entry);
            string json = JsonSerializer.Serialize(list, _opts);
            await File.WriteAllTextAsync(_filePath, json).ConfigureAwait(false);
        }
    }
}
