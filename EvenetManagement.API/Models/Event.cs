using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenetManagement.API.Models
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string EventName { get; set; }

        public string EventDescription { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string HostId { get; set; }

    }
}
