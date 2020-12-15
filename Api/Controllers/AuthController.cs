using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService tokenService;

        public AuthController(TokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        [HttpGet("generate/token/to/user")]
        public IActionResult GenerateTokenToUser()
        {
            return Ok(tokenService.GenerateTokenTo("User"));
        }

        [HttpGet("generate/token/to/admin")]
        public IActionResult GenerateTokenToAdmin()
        {
            return Ok(tokenService.GenerateTokenTo("Admin"));
        }
    }
}