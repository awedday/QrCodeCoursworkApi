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
    public class EmployeePositionsController : ControllerBase
    {
        private readonly QrCode_DatabaseContext _context;

        public EmployeePositionsController(QrCode_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/EmployeePositions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeePosition>>> GetEmployeePositions()
        {
          if (_context.EmployeePositions == null)
          {
              return NotFound();
          }
            return await _context.EmployeePositions.ToListAsync();
        }

        // GET: api/EmployeePositions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeePosition>> GetEmployeePosition(int id)
        {
          if (_context.EmployeePositions == null)
          {
              return NotFound();
          }
            var employeePosition = await _context.EmployeePositions.FindAsync(id);

            if (employeePosition == null)
            {
                return NotFound();
            }

            return employeePosition;
        }

        // PUT: api/EmployeePositions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeePosition(int id, EmployeePosition employeePosition)
        {
            if (id != employeePosition.IdEmployeePosition)
            {
                return BadRequest();
            }

            _context.Entry(employeePosition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeePositionExists(id))
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

        // POST: api/EmployeePositions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeePosition>> PostEmployeePosition(EmployeePosition employeePosition)
        {
          if (_context.EmployeePositions == null)
          {
              return Problem("Entity set 'QrCode_DatabaseContext.EmployeePositions'  is null.");
          }
            _context.EmployeePositions.Add(employeePosition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeePosition", new { id = employeePosition.IdEmployeePosition }, employeePosition);
        }

        // DELETE: api/EmployeePositions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeePosition(int id)
        {
            if (_context.EmployeePositions == null)
            {
                return NotFound();
            }
            var employeePosition = await _context.EmployeePositions.FindAsync(id);
            if (employeePosition == null)
            {
                return NotFound();
            }

            _context.EmployeePositions.Remove(employeePosition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeePositionExists(int id)
        {
            return (_context.EmployeePositions?.Any(e => e.IdEmployeePosition == id)).GetValueOrDefault();
        }
    }
}
