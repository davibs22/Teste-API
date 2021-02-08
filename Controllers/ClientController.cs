using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ClientApi.Models;

namespace ClientApi.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class ClientController : Controller
    {
        private readonly ClientContext _context;

        public ClientController(ClientContext context)
        {
            _context = context;

            if (_context.ClientItems.Count() == 0)
            {
                _context.ClientItems.Add(new ClientItem { Name = "Davi", Address = "Rua Pedro da Veiga, 222", City = "Rio de Janeiro" });
                _context.SaveChanges();
            }
        }

        // GET: api/Client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientItem>>> GetClientItem()
        {
            return await _context.ClientItems.ToListAsync();
        }

        private bool ClientItemExists(long id)
        {
            return _context.ClientItems.Any(e => e.Id == id);
        }

        // PUT: api/Client/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientItem(long id, ClientItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Client
        [HttpPost]
        public async Task<ActionResult<ClientItem>> PostClientItem(ClientItem todoItem)
        {
            _context.ClientItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientItem", new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClientItem>> DeleteClientItem(long id)
        {
            var todoItem = await _context.ClientItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.ClientItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        private bool ClientItemExists(long id)
        {
            return _context.ClientItems.Any(e => e.Id == id);
        }
    }
    }
}
