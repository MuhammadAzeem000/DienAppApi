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
    public class SPController : ControllerBase
    {
        private readonly DIENAPPRESTAPIContext _context;

        public SPController(DIENAPPRESTAPIContext context)
        {
            _context = context;
        }

        // GET: api/Jobcategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetAllJobs()
        {
            return await _context.Jobs.FromSqlRaw("CALL sp_getAlljob").ToListAsync();
        }
    }
}
