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
public class ManagmentcompanyController : ControllerBase
{
private readonly DIENAPPRESTAPIContext _context;

public ManagmentcompanyController(DIENAPPRESTAPIContext context)
{
_context = context;
}

// GET: api/Managmentcompany
[HttpGet]
public async Task<ActionResult<IEnumerable<Managmentcompany>>> GetAllManagmentcompanies()
    {
    return await _context.Managmentcompanies.ToListAsync();
    }

    // GET: api/Managmentcompany/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Managmentcompany>> GetManagmentcompanyById(int id)
        {
        var managmentcompany = await _context.Managmentcompanies.FindAsync(id);

        if (managmentcompany == null)
        {
        return NotFound();
        }

        return managmentcompany;
        }

        // PUT: api/Managmentcompany/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManagmentcompanyById(int id,
                        Managmentcompany managmentcompany)
            {
            if (id != managmentcompany.ManagmentId)
            {
            return BadRequest();
            }

            _context.Entry(managmentcompany).State = EntityState.Modified;

            try
            {
            await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            if (!ManagmentcompanyExists(id))
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

            // POST: api/Managmentcompany
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<ActionResult<Managmentcompany>> CreateManagmentcompany(Managmentcompany
                                managmentcompany)
                {
                _context.Managmentcompanies.Add(managmentcompany);
                                        try
                                        {
                                        await _context.SaveChangesAsync();
                                        }
                                        catch (DbUpdateException)
                                        {
                                        if (ManagmentcompanyExists(managmentcompany.ManagmentId))
                                        {
                                        return Conflict();
                                        }
                                        else
                                        {
                                        throw;
                                        }
                                        }

                return CreatedAtAction("GetManagmentcompany", new { id = managmentcompany.ManagmentId },
                                managmentcompany);
                }

                // DELETE: api/Managmentcompany/5
                [HttpDelete("{id}")]
                public async Task<IActionResult> DeleteManagmentcompanyById(int id)
                    {
                    var managmentcompany = await _context.Managmentcompanies.FindAsync(id);
                    if (managmentcompany == null)
                    {
                    return NotFound();
                    }

                    _context.Managmentcompanies.Remove(managmentcompany);
                    await _context.SaveChangesAsync();

                    return NoContent();
                    }

                    private bool ManagmentcompanyExists(int id)
                    {
                    return _context.Managmentcompanies.Any(e => e.ManagmentId == id);
                    }
                    }
                    }
