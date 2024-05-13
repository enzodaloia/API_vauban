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
    public class MenageController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<MenageController> _logger;

        public MenageController(ApiDbContext context, ILogger<MenageController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var menages = await _context.Menages.ToListAsync();
                return Ok(menages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to access the database.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Menage>> PostMenage(Menage menage)
        {
            try
            {
                _context.Menages.Add(menage);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMenage), new { id = menage.id }, menage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to create the menage task.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Menage>> GetMenage(int id)
        {
            var menage = await _context.Menages.FindAsync(id);

            if (menage == null)
            {
                return NotFound();
            }

            return menage;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMenage(int id, Menage menage)
        {
            if (id != menage.id)
            {
                return BadRequest();
            }

            _context.Entry(menage).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenage(int id)
        {
            var menage = await _context.Menages.FindAsync(id);
            if (menage == null)
            {
                return NotFound();
            }

            _context.Menages.Remove(menage);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
