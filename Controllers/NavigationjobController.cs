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
    public class NavigationjobController : ControllerBase
    {
        private readonly DIENAPPRESTAPIContext _context;

        public NavigationjobController(DIENAPPRESTAPIContext context)
        {
            _context = context;
        }

        // GET: api/Navigationjob
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Navigationjob>>> GetAllNavigationjobs()
        {
            return await _context.Navigationjobs.ToListAsync();
        }

        // GET: api/Navigationjob/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Navigationjob>> GetNavigationjobById(int id)
        {
            var navigationjob = await _context.Navigationjobs.FindAsync(id);

            if (navigationjob == null)
            {
                return NotFound();
            }

            return navigationjob;
        }

        // PUT: api/Navigationjob/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNavigationjobById(int id, Navigationjob navigationjob)
        {
            if (id != navigationjob.NavigationjobId)
            {
                return BadRequest();
            }

            _context.Entry(navigationjob).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NavigationjobExists(id))
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

        // POST: api/Navigationjob
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Navigationjob>> CreateNavigationjob(Navigationjob navigationjob)
        {
            _context.Navigationjobs.Add(navigationjob);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NavigationjobExists(navigationjob.NavigationjobId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNavigationjob", new { id = navigationjob.NavigationjobId }, navigationjob);
        }

        // DELETE: api/Navigationjob/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNavigationjobById(int id)
        {
            var navigationjob = await _context.Navigationjobs.FindAsync(id);
            if (navigationjob == null)
            {
                return NotFound();
            }

            _context.Navigationjobs.Remove(navigationjob);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NavigationjobExists(int id)
        {
            return _context.Navigationjobs.Any(e => e.NavigationjobId == id);
        }
    }
}
