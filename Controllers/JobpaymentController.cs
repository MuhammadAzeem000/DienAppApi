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
    public class JobpaymentController : ControllerBase
    {
        private readonly DIENAPPRESTAPIContext _context;

        public JobpaymentController(DIENAPPRESTAPIContext context)
        {
            _context = context;
        }

        // GET: api/Jobpayment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jobpayment>>> GetAllJobpayments()
        {
            return await _context.Jobpayments.ToListAsync();
        }

        // GET: api/Jobpayment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jobpayment>> GetJobpaymentById(int id)
        {
            var jobpayment = await _context.Jobpayments.FindAsync(id);

            if (jobpayment == null)
            {
                return NotFound();
            }

            return jobpayment;
        }

        // PUT: api/Jobpayment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobpaymentById(int id,
                        Jobpayment jobpayment)
        {
            if (id != jobpayment.JobpaymentId)
            {
                return BadRequest();
            }

            _context.Entry(jobpayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobpaymentExists(id))
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

        // POST: api/Jobpayment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Jobpayment>> CreateJobpayment(Jobpayment
                            jobpayment)
        {
            _context.Jobpayments.Add(jobpayment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobpaymentExists(jobpayment.JobpaymentId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJobpayment", new { id = jobpayment.JobpaymentId },
                            jobpayment);
        }

        // DELETE: api/Jobpayment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobpaymentById(int id)
        {
            var jobpayment = await _context.Jobpayments.FindAsync(id);
            if (jobpayment == null)
            {
                return NotFound();
            }

            _context.Jobpayments.Remove(jobpayment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobpaymentExists(int id)
        {
            return _context.Jobpayments.Any(e => e.JobpaymentId == id);
        }
    }
}
