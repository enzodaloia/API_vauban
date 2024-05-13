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
    public class MenuController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<MenuController> _logger;

        public MenuController(ApiDbContext context, ILogger<MenuController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var menus = await _context.Menus.ToListAsync();
                return Ok(menus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to access the database.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(Menu menu)
        {
            try
            {
                _context.Menus.Add(menu);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMenu), new { id = menu.Id }, menu);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to create the menu.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            var menu = await _context.Menus.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMenu(int id, Menu menu)
        {
            if (id != menu.Id)
            {
                return BadRequest();
            }

            _context.Entry(menu).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
