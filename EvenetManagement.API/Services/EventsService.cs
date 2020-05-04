using EvenetManagement.API.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvenetManagement.API.Services
{
    public class EventsService : IEventsService
    {
        private readonly IMongoCollection<Event> _events;

        public EventsService(IDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            _events = database.GetCollection<Event>("Events");
        }


        public List<Event> Get() =>
            _events.Find(eventData => true).ToList();

        public Event Get(string id) =>
            _events.Find<Event>(eventData => eventData.Id == id).FirstOrDefault();

        public Event Create(Event eventData)
        {
            _events.InsertOne(eventData);
            return eventData;
        }
    }
}
