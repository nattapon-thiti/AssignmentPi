using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pi.Interfaces.Services.Admin;
using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Users;
using Pi.Models.ResponseModels;
using Pi.Models.ResponseModels.Users;
using Pi.Services.Admin;
using System.Collections.Generic;

namespace AssignmentPiSecurities.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminServices;
        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetUsers(string? request)
        {
            try
            {
                var response = await _adminServices.GetUsers(request);
                return Ok(new GetUserResponse(response));
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse(false, ex.Message));
            }
        }
    }
}
