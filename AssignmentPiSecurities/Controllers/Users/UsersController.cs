using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pi.Interfaces.Services.Users;
using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Users;
using Pi.Models.ResponseModels;
using Pi.Models.ResponseModels.Users;
using System.Collections.Generic;

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
        [Authorize]
        public async Task<IActionResult> GetUsers(string? request)
        {
            try
            {
                var response = await _userServices.GetUsers(request);
                return Ok(new GetUserResponse(response));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse(false, ex.Message));
            }

        }
        [HttpPost]
        [Route("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdateUser(UserCreateOrUpdateRequest request)
        {
            try
            {
                var response = await _userServices.CreateOrUpdateUser(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse(false, ex.Message));
            }

        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteUsers(int request)
        {
            try
            {
                var response = await _userServices.DeleteUsers(request);
                if (response)
                {
                    return Ok(new BaseResponse(true, $"User Id {request} deleted"));
                }
                else
                {
                    return Ok(new BaseResponse(false, $"User Id {request} not found"));
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse(false, ex.Message));
            }
        }
    }
}
