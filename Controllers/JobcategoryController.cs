using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DienappApi.Data;
using DienappApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace DienappApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobcategoryController : ControllerBase
    {
        private readonly DIENAPPRESTAPIContext _context;

        public JobcategoryController(DIENAPPRESTAPIContext context)
        {
            _context = context;
        }

        // GET: api/Jobcategory
        [HttpGet ]
        public async Task<ActionResult<IEnumerable<Jobcategory>>> GetAllJobcategories()
        {
            return await _context.Jobcategories.ToListAsync();
        }

        // GET: api/Jobcategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jobcategory>> GetJobcategoryById(int id)
        {
            var jobcategory = await _context.Jobcategories.FindAsync(id);

            if (jobcategory == null)
            {
                return NotFound();
            }

            return jobcategory;
        }

        // PUT: api/Jobcategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobcategoryById(int id,
                        Jobcategory jobcategory)
        {
            if (id != jobcategory.Jobcategoryid)
            {
                return BadRequest();
            }

            _context.Entry(jobcategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobcategoryExists(id))
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

        // POST: api/Jobcategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Jobcategory>> CreateJobcategory(Jobcategory
                            jobcategory)
        {
            _context.Jobcategories.Add(jobcategory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobcategoryExists(jobcategory.Jobcategoryid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJobcategory", new { id = jobcategory.Jobcategoryid },
                            jobcategory);
        }

        // DELETE: api/Jobcategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobcategoryById(int id)
        {
            var jobcategory = await _context.Jobcategories.FindAsync(id);
            if (jobcategory == null)
            {
                return NotFound();
            }

            _context.Jobcategories.Remove(jobcategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobcategoryExists(int id)
        {
            return _context.Jobcategories.Any(e => e.Jobcategoryid == id);
        }
    }
}
