using CancionesWebAPI.Data;
using CancionesWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CancionesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private readonly AppDbContext _context;
        public RandomController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<Canción>> GetCanción()
        {
            var list = await _context.Canción.ToListAsync();
            var max = list.Count;
            int index = new Random().Next(0, max);
            var canción = list[index];
            if (canción == null)
            {
                return NoContent();
            }
            return canción;
        }
    }
}
