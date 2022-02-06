using System;

namespace Reminder.Core.DTOs
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }
        public string Email { get; set; }
        public int TodoId { get; set; }
    }
}
