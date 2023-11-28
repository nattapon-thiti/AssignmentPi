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
            try
            {
                var response = await _userServices.GetUsers();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        [Route("CreateOrUpdate")]
        public async Task<IActionResult> UpdateUsers()
        {
            try
            {
                var response = await _userServices.UpdateUsers();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteUsers()
        {
            try
            {
                var response = await _userServices.DeleteUsers();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
