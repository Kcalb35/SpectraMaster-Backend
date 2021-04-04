using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SpectraMaster.Data;
using SpectraMaster.Models;
using SpectraMaster.Utils;

namespace SpectraMaster.Controllers
{

    [EnableCors]
    [AllowAnonymous]
    [ApiController]
    [Route("api")]
    public class LoginController : ControllerBase 
    {
        private ManagerDbContext _context;
        private IAuthentiacteService _auth;

        public LoginController(ManagerDbContext context,IAuthentiacteService service)
        {
            _context = context;
            _auth = service;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginReq req)
        {
            var flag = _auth.IsAuthenticate(req, out var jwt);
            if (!flag) return NotFound(ApiResponse.Error("incorrect username or password"));
            else return Ok(ApiResponse.Success(jwt));
        }
    }
}