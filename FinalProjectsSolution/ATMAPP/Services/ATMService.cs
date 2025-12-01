using System;
using System.Threading.Tasks;
using ATMApp.Models;

namespace ATMApp.Services
{
    public class ATMService
    {
        private readonly UserService _userService = new UserService();
        private readonly LoggerService _logger = new LoggerService();

        //შევამოწმოთ ბალანსი, დეპოზიტი და გამოტანა
        public async Task CheckBalanceAsync(User user)
        {
            Console.WriteLine($"Balance: {user.Balance} GEL");
            await _logger.LogAsync($"Checked balance: {user.Balance} GEL", user.PersonalNumber);
        }

        public async Task DepositAsync(User user, decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            user.Balance += amount;
            await _userService.UpdateUserAsync(user);
            await _logger.LogAsync($"Deposited {amount} GEL. Balance now: {user.Balance} GEL", user.PersonalNumber);
        }

        public async Task WithdrawAsync(User user, decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            if (amount > user.Balance)
            {
                Console.WriteLine("Insufficient funds.");
                return;
            }

            user.Balance -= amount;
            await _userService.UpdateUserAsync(user);
            await _logger.LogAsync($"Withdrew {amount} GEL. Balance now: {user.Balance} GEL", user.PersonalNumber);
        }
    }
}
