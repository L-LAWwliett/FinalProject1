using System;

namespace ATMApp.Models
{
    public class LogEntry
    {
        public string Message { get; set; } = string.Empty;
        public DateTime Time { get; set; } = DateTime.UtcNow;
        public string UserPersonalNumber { get; set; } = string.Empty;
    }
}
