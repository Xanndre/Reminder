using Microsoft.AspNetCore.Mvc;
using Reminder.Core.DTOs;
using Reminder.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reminder.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/<TodosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDTO>>> GetTodos()
        {
            try
            {
                return Ok(await _todoService.GetTodos());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDTO>> GetTodo(int id)
        {
            try
            {
                return Ok(await _todoService.GetTodo(id));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // POST api/<TodosController>
        [HttpPost]
        public async Task<ActionResult<TodoDTO>> CreateTodo([FromBody] TodoDTO todo)
        {
            try
            {
                return Ok(await _todoService.CreateTodo(todo));
            }
            catch (Exception exception)
            {

                return BadRequest(exception.Message);
            }
        }

        // PUT api/<TodosController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoDTO>> UpdateTodo([FromBody] TodoDTO todo)
        {
            try
            {
                return Ok(await _todoService.UpdateTodo(todo));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            try
            {
                await _todoService.DeleteTodo(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
