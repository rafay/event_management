using EvenetManagement.API.Models;
using System.Collections.Generic;

namespace EvenetManagement.API.Services
{
    public interface IEventsService
    {
        Event CreateEvent(Event eventData);
        List<Event> Get();
        Event Get(string id);
        
        Session CreateSession(Session sessionData);

        void AddAttendee(string sessionId, string userId, string userName);

        void ApproveAttendee(string sessionId, string userId);

        List<UserSession> GetUserSessions(string userId);
    }
}