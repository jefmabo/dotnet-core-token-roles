using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthorizeController : ControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpGet("just/user/role")]
        public IActionResult JustUserRole()
        {
            return Ok(new { message = "Just User role is authorized!!!" });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("just/admin/role")]
        public IActionResult JustAdminRole()
        {
            return Ok(new { message = "Just Admin role is authorized!!!" });
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("user/and/admin/role")]
        public IActionResult UserAndAdmin()
        {
            return Ok(new { message = "Just User and Admin role is authorized!!!" });
        }

        [HttpGet("anyone/with/token")]
        public IActionResult AnyoneWithToken()
        {
            return Ok(new { message = "Anyone with token is authorized!!!" });
        }

        [AllowAnonymous]
        [HttpGet("anyone")]
        public IActionResult Anyone()
        {
            return Ok(new { message = "Anyone is authorized!!!" });
        }
    }
}