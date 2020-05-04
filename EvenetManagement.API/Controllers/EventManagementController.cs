using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvenetManagement.API.Models;
using EvenetManagement.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvenetManagement.API.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventManagementController : ControllerBase
    {
        private readonly IEventsService _eventsService;
        public EventManagementController(IEventsService eventsService)
        {
            _eventsService = eventsService ??
               throw new ArgumentNullException(nameof(eventsService));
        }

        [HttpPost(Name = "CreateEvent")]
        public ActionResult<Event> CreateEvent(Event eventData)
        {
            var sampleEventData = new Event
            {
                EventName = "FirstEvent",
                EventDescription = "FirstEvent-Description",
                HostId = "1234",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(2)
            };
            var createdEvent = _eventsService.Create(sampleEventData);
            return Ok(createdEvent);
        }

        [HttpGet(Name = "GetAllEvents")]
        public ActionResult<IEnumerable<Event>> GetAllEvents()
        {
            var eventsFromService = _eventsService.Get();
            return Ok(eventsFromService);
        }
    }
}