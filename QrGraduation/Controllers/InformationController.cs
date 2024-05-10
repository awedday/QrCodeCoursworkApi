using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QrGraduation.Models;

namespace QrGraduation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly QrCode_DatabaseContext _context;
        private readonly ILogger<InformationController> _logger;

        public InformationController(QrCode_DatabaseContext context, ILogger<InformationController> logger)
        {
            _context = context;
            _logger = logger; 
        }
        [HttpGet("latest/{employeeId}")]
        public async Task<ActionResult<Information>> GetLatestInformation(int employeeId)
        {
            var latestInformation = await _context.Information
                .Where(i => i.EmployeeId == employeeId)
                .OrderByDescending(i => i.IdInformation)
                .FirstOrDefaultAsync();

            if (latestInformation == null)
            {
                return NotFound();
            }

            return latestInformation;
        }
        // GET: api/Information
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Information>>> GetInformation()
        {
            _logger.LogCritical("Данные о информации пользователя выгружены");
          if (_context.Information == null)
          {
              return NotFound();
          }
            return await _context.Information.ToListAsync();
        }

        // GET: api/Information/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Information>> GetInformation(int id)
        {
          if (_context.Information == null)
          {
              return NotFound();
          }
            var information = await _context.Information.FindAsync(id);

            if (information == null)
            {
                return NotFound();
            }

            return information;
        }

        // PUT: api/Information/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformation(int id, Information information)
        {
            if (id != information.IdInformation)
            {
                return BadRequest();
            }

            _context.Entry(information).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InformationExists(id))
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

        // POST: api/Information
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Information>> PostInformation(Information information)
        {
          if (_context.Information == null)
          {
              return Problem("Entity set 'QrCode_DatabaseContext.Information'  is null.");
          }
            _context.Information.Add(information);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInformation", new { id = information.IdInformation }, information);
        }

        // DELETE: api/Information/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformation(int id)
        {
            if (_context.Information == null)
            {
                return NotFound();
            }
            var information = await _context.Information.FindAsync(id);
            if (information == null)
            {
                return NotFound();
            }

            _context.Information.Remove(information);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InformationExists(int id)
        {
            return (_context.Information?.Any(e => e.IdInformation == id)).GetValueOrDefault();
        }
    }
}
