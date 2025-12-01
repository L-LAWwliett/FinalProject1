using System.Threading.Tasks;
using ATMApp.Models;

namespace ATMApp.Services
{
    public class AuthService
    {
        private readonly UserService _userService = new UserService();

        public Task<User> LoginAsync(string personal, string password) =>
            _userService.GetUserAsync(personal, password);
    }
}
