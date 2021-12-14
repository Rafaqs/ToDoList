using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Todo.Dominio;
using Todo.Repo;


namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        [HttpGet]
        
        public async Task<IActionResult> GetAsync([FromServices] TodoContext context)
        {
            var todos = await context
                .Todos
                .AsNoTracking()
                .ToListAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]        
        public async Task<IActionResult> GetByIdAsync([FromServices] TodoContext context,
                                                      [FromRoute] int id)
        {
            var todo = await context
                .Todos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return todo == null ? NotFound() : Ok(todo);
        }

        [HttpPost]
        

        public async Task<IActionResult> PostAsync(
            [FromServices] TodoContext context, [FromBody] TodoView model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = new ToDo
            {
                Data = DateTime.Now,
                Finalizado = false,
                Titulo = model.Titulo,
                Descricao = model.Descricao
            };

            try
            {
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
                return Created($"todos/{todo.Id}", todo);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] TodoContext context, 
            [FromBody] TodoView model,
            [FromRoute] int id)

        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)
                return NotFound();

            try
            {
                todo.Titulo = model.Titulo;
                todo.Descricao = model.Descricao;

                context.Todos.Update(todo);
                await context.SaveChangesAsync();
                return Ok(todo);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteAsync(
            [FromServices] TodoContext context,
            [FromRoute] int id)
        {
            var todo = await context
                .Todos
                .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Todos.Remove(todo);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
