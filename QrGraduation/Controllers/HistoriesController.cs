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
    public class HistoriesController : ControllerBase
    {
        private readonly QrCode_DatabaseContext _context;

        public HistoriesController(QrCode_DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet("latest/{employeeId}")]
        public async Task<ActionResult<History>> GetLatestHistory(int employeeId)
        {
            var latestHistory = await _context.Histories
                .Where(h => h.EmployeeId == employeeId)
                .OrderByDescending(h => h.DateStartHistory)
                .FirstOrDefaultAsync();

            if (latestHistory == null)
            {
                return NotFound();
            }

            return latestHistory;
        }
        // GET: api/Histories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<History>>> GetHistories()
        {
          if (_context.Histories == null)
          {
              return NotFound();
          }
            return await _context.Histories.ToListAsync();
        }

        // GET: api/Histories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<History>> GetHistory(int id)
        {
          if (_context.Histories == null)
          {
              return NotFound();
          }
            var history = await _context.Histories.FindAsync(id);

            if (history == null)
            {
                return NotFound();
            }

            return history;
        }

        // PUT: api/Histories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistory(int id, History history)
        {
            if (id != history.IdHistory)
            {
                return BadRequest();
            }

            _context.Entry(history).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryExists(id))
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

        // POST: api/Histories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<History>> PostHistory(History history)
        {
          if (_context.Histories == null)
          {
              return Problem("Entity set 'QrCode_DatabaseContext.Histories'  is null.");
          }
            _context.Histories.Add(history);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistory", new { id = history.IdHistory }, history);
        }
        [HttpGet("Employee/{id}")]
        public async Task<ActionResult<IEnumerable<History>>> GetEmployeeHistory(int id)
        {
            var employeeHistory = await _context.Histories
                                                .Where(h => h.EmployeeId == id)
                                                .ToListAsync();

            if (employeeHistory == null || employeeHistory.Count == 0)
            {
                return NotFound();
            }

            return Ok(employeeHistory);
        }


        // DELETE: api/Histories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistory(int id)
        {
            if (_context.Histories == null)
            {
                return NotFound();
            }
            var history = await _context.Histories.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }

            _context.Histories.Remove(history);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("BySecondName/{secondName}")]
        public async Task<ActionResult<IEnumerable<History>>> GetHistoriesBySecondName(string secondName)
        {
            // Находим ID сотрудников с заданной фамилией
            var employeeIds = await _context.Employees
                .Where(e => e.SecondNameEmployee == secondName)
                .Select(e => e.IdEmployee)
                .ToListAsync();

            if (!employeeIds.Any())
            {
                return NotFound("Сотрудники с такой фамилией не найдены.");
            }

            // Используя найденные ID, находим соответствующие истории
            var histories = await _context.Histories
                .Where(h => employeeIds.Contains(h.EmployeeId))
                .ToListAsync();

            if (!histories.Any())
            {
                return NotFound("Истории для данных сотрудников не найдены.");
            }

            return histories;
        }
        
        private bool HistoryExists(int id)
        {
            return (_context.Histories?.Any(e => e.IdHistory == id)).GetValueOrDefault();
        }
    }
}
