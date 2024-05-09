using api1.DTO;
using api1.Models;
using api1.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;
        private IConfiguration configuration;
        private RoleManager<IdentityRole> RoleManager;

        public AccountController(UserManager<ApplicationUser> userManager ,IConfiguration configuration, RoleManager<IdentityRole> roleManager) 
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.RoleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserDTO registerUserDTO)
        {
           
            if (registerUserDTO != null)
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = new ApplicationUser()
                    {
                        UserName = registerUserDTO.UserName,
                        Email = registerUserDTO.Email,
                        PasswordHash = registerUserDTO.Password,
                    };

                   IdentityResult result = await userManager.CreateAsync(user , registerUserDTO.Password);

                    if(result.Succeeded)
                    {
                        //add role

                        if (!RoleManager.RoleExistsAsync("ADMIN").Result)
                        {
                            var adminRole = new IdentityRole
                            {
                                Name = "ADMIN"
                            };
                            RoleManager.CreateAsync(adminRole).Wait();
                        }

                        if (!RoleManager.RoleExistsAsync("USER").Result)
                        {
                            var userRole = new IdentityRole
                            {
                                Name = "USER"
                            };
                            RoleManager.CreateAsync(userRole).Wait();
                        }


                        IdentityResult ResultRole = await userManager.AddToRoleAsync(user, registerUserDTO.Role);

                        if(ResultRole == null)
                        {
                            return BadRequest("role didn't work");
                        }
                        ///////////
                        ///
                        return Ok(result);
                    }
                    return BadRequest(result.Errors);
                }

                return BadRequest(ModelState);
            }
            return BadRequest(ModelState);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO userDTO)
        {
            if (userDTO != null)
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser UserDb = await userManager.FindByNameAsync(userDTO.UserName);
                    if(UserDb != null)
                    {
                        bool found = await userManager.CheckPasswordAsync(UserDb , userDTO.Password);
                        if(found)
                        {
                            //create token
                            
                            //create claims
                            List<Claim> claims = new List<Claim>();
                            claims.Add(new Claim(ClaimTypes.Name, UserDb.UserName));
                            claims.Add(new Claim(ClaimTypes.NameIdentifier, UserDb.Id));
                            claims.Add(new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                            var roles = await userManager.GetRolesAsync(UserDb);

                            foreach (var role in roles)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, role));
                            }

                            //security

                            var SignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("any words w khalas"));
                            SigningCredentials signingCredentials = new SigningCredentials(SignKey, SecurityAlgorithms.HmacSha256);

                            JwtSecurityToken MyToken = new JwtSecurityToken(
                                issuer: configuration["JWT,ValidIss"],
                                audience: configuration["JWT,ValidAud"],
                                expires: DateTime.Now.AddHours(3),
                                claims:claims,
                                signingCredentials:signingCredentials
                                );

                            return Ok(new
                            {
                                Token = MyToken,
                                Expired = MyToken.ValidTo
                            });

                        }
                        return BadRequest("Wrong Password");

                    }
                    return BadRequest(ModelState);

                }
            }
            return BadRequest(ModelState);
        }

       
    }
}
