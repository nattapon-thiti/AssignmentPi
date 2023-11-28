using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentPiSecurities.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [Route("CreateOrUpdateUsers")]
        public async Task<IActionResult> UpdateUsers()
        {
            throw new NotImplementedException();
        }
        [HttpDelete]
        [Route("DeleteUsers")]
        public async Task<IActionResult> DeleteUsers()
        {
            throw new NotImplementedException();
        }
    }
}
