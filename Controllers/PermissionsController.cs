using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthServiceApi.Contexts;
using AuthServiceApi.Models;

namespace AuthServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly PermissionsContext _context;
        private readonly RolesContext _rolesContext;

        public PermissionsController(PermissionsContext context, RolesContext rolesContext)
        {
            _context = context;
            _rolesContext = rolesContext;
        }

        // GET: api/Permissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permissions>>> GetPermissions()
        {
            if (_context.Permissions == null)
            {
                return NotFound();
            }
            return await _context.Permissions.ToListAsync();
        }

        // GET: api/Permissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Permissions>> GetPermissions(long id)
        {
            if (_context.Permissions == null)
            {
                return NotFound();
            }
            var permissions = await _context.Permissions.FindAsync(id);

            if (permissions == null)
            {
                return NotFound();
            }

            return permissions;
        }

        // PUT: api/Permissions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermissions(long id, Permissions permissions)
        {
            if (id != permissions.Id)
            {
                return BadRequest();
            }

            _context.Entry(permissions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionsExists(id))
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

        // POST: api/Permissions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Permissions>> PostPermissions(Permissions permissions)
        {
            if (_context.Permissions == null)
            {
                return Problem("Entity set 'PermissionsContext.Permissions'  is null.");
            }

            if (PermissionsExists(permissions.Name))
            {
                return Problem($"Permission '{permissions.Name}' already exists.");
            }

            // Get the role with the given name
            ICollection<Roles> roles = new List<Roles>();

            foreach (var role in permissions.Roles)
            {
                var roleFromDb = await _rolesContext.Roles.FindAsync(role.Name);
                if (roleFromDb == null)
                {
                    return Problem($"Role '{role.Name}' does not exist.");
                }

                roles.Add(roleFromDb);
            }

            _context.Permissions.Add(permissions);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPermissions), new { id = permissions.Id }, permissions);
        }

        // DELETE: api/Permissions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermissions(long id)
        {
            if (_context.Permissions == null)
            {
                return NotFound();
            }
            var permissions = await _context.Permissions.FindAsync(id);
            if (permissions == null)
            {
                return NotFound();
            }

            _context.Permissions.Remove(permissions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PermissionsExists(long id)
        {
            return (_context.Permissions?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool PermissionsExists(string name)
        {
            return (_context.Permissions?.Any(e => e.Name == name)).GetValueOrDefault();
        }
    }
}
