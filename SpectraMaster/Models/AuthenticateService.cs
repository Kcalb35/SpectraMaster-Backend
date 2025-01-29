using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SpectraMaster.Controllers;
using SpectraMaster.Data;

namespace SpectraMaster.Models
{
    public class AuthenticateService:IAuthentiacteService
    {
        private ManagerDbContext _context;
        private TokenManagement _token;

        public AuthenticateService(IOptions<TokenManagement> opt,ManagerDbContext context)
        {
            _context = context;
            _token = opt.Value;
        }
        
        public bool IsAuthenticate(LoginReq req, out string jwt)
        {
            var mgr = _context.Managers.FirstOrDefault(x => x.Username == req.Username);
            // username or password incorrect
            if (mgr == null || req.Password != mgr.Password)
            {
                jwt = null;
                return false;
            }
            else
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Exp,
                        $"{new DateTimeOffset(DateTime.Now.AddHours(24)).ToUnixTimeSeconds()}"),
                    new Claim(ClaimTypes.Name, req.Username)
                };
                var secret = _token.Secret;
                var issuer = _token.Issuer;
                var audience = _token.Audience;
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims:claims,
                    expires:DateTime.UtcNow.AddHours(24),
                    signingCredentials:creds
                );
                jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return true;
            }
        }
    }
}