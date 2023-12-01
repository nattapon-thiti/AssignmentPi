using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pi.Interfaces.Services.Admin;
using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Admin;
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
        
        [AllowAnonymous]
        [HttpPost]
        [Route("Register-User")]
        public async Task<IActionResult> Register(RegisterUserRequest req)
        {
            try
            {
                var result = await _adminServices.RegisterUser(req);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse(false, ex.Message));
            }

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            try
            {
                var result = await _adminServices.ValidateLogin(req);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse(false, ex.Message));
            }

        }
    }
}
