using MedidorLoginNew.Filters;
using MedidorLoginNew.Models;
using MedidorLoginNew.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MedidorLoginNew.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> LogIn(Login model)
        {
            //0.- Validation
            if (!await _identityService.UserExists(model.Username)) return NotFound();

            //1.- Log in
            if (!await _identityService.LogIn(model)) return NotFound();

            //2.- Return bearer token
            return Ok(await _identityService.GetJwtToken(model));
        }
    }
}