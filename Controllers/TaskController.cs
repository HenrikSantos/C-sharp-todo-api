using C_sharp_todo_api.Context;
using C_sharp_todo_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace C_sharp_todo_api.Controllers
{
    [ApiController]
    [Route("task")]
    public class TaskController : ControllerBase
    {
        private readonly TodoDbContext _context;

        public TaskController(TodoDbContext context)
        {
            this._context = context;
        }


        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(_context.Tasks.ToList());
        }


        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var result = _context.Tasks.Find(id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("title/{title}")]
        public IActionResult GetByTitle(string title)
        {
            var result = _context.Tasks.Where(x => x.Title.ToUpper().Contains(title.ToUpper())).ToList();

            return Ok(result);
        }

        [HttpGet("date/{date}")]
        public IActionResult GetByDate(DateTime date)
        {
            var result = _context.Tasks.Where(x => x.Date == date).ToList();

            return Ok(result);
        }

        [HttpGet("status/{status}")]
        public IActionResult GetByStatus(string status)
        {
            EnumStatus enumStatus;
            if (!Enum.TryParse(status, true, out enumStatus))
            {
                return BadRequest("Invalid status");
            }

            var result = _context.Tasks.Where(x => x.Status == enumStatus).ToList();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(TodoTask newTask)
        {
            if (newTask.Date == DateTime.MinValue)
                newTask.Date = DateTime.Now;

            _context.Tasks.Add(newTask);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTaskById), new { id = newTask.Id }, newTask);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, TodoTask updatedTask)
        {
            var result = _context.Tasks.Find(id);

            if (result == null) return NotFound();

            if (updatedTask.Date == DateTime.MinValue) return BadRequest(new { Erro = "Invalid date" });

            result.Title = updatedTask.Title;
            result.Description = updatedTask.Description;   
            result.Status = updatedTask.Status;
            result.Date = updatedTask.Date;

            _context.Tasks.Update(result);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id) {
            var result = _context.Tasks.Find(id);

            if(result == null) return NotFound();

            _context.Tasks.Remove(result);
            _context.SaveChanges();

            return Ok(); 
        }
    }
}
