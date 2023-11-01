using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DienappApi.Data;
using DienappApi.Models;

namespace DienappApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProvideremployeeController : ControllerBase
    {
        private readonly DIENAPPRESTAPIContext _context;

        public ProvideremployeeController(DIENAPPRESTAPIContext context)
        {
            _context = context;
        }

        // GET: api/Provideremployee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provideremployee>>> GetAllProvideremployees()
        {
            return await _context.Provideremployees.ToListAsync();
        }

        // GET: api/Provideremployee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Provideremployee>> GetProvideremployeeById(int id)
        {
            var provideremployee = await _context.Provideremployees.FindAsync(id);

            if (provideremployee == null)
            {
                return NotFound();
            }

            return provideremployee;
        }

        // PUT: api/Provideremployee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvideremployeeById(int id,
                        Provideremployee provideremployee)
        {
            if (id != provideremployee.Provideremployeeid)
            {
                return BadRequest();
            }

            _context.Entry(provideremployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvideremployeeExists(id))
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

        // POST: api/Provideremployee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Provideremployee>> CreateProvideremployee(Provideremployee
                            provideremployee)
        {
            _context.Provideremployees.Add(provideremployee);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProvideremployeeExists(provideremployee.Provideremployeeid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProvideremployee", new { id = provideremployee.Provideremployeeid },
                            provideremployee);
        }

        // DELETE: api/Provideremployee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvideremployeeById(int id)
        {
            var provideremployee = await _context.Provideremployees.FindAsync(id);
            if (provideremployee == null)
            {
                return NotFound();
            }

            _context.Provideremployees.Remove(provideremployee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProvideremployeeExists(int id)
        {
            return _context.Provideremployees.Any(e => e.Provideremployeeid == id);
        }
    }
}
