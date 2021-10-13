using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CancionesWebAPI.Data;
using CancionesWebAPI.Models;

namespace CancionesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancionesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CancionesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Canciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Canción>>> GetCanción()
        {
            return await _context.Canción.ToListAsync();
        }

        // GET: api/Canciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Canción>> GetCanción(string id)
        {
            var canción = await _context.Canción.FindAsync(id);

            if (canción == null)
            {
                return NotFound();
            }

            return canción;
        }

        // PUT: api/Canciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCanción(string id, Canción canción)
        {
            if (id != canción.Nombre)
            {
                return BadRequest();
            }

            _context.Entry(canción).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CanciónExists(id))
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

        // POST: api/Canciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Canción>> PostCanción(Canción canción)
        {
            _context.Canción.Add(canción);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CanciónExists(canción.Nombre))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCanción", new { id = canción.Nombre }, canción);
        }

        // DELETE: api/Canciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCanción(string id)
        {
            var canción = await _context.Canción.FindAsync(id);
            if (canción == null)
            {
                return NotFound();
            }

            _context.Canción.Remove(canción);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CanciónExists(string id)
        {
            return _context.Canción.Any(e => e.Nombre == id);
        }
    }
}
