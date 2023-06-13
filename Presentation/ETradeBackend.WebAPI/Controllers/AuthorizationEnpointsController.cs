using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETradeBackend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEnpointsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return null;
        }
    }
}
