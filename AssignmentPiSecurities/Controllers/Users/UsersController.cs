using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pi.Interfaces.Services.Users;

namespace AssignmentPiSecurities.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _userServices.GetUsers();
            return Ok(response);
        }
        [HttpPost]
        [Route("CreateOrUpdate")]
        public async Task<IActionResult> UpdateUsers()
        {
            var response = await _userServices.UpdateUsers();
            return Ok(response);
        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteUsers()
        {
            var response = await _userServices.DeleteUsers();
            return Ok(response);
        }
    }
}
