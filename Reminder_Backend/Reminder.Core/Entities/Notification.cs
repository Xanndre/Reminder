using System;

namespace Reminder.Core.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }
        public string Email { get; set; }
        public Todo Todo { get; set; }
        public int TodoId { get; set; }
    }
}
