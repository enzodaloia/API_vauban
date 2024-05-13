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
    public class CommandeController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<CommandeController> _logger;

        public CommandeController(ApiDbContext context, ILogger<CommandeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var commandes = await _context.Commandes.ToListAsync();
                return Ok(commandes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to access the database.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Commande>> PostCommande(Commande commande)
        {
            try
            {
                _context.Commandes.Add(commande);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCommande), new { id = commande.Id }, commande);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to create the commande.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Commande>> GetCommande(int id)
        {
            var commande = await _context.Commandes.FindAsync(id);

            if (commande == null)
            {
                return NotFound();
            }

            return commande;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCommande(int id, Commande commande)
        {
            if (id != commande.Id)
            {
                return BadRequest();
            }

            _context.Entry(commande).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommande(int id)
        {
            var commande = await _context.Commandes.FindAsync(id);
            if (commande == null)
            {
                return NotFound();
            }

            _context.Commandes.Remove(commande);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
