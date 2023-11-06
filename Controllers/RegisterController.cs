using DienappApi.Data;
using DienappApi.Models;
using DienappApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace DienappApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<Register> _userManager;
        private readonly SignInManager<Register> _signInManager;
        private readonly DIENAPPRESTAPIContext _context;
        private readonly ILogger _logger;

        public RegisterController(
            UserManager<Register> usermanager,
            SignInManager<Register> signInManager,
            ILogger<RegisterController> logger,
            DIENAPPRESTAPIContext context)
        {
            _userManager = usermanager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="model">User registration information</param>
        /// <returns>Registration result</returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new Register
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Handle successful registration, such as returning a token or redirecting.
                // You can also add the user to a specific role if needed.

                return Ok(new { Message = "Registration successful" });
            }
            else
            {
                // Handle registration failure (e.g., return error messages).
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(errors);
            }
        }

    }
}
