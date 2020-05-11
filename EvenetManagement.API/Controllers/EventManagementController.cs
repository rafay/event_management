using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvenetManagement.API.Models;
using EvenetManagement.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EvenetManagement.API.Controllers
{
    [Authorize]
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
            var createdEvent = _eventsService.CreateEvent(eventData);
            return Ok(createdEvent);
        }

        [HttpPost("session", Name = "AddNewSession")]
        public ActionResult<Session> AddNewSession(Session sessionData)
        {
            var createdSession = _eventsService.CreateSession(sessionData);
            return Ok(createdSession);
        }

        [HttpPost("session/attendee", Name = "AddAttendee")]
        public IActionResult AddAttendee(string sessionId, string userId, string userName)
        {
            _eventsService.AddAttendee(sessionId, userId, userName);
            return Ok();
        }

        [HttpPut("session/attendee/approve", Name = "ApproveAttendee")]
        public IActionResult ApproveAttendee(string sessionId, string userId)
        {
            _eventsService.ApproveAttendee(sessionId, userId);
            return Ok();
        }

        [HttpGet(Name = "GetAllEvents")]
        public ActionResult<IEnumerable<Event>> GetAllEvents()
        {
            var eventsFromService = _eventsService.Get();
            return Ok(eventsFromService);
        }

        [HttpGet("sessions", Name = "GetUserSessions")]
        public IActionResult GetUserSessions(string userId)
        {
            var userSessions = _eventsService.GetUserSessions(userId);
            return Ok(userSessions);
        }
    }
}