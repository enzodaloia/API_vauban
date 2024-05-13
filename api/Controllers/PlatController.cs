using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<PlatController> _logger;

        public PlatController(ApiDbContext context, ILogger<PlatController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var plats = await _context.Plats.ToListAsync();
                return Ok(plats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to access the database.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Plat>> PostPlat(Plat plat)
        {
            try
            {
                _context.Plats.Add(plat);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPlat), new { id = plat.Id }, plat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to create the plat.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Plat>> GetPlat(int id)
        {
            var plat = await _context.Plats.FindAsync(id);

            if (plat == null)
            {
                return NotFound();
            }

            return plat;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPlat(int id, Plat plat)
        {
            if (id != plat.Id)
            {
                return BadRequest();
            }

            _context.Entry(plat).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlat(int id)
        {
            var plat = await _context.Plats.FindAsync(id);
            if (plat == null)
            {
                return NotFound();
            }

            _context.Plats.Remove(plat);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
