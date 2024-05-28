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
    public class OrganizationUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrganizationUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/OrganizationUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationUser>>> GetOrganizationUsers()
        {
            return await _context.OrganizationUsers.Include(x => x.User)
                                                   .Include(x => x.Organization).ToListAsync();
        }

        // GET: api/OrganizationUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationUser>> GetOrganizationUser(string id)
        {
            var organizationUser = await _context.OrganizationUsers.Include(x => x.User)
                                                                   .Include(x => x.Organization).FirstAsync(x => x.UserId == id);

            if (organizationUser == null)
            {
                return NotFound();
            }

            return organizationUser;
        }

        // PUT: api/OrganizationUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganizationUser(string id, OrganizationUser organizationUser)
        {
            if (id != organizationUser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(organizationUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationUserExists(id))
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

        // POST: api/OrganizationUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrganizationUser>> PostOrganizationUser(OrganizationUser organizationUser)
        {
            _context.OrganizationUsers.Add(organizationUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrganizationUserExists(organizationUser.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrganizationUser", new { id = organizationUser.UserId }, organizationUser);
        }

        // DELETE: api/OrganizationUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationUser(string id)
        {
            var organizationUser = await _context.OrganizationUsers.FindAsync(id);
            if (organizationUser == null)
            {
                return NotFound();
            }

            _context.OrganizationUsers.Remove(organizationUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizationUserExists(string id)
        {
            return _context.OrganizationUsers.Any(e => e.UserId == id);
        }
    }
}
