using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenetManagement.API.Models
{
    public class Session
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string EventId { get; set; }

        public string SessionName { get; set; }

        public string SessionDescription { get; set; }

        public string HostId { get; set; }

        public int MaxAttendees { get; set; }

        public List<Attendee> Attendees { get; set; } = new List<Attendee>();


        [BsonDateTimeOptions]
        public DateTime StartDate { get; set; }

        [BsonDateTimeOptions]
        public DateTime EndDate { get; set; }
    }

    public class Attendee
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public bool Approved { get; set; } = false;
    }
}
