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
    public class RateController : ControllerBase
    {
        private readonly DIENAPPRESTAPIContext _context;

        public RateController(DIENAPPRESTAPIContext context)
        {
            _context = context;
        }

        // GET: api/Rate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rate>>> GetAllRates()
        {
            return await _context.Rates.ToListAsync();
        }

        // GET: api/Rate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rate>> GetRateById(int id)
        {
            var rate = await _context.Rates.FindAsync(id);

            if (rate == null)
            {
                return NotFound();
            }

            return rate;
        }

        // PUT: api/Rate/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRateById(int id, Rate rate)
        {
            if (id != rate.Rateid)
            {
                return BadRequest();
            }

            _context.Entry(rate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RateExists(id))
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

        // POST: api/Rate
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rate>> CreateRate(Rate rate)
        {
            _context.Rates.Add(rate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RateExists(rate.Rateid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRate", new { id = rate.Rateid }, rate);
        }

        // DELETE: api/Rate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRatebyId(int id)
        {
            var rate = await _context.Rates.FindAsync(id);
            if (rate == null)
            {
                return NotFound();
            }

            _context.Rates.Remove(rate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RateExists(int id)
        {
            return _context.Rates.Any(e => e.Rateid == id);
        }
    }
}
