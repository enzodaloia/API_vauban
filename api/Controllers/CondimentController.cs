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
    public class CondimentController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<CondimentController> _logger;

        public CondimentController(ApiDbContext context, ILogger<CondimentController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var condiments = await _context.Condiments.ToListAsync();
                return Ok(condiments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to access the database.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Condiment>> PostCondiment(Condiment condiment)
        {
            try
            {
                _context.Condiments.Add(condiment);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCondiment), new { id = condiment.Id }, condiment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to create the condiment.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Condiment>> GetCondiment(int id)
        {
            var condiment = await _context.Condiments.FindAsync(id);

            if (condiment == null)
            {
                return NotFound();
            }

            return condiment;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCondiment(int id, Condiment condiment)
        {
            if (id != condiment.Id)
            {
                return BadRequest();
            }

            _context.Entry(condiment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCondiment(int id)
        {
            var condiment = await _context.Condiments.FindAsync(id);
            if (condiment == null)
            {
                return NotFound();
            }

            _context.Condiments.Remove(condiment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
