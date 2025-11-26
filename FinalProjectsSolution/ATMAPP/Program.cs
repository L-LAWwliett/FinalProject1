using System;
using System.Threading.Tasks;
using ATMApp.Services;

class Program
{
    static async Task Main()
    {
        var userService = new UserService();
        var authService = new AuthService();
        var atm = new ATMService();

        while (true)
        {
            Console.WriteLine("1) Register");
            Console.WriteLine("2) Login");
            Console.WriteLine("3) Exit");
            Console.Write("Choice: ");
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Firstname: ");
                string f = Console.ReadLine() ?? "";
                Console.Write("Lastname: ");
                string l = Console.ReadLine() ?? "";
                Console.Write("Personal Number: ");
                string p = Console.ReadLine() ?? "";

                try
                {
                    var user = await userService.RegisterUserAsync(f, l, p);
                    Console.WriteLine($"Registered. Password: {user.Password}");
                }
                catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
            }
            else if (choice == "2")
            {
                Console.Write("Personal Number: ");
                string p = Console.ReadLine() ?? "";
                Console.Write("Password: ");
                string pass = Console.ReadLine() ?? "";

                var user = await authService.LoginAsync(p, pass);
                if (user == null) { Console.WriteLine("Invalid login."); continue; }

                while (true)
                {
                    Console.WriteLine("\n1) Balance  2) Deposit  3) Withdraw  4) Logout");
                    Console.Write("Choice: ");
                    string? op = Console.ReadLine();
                    if (op == "1") await atm.CheckBalanceAsync(user);
                    else if (op == "2")
                    {
                        Console.Write("Amount: ");
                        decimal amt = decimal.Parse(Console.ReadLine() ?? "0");
                        await atm.DepositAsync(user, amt);
                    }
                    else if (op == "3")
                    {
                        Console.Write("Amount: ");
                        decimal amt = decimal.Parse(Console.ReadLine() ?? "0");
                        await atm.WithdrawAsync(user, amt);
                    }
                    else if (op == "4") break;
                }
            }
            else if (choice == "3") break;
        }
    }
}
