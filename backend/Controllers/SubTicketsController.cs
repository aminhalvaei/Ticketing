using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ticketing.Models;

namespace Ticketing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SubTickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubTicket>>> GetSubTickets()
        {
            var result = await _context.SubTickets.Include(x => x.Attachments).Include(x => x.Ticket).ToListAsync();
            return result;
        }

        // GET: api/SubTickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubTicket>> GetSubTicket(int id)
        {
            var subTicket = await _context.SubTickets.Include(x => x.Attachments).FirstAsync(x => x.Id == id);

            if (subTicket == null)
            {
                return NotFound();
            }

            return subTicket;
        }

        // PUT: api/SubTickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubTicket(int id, SubTicket subTicket)
        {
            if (id != subTicket.Id)
            {
                return BadRequest();
            }

            _context.Entry(subTicket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubTicketExists(id))
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

        // POST: api/SubTickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubTicket>> PostSubTicket(SubTicket subTicket)
        {
            _context.SubTickets.Add(subTicket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubTicket", new { id = subTicket.Id }, subTicket);
        }

        // DELETE: api/SubTickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubTicket(int id)
        {
            var subTicket = await _context.SubTickets.FindAsync(id);
            if (subTicket == null)
            {
                return NotFound();
            }

            _context.SubTickets.Remove(subTicket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubTicketExists(int id)
        {
            return _context.SubTickets.Any(e => e.Id == id);
        }
    }
}
