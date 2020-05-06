using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenetManagement.API.Models
{
    public class UserSession
    {
        public string EventId { get; set; }
        public string SessionId { get; set; }
        public string SessionName { get; set; }
        public Attendee attendee { get; set; }
        public Event eventDetails { get; set; }
    }
}
