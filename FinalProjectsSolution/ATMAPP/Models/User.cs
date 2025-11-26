namespace ATMApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string PersonalNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // 4 
        public decimal Balance { get; set; } = 0m;
    }
}
