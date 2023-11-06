using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DienappApi.Data;
using DienappApi.Models.SPModels;
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
        public async Task<ActionResult<IEnumerable<GetAllJobs>>> GetAllJobs()
        {
            return await _context.GetAllJobs.FromSqlRaw("CALL sp_getAlljob").ToListAsync();
        }

        // GET: api/Jobcategory
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Seeker>>> GetSeekerInfoById(int id)
        {
            return await _context.Seekers.FromSql($"CALL sp_GetSeekerInfoby_Id ({id})").ToListAsync();
        }
    }
}
