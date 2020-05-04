using EvenetManagement.API.Models;
using System.Collections.Generic;

namespace EvenetManagement.API.Services
{
    public interface IEventsService
    {
        Event Create(Event eventData);
        List<Event> Get();
        Event Get(string id);
    }
}