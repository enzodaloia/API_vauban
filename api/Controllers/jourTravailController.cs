using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class JourTravailController : Controller
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<JourTravailController> _logger;

        public JourTravailController(ApiDbContext context, ILogger<JourTravailController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var records = await _context.JourTravail.ToListAsync();
                return Ok(records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to access the database.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<jourTravail>> PostJourTravail(jourTravail jourTravail)
        {
            _context.JourTravail.Add(jourTravail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = jourTravail.Id }, jourTravail);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<jourTravail>> GetJourTravail(int id)
        {
            var jourTravail = await _context.JourTravail.FindAsync(id);

            if (jourTravail == null)
            {
                return NotFound();
            }

            return jourTravail;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchJourTravail(int id, jourTravail jourTravail)
        {
            if (id != jourTravail.Id)
            {
                return BadRequest();
            }

            _context.Entry(jourTravail).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJourTravail(int id)
        {
            var jourTravail = await _context.JourTravail.FindAsync(id);
            if (jourTravail == null)
            {
                return NotFound();
            }

            _context.JourTravail.Remove(jourTravail);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
