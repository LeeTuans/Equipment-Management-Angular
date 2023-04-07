using ApiEquipment.Dto;
using ApiEquipment.Entities;
using ApiEquipment.GlobalClass;
using ApiEquipment.Helpers;
using ApiEquipment.Interfaces;
using ApiEquipment.Services.Interfaces;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NuGet.Common;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DemoJWTSwaggerData.Controllers
{
    [Route("api/[Action]")]
    [ApiController]
    public class JWTTokenController : ControllerBase
    {
        public IConfiguration _configuration;
        public readonly IUnitOfWork _unitOfWork;
        public readonly IEmployeeService _employeeService;

        public JWTTokenController(IConfiguration configuration, IUnitOfWork unitOfWork, IEmployeeService employeeService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _employeeService = employeeService;
        }
        [HttpPost]

        public async Task<ActionResult<Respone>> Login([FromBody] DtoLogin employeeLogin)
        {
            if (employeeLogin != null && employeeLogin.Email != null && employeeLogin.Password != null)
            {
                var employeeData = await Authenticate(employeeLogin);

                if (employeeData != null)
                {
                    if (employeeData.IsBan == true)
                    {
                        return BadRequest(new Respone()
                        {
                            Status = false,

                            Message = "Employee is ban!",
                        });
                    }
                    //var infor = new
                    //{
                    //    school = "CTu",
                    //    address = "Can Tho",
                    //    phone = "021321312",
                    //    age = "10",
                    //    brand = "Flxu",
                    //    vi = "car",
                    //};
                    //var inforJson = JsonConvert.SerializeObject(infor);
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim("Id", employeeData.EmployeeId.ToString()),
                        new Claim("Email", employeeData.Email),
                        //new Claim("Info", inforJson),
                    };

                    var empRole = employeeData.EmployeeRoles;
                    if (empRole != null)
                    {
                        foreach (var er in empRole)
                        {
                            if (er.Role != null)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, er.Role.RoleName));
                            }
                        }
                    }
                    
                    var token = GenerateToken(claims);
                    var refreshToken = GenerateRefreshToken();

                    //_ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                    var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

                    employeeData.RefreshToken = refreshToken;

                    employeeData.RefreshTokenExpiryTime = DateTime.Now.AddDays(jwt.RefreshTokenValidityInDays);

                    _unitOfWork.GetRepo<IEmployeeRepo>().Update(employeeData);
                    var result = _unitOfWork.Save();
                    if (result <= 0)
                    {
                        return BadRequest(new Respone()
                        {
                            Status = false,

                            Message = "Incomplete data!",
                        });
                    }

                    return Ok(new
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        RefreshToken = refreshToken,
                        Expiration = token.ValidTo
                    });
                    
                }
                
                return NotFound(new Respone()
                {Status = false,
                    Message = "Login information is incorrect!",
                });
            }
            else
                return BadRequest(new Respone()
                {
                    Status = false,

                    Message = "Incomplete data!",
                });

        }
        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;
            ClaimsPrincipal? principal = null;
            try
            {
                principal = GetPrincipalFromExpiredToken(accessToken);
            }
            catch
            {
                return BadRequest("Invalid access token or refresh token");
            }
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Claim idClaim = principal.Claims.FirstOrDefault(c => c.Type == "Id");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (idClaim == null)
            {
                return BadRequest("Employee ID not found.");
            }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            
            int employeeId = int.Parse(idClaim.Value);
            var employee = await _unitOfWork.GetRepo<IEmployeeRepo>().GetById(employeeId);

            if (employee == null || employee.RefreshToken != refreshToken || employee.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = GenerateToken(principal.Claims.ToList());
            
            var newRefreshToken = GenerateRefreshToken();

            employee.RefreshToken = newRefreshToken;
            _unitOfWork.GetRepo<IEmployeeRepo>().Update(employee);
            var result = _unitOfWork.Save();
            if (result <= 0)
            {
                return BadRequest(new Respone()
                {
                    Status = false,

                    Message = "Incomplete data!",
                });
            }
            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }
        private async Task<Employee?> Authenticate([FromBody] DtoLogin employeeLogin)
        {
            var employeeData = await _employeeService.LoginEmployee(employeeLogin.Email, employeeLogin.Password);
            if (employeeData == null)
            {
                return null;

            }
            return employeeData;

        }
        private JwtSecurityToken GenerateToken(List<Claim> authClaims)
        {
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            var aud = authClaims.Where(c => c.Type.Equals("aud")).FirstOrDefault();
            if (aud != null)
            {
                authClaims.Remove(aud);
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
            var token = new JwtSecurityToken(
                // Header
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                // Payload
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(jwt.TokenValidityInMinutes),
                // Signing Key

                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }
        //private JwtSecurityToken CreateToken(List<Claim> authClaims)
        //{
       
        //    _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

        //    var token = new JwtSecurityToken(
        //        issuer: _configuration["JWT:ValidIssuer"],
        //        audience: _configuration["JWT:ValidAudience"],
        //        expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
        //        claims: authClaims,
        //        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //        );

        //    return token;
        //}

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            
            return principal;

        }
        //[HttpPost]
        //[Authorize]
        //public ActionResult<Respone> Logout()
        //{
        //    // Delete the JWT token from the cookie of the current user
        //    if (Request.Cookies.TryGetValue("jwt", out string jwt))
        //    {
        //        Response.Cookies.Delete("jwt");
        //    }

        //    return Ok(new Respone()
        //    {Status = true,
        //        Message = "Logout success!",
        //    });
        //}
        //[HttpGet()]

        //public async Task<ActionResult<Respone>> LoginFrom([FromForm] DtoLogin employeeLogin) 
        //{
        //    if (employeeLogin != null && employeeLogin.Email != null && employeeLogin.Password != null)
        //    {
        //        var employeeData = await Authenticate(employeeLogin);
        //        if (employeeData != null)
        //        {
        //            var token = GenerateToken(employeeData);
        //            return Ok(new Respone()
        //            {
        //                Status = true,

        //                Message = "Login success!",
        //                Data = token
        //            });
        //        }

        //        return NotFound(new Respone()
        //        {Status = false,
        //            Message = "Login information is incorrect!",
        //        });
        //    }
        //    else
        //        return BadRequest(new Respone()
        //        {Status = false,
        //            Message = "Incomplete data!",
        //        });

        //}
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Respone>> GetCurrentInforEmployee()
        {
            var employeeIdPayload = User.FindFirstValue("Id");
            //if (string.IsNullOrEmpty(jwtPayload))
            //{
            //    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    return;
            //}
            if (string.IsNullOrEmpty(employeeIdPayload))
            {
               return BadRequest(new Respone()
               {
                   Status = false,

                   Message = "please log in!",
               });
            }
            var employeeId = int.Parse(employeeIdPayload);
            var employeeData = await _employeeService.GetById(employeeId);
            return (new Respone()
            {
                Status = true,
                Message = "Current employee info!",
                Data = employeeData
            });
            
        }
        //var email = "";
        //var emailClaim = User.Claims.FirstOrDefault(c => c.Type == "Email");
        //if (emailClaim != null)
        //{
        //     email = emailClaim.Value;
        //}
        //var emailClaimType = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        //if (emailClaimType != null)
        //{
        //    email = emailClaimType.Value;
        //}
        //var roles = User.Claims
        //                .Where(c => c.Type == ClaimTypes.Role)
        //                .Select(c => c.Value)
        //                .ToList();
        //return BadRequest(new Respone()
        //{
        //    Status = false,
        //    Message = "Incomplete data!",
        //    Data = roles
        //});

    }
    
}

        
        
 