using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using ATMApp.Models;

namespace ATMApp.Data
{
    public class UserRepository
    {
        private readonly string _filePath = "users.json";
        private readonly JsonSerializerOptions _opts = new JsonSerializerOptions { WriteIndented = true };

        public async IAsyncEnumerable<User> LoadUsersAsync()
        {
            if (!File.Exists(_filePath)) yield break;

            string json = await File.ReadAllTextAsync(_filePath).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(json)) yield break;

            var users = JsonSerializer.Deserialize<List<User>>(json, _opts);
            if (users == null) yield break;

            foreach (var u in users) yield return u;
        }

        public async Task SaveUsersAsync(IEnumerable<User> users)
        {
            var list = users.ToList();
            string json = JsonSerializer.Serialize(list, _opts);
            await File.WriteAllTextAsync(_filePath, json).ConfigureAwait(false);
        }
    }
}
