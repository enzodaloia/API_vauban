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
    public class FacturationController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<FacturationController> _logger;

        public FacturationController(ApiDbContext context, ILogger<FacturationController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var facturations = await _context.Facturations.ToListAsync();
                return Ok(facturations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to access the database.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Facturation>> PostFacturation(Facturation facturation)
        {
            try
            {
                _context.Facturations.Add(facturation);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetFacturation), new { id = facturation.Id }, facturation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to create the facturation.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Facturation>> GetFacturation(int id)
        {
            var facturation = await _context.Facturations.FindAsync(id);

            if (facturation == null)
            {
                return NotFound();
            }

            return facturation;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchFacturation(int id, Facturation facturation)
        {
            if (id != facturation.Id)
            {
                return BadRequest();
            }

            _context.Entry(facturation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacturation(int id)
        {
            var facturation = await _context.Facturations.FindAsync(id);
            if (facturation == null)
            {
                return NotFound();
            }

            _context.Facturations.Remove(facturation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
