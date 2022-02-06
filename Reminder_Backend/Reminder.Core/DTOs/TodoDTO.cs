using System;
using System.Collections.Generic;

namespace Reminder.Core.DTOs
{
    public class TodoDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public ICollection<NotificationDTO> Notifications { get; set; }
    }
}
