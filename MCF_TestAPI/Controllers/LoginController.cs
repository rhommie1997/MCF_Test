using AutoMapper;
using MCF_TestAPI.Data;
using MCF_TestAPI.Models.Dto;
using MCF_TestAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MCF_TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MCF_TestAPI_DbContext _db;
        private ResponseDto _res;
        private IMapper _mapper;
        private readonly IConfiguration _config;

        public LoginController(MCF_TestAPI_DbContext db, IMapper mapper, IConfiguration config)
        {
            _db = db;
            _mapper = mapper;
            _res = new ResponseDto();
            _config = config;
        }

        [HttpPost]
        public ResponseDto Login(UserLoginViewModel user)
        {

            var uvm = new UserViewModel();

            if (user != null)
            {
                var getUser = _db.ms_users.Where(x => x.user_name == user.user_name).FirstOrDefault();
                if (getUser == null)
                {
                    _res.IsSuccess = false;
                    _res.Message = "Invalid Username";
                }
                else
                {
                    if (getUser.password != user.password)
                    {
                        _res.IsSuccess = false;
                        _res.Message = "Invalid Password";
                    }
                    else
                    {
                        _res.IsSuccess = true;

                        uvm.user_id = getUser.user_id;
                        uvm.user_name = getUser.user_name;
                        uvm.password = getUser.password;
                        uvm.isActive = getUser.isActive;
                        uvm.Token = GetToken(uvm);

                        var principal = ValidateToken(uvm.Token);

                        _res.Result = uvm;
                        _res.Message = "Login successful";
                    }
                }
            }

            return _res;
        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidAudience = _config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
                };

                // Validasi token
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                return principal;
            }
            catch (Exception ex)
            {
                // Tangani kesalahan validasi token
                // Log ex.Message atau lakukan penanganan kesalahan sesuai kebutuhan
                return null;
            }
        }

        string GetToken(UserViewModel user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub,_config["jwt:Subject"] ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString()),
                new Claim("user_name",user.user_name ?? ""),
                new Claim("Password",user.password ?? ""),
                new Claim("isActive",user.isActive.ToString()),
                new Claim("user_id",user.user_id.ToString()),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? ""));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signIn);

            string Token = new JwtSecurityTokenHandler().WriteToken(token);

            return Token;
        }

    }
}
