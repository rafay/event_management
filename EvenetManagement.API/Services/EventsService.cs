using EvenetManagement.API.Models;
using MongoDB.Bson;
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
        private readonly IMongoCollection<Session> _sessions;

        public EventsService(IDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            _events = database.GetCollection<Event>("Events");
            _sessions = database.GetCollection<Session>("Sessions");
        }


        public List<Event> Get() =>
            _events.Find(eventData => true).ToList();

        public Event Get(string id) =>
            _events.Find<Event>(eventData => eventData.Id == id).FirstOrDefault();

        public Event CreateEvent(Event eventData)
        {
            _events.InsertOne(eventData);
            return eventData;
        }

        public Session CreateSession(Session sessionData)
        {
            _sessions.InsertOne(sessionData);
            return sessionData;
        }

        public void AddAttendee(string sessionId, string userId, string userName)
        {
            var session = _sessions.Find<Session>(sessionData => sessionData.Id == sessionId).FirstOrDefault();

            if (session != null && session.MaxAttendees > session.Attendees.Count())
            {
                if (!session.Attendees.Any<Attendee>(atnd => atnd.UserId == userId))
                {
                    var attendee = new Attendee
                    {
                        UserId = userId,
                        Name = userName
                    };

                    var filter = Builders<Session>.Filter.Eq("_id", ObjectId.Parse(sessionId));
                    var update = Builders<Session>.Update.Push<Attendee>("Attendees", attendee);
                    var updatedSession = _sessions.UpdateOne(filter, update);
                }

            }
        }

        public void ApproveAttendee(string sessionId, string userId)
        {
            var builder = Builders<Session>.Filter;
            var filter = builder.Eq("_id", ObjectId.Parse(sessionId)) & builder.Eq("Attendees.UserId", userId);
            var update = Builders<Session>.Update.Set<bool>("Attendees.$.Approved", true);

            var updatedSession = _sessions.UpdateOne(filter, update);
        }


        public List<UserSession> GetUserSessions(string userId)
        {
            var userSessions = (from s in _sessions.AsQueryable()
                                join e in _events.AsQueryable() on s.EventId equals e.Id into joined
                                where s.Attendees.Any<Attendee>(a => a.UserId == userId)
                                select new UserSession { attendee = s.Attendees.Where(atd => atd.UserId == userId).First(), SessionName = s.SessionName, SessionId = s.Id, EventId = s.EventId, eventDetails = joined.First() }).ToList<UserSession>();

            return userSessions;


        }
    }
}
