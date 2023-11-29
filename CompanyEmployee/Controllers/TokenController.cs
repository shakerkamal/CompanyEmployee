using CompanyEmployee.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Threading.Tasks;

namespace CompanyEmployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public TokenController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Refresh([FromBody]TokenDto token)
        {
            var tokenToReturn = await _serviceManager.AutheticationService.RefreshToken(token);

            return Ok(tokenToReturn);
        }

    }
}
