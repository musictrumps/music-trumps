using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrumpEngine.Api.Models;
using TrumpEngine.Api.Security;
using FirebaseAuthException = FirebaseAdmin.Auth.FirebaseAuthException;

namespace TrumpEngine.Api.Controllers
{

    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
      
        [HttpPost]
        [Route("api/login")]
        public async Task<ActionResult> Login([FromBody] LoginApiRequest loginRequest)
        {
            try
            {
                var token = await _accountService.Authenticate(loginRequest);
                return Ok(token);
            }
            catch (FirebaseAuthException e)
            {
                return Problem(e.Message);
            }
        }
    }

}
