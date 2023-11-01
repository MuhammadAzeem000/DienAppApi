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
    public class JobrequestController : ControllerBase
    {
        private readonly DIENAPPRESTAPIContext _context;

        public JobrequestController(DIENAPPRESTAPIContext context)
        {
            _context = context;
        }

        // GET: api/Jobrequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jobrequest>>> GetAllJobrequests()
        {
            return await _context.Jobrequests.ToListAsync();
        }

        // GET: api/Jobrequest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jobrequest>> GetJobrequestById(int id)
        {
            var jobrequest = await _context.Jobrequests.FindAsync(id);

            if (jobrequest == null)
            {
                return NotFound();
            }

            return jobrequest;
        }

        // PUT: api/Jobrequest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobrequestById(int id,
                        Jobrequest jobrequest)
        {
            if (id != jobrequest.Jobrequestid)
            {
                return BadRequest();
            }

            _context.Entry(jobrequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobrequestExists(id))
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

        // POST: api/Jobrequest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Jobrequest>> CreateJobrequest(Jobrequest
                            jobrequest)
        {
            _context.Jobrequests.Add(jobrequest);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobrequestExists(jobrequest.Jobrequestid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJobrequest", new { id = jobrequest.Jobrequestid },
                            jobrequest);
        }

        // DELETE: api/Jobrequest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobrequestById(int id)
        {
            var jobrequest = await _context.Jobrequests.FindAsync(id);
            if (jobrequest == null)
            {
                return NotFound();
            }

            _context.Jobrequests.Remove(jobrequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobrequestExists(int id)
        {
            return _context.Jobrequests.Any(e => e.Jobrequestid == id);
        }
    }
}
