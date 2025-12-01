using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATMApp.Data;
using ATMApp.Models;

namespace ATMApp.Services
{
    public class UserService
    {
        private readonly UserRepository _repo = new UserRepository();

        // არეგისტრირებს ახალ მომხმარებელს
        public async Task<User> RegisterUserAsync(string first, string last, string personal)
        {
            if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(last) || string.IsNullOrWhiteSpace(personal))
                throw new ArgumentException("Firstname, lastname and personal number are required.");

            if (personal.Length != 11 || !personal.All(char.IsDigit))
                throw new ArgumentException("Personal number must be exactly 11 digits long.");

            var users = new List<User>();
            await foreach (var u in _repo.LoadUsersAsync()) users.Add(u);

            if (users.Any(u => u.PersonalNumber == personal))
                throw new InvalidOperationException("User with this personal number already exists.");

            


            string password = new Random().Next(1000, 10000).ToString();

            var newUser = new User
            {
                Id = users.Count == 0 ? 1 : users.Max(x => x.Id) + 1,
                Firstname = first,
                Lastname = last,
                PersonalNumber = personal,
                Password = password,
                Balance = 0m
            };

            users.Add(newUser);
            await _repo.SaveUsersAsync(users);
            return newUser;
        }

        // აბრუნებს მომხმარებელს პირადი ნომრითა და პაროლით
        public async Task<User?> GetUserAsync(string personal, string password)
        {
            await foreach (var u in _repo.LoadUsersAsync())
            {
                if (u.PersonalNumber == personal && u.Password == password)
                    return u;
            }
            return null;
        }

        // ანახლებს მომხმარებლის ინფორმაციას
        public async Task UpdateUserAsync(User updated)
        {
            var users = new List<User>();
            await foreach (var u in _repo.LoadUsersAsync()) users.Add(u);

            var idx = users.FindIndex(x => x.Id == updated.Id);
            if (idx >= 0) users[idx] = updated;
            else users.Add(updated);

            await _repo.SaveUsersAsync(users);
        }

        public IAsyncEnumerable<User> GetAllUsersAsync() => _repo.LoadUsersAsync();
    }
}
