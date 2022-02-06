using Microsoft.AspNetCore.Mvc;
using Reminder.Core.DTOs;
using Reminder.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reminder.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // GET: api/<NotificationsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDTO>>> GetNotifications()
        {
            try
            {
                return Ok(await _notificationService.GetNotifications());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // GET api/<NotificationsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationDTO>> GetNotification(int id)
        {
            try
            {
                return Ok(await _notificationService.GetNotification(id));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // POST api/<NotificationsController>
        [HttpPost]
        public async Task<ActionResult<NotificationDTO>> CreateNotification([FromBody] NotificationDTO notification)
        {
            try
            {
                return Ok(await _notificationService.CreateNotification(notification));
            }
            catch (Exception exception)
            {

                return BadRequest(exception.Message);
            }
        }

        // PUT api/<NotificationsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<NotificationDTO>> UpdateNotification([FromBody] NotificationDTO notification)
        {
            try
            {
                return Ok(await _notificationService.UpdateNotification(notification));
            }
            catch (Exception exception)
            {

                return BadRequest(exception.Message);
            }
        }

        // DELETE api/<NotificationsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            try
            {
                await _notificationService.DeleteNotification(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
