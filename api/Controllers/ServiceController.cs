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
    public class ServiceController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(ApiDbContext context, ILogger<ServiceController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var services = await _context.Services.ToListAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to access the database.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            try
            {
                _context.Services.Add(service);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetService), new { id = service.id }, service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to create the service.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchService(int id, Service service)
        {
            if (id != service.id)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
