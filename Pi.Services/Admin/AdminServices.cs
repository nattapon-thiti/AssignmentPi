using Microsoft.Extensions.Configuration;
using Pi.Interfaces.Repositories.Admin;
using Pi.Interfaces.Services.Admin;
using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Users;
using Pi.Models.ResponseModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Services.Admin
{
    public class AdminServices : IAdminServices
    {
        private readonly IAdminRepositories _adminRepositories;
        public AdminServices(IAdminRepositories adminRepositories)
        {
            _adminRepositories = adminRepositories;
        }
        public async Task<IEnumerable<PiUser>> GetUsers(string? request)
        {
            try
            {
                IEnumerable<PiUser> result = await _adminRepositories.GetAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
