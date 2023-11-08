using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DienappApi.Models;
using DienappApi.Services;
using DienappApi.Interfaces;
using System.Globalization;

namespace DienappApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IEmailService emailService,
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser, string role)
        {

            //Check User Exist 
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);

            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "User already exists!" });
            }

            if (userExist.PhoneNumber == registerUser.PhoneNumber)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                     new Response { Status = "Error", Message = "Phone Number already exists!" });
            }


            //Add the User in the database
            ApplicationUser user = new()
            {
                Name = registerUser.Name,
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.PhoneNumber,
                PhoneNumber = registerUser.PhoneNumber,
                TwoFactorEnabled = true
            };

            string autoPassword = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(registerUser.Name) + "@" + registerUser.PhoneNumber;

            if (await _roleManager.RoleExistsAsync(role))
            {
                if (registerUser.Email != null)
                {
                    var result = await _userManager.CreateAsync(user, autoPassword);
                    if (!result.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            new Response { Status = "Error", Message = "User Failed to Create" });
                    }
                }
                //Add role to the user....

                await _userManager.AddToRoleAsync(user, role);

                //Add Token to Verify the email....
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink!);
                _emailService.SendEmail(message);



                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = $"User created & Email Sent to {user.Email} SuccessFully" });

            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { Status = "Error", Message = "This Role Doesnot Exist." });
            }


        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                      new Response { Status = "Success", Message = "Email Verified Successfully" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response { Status = "Error", Message = "This User Doesnot exist!" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel.PhoneNumber != null)
            {
                var user = await _userManager.FindByNameAsync(loginModel.PhoneNumber);


                if (user != null && user.TwoFactorEnabled)
                {
                    await _signInManager.SignOutAsync();
                    string autoPassword = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.Name) + "@" + user.PhoneNumber;
                    await _signInManager.PasswordSignInAsync(user, autoPassword, false, true);

                    var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

                    var message = new Message(new string[] { user.Email! }, "OTP Confrimation", token);
                    _emailService.SendEmail(message);

                    return StatusCode(StatusCodes.Status200OK,
                     new Response { Status = "Success", Message = $"We have sent an OTP to your Email {user.Email}" });
                }

            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("login-2FA")]
        public async Task<IActionResult> LoginWithOTP(string code, string PhoneNumber)
        {
            var user = await _userManager.FindByNameAsync(PhoneNumber);
            var signIn = await _signInManager.TwoFactorSignInAsync("Email", code, false, false);


            if (signIn.Succeeded)
            {
                if (user != null && user.UserName != null)
                {
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                    var userRoles = await _userManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var jwtToken = GetToken(authClaims);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        expiration = jwtToken.ValidTo
                    });
                    //returning the token...

                }
            }
            return StatusCode(StatusCodes.Status404NotFound,
                new Response { Status = "Not Found", Message = $"Invalid Code" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

    }
}