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
    public class QrsController : ControllerBase
    {
        private readonly QrCode_DatabaseContext _context;

        public QrsController(QrCode_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Qrs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Qr>>> GetQrs()
        {
          if (_context.Qrs == null)
          {
              return NotFound();
          }
            return await _context.Qrs.ToListAsync();
        }

        // GET: api/Qrs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Qr>> GetQr(int id)
        {
          if (_context.Qrs == null)
          {
              return NotFound();
          }
            var qr = await _context.Qrs.FindAsync(id);

            if (qr == null)
            {
                return NotFound();
            }

            return qr;
        }

        // PUT: api/Qrs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQr(int id, Qr qr)
        {
            if (id != qr.IdQr)
            {
                return BadRequest();
            }

            _context.Entry(qr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QrExists(id))
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

        // POST: api/Qrs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Qr>> PostQr(Qr qr)
        {
          if (_context.Qrs == null)
          {
              return Problem("Entity set 'QrCode_DatabaseContext.Qrs'  is null.");
          }
            _context.Qrs.Add(qr);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQr", new { id = qr.IdQr }, qr);
        }

        // DELETE: api/Qrs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQr(int id)
        {
            if (_context.Qrs == null)
            {
                return NotFound();
            }
            var qr = await _context.Qrs.FindAsync(id);
            if (qr == null)
            {
                return NotFound();
            }

            _context.Qrs.Remove(qr);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QrExists(int id)
        {
            return (_context.Qrs?.Any(e => e.IdQr == id)).GetValueOrDefault();
        }
    }
}
