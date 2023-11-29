using Pi.Interfaces.Repositories.Users;
using Pi.Interfaces.Services.Users;
using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Services.UserServices
{
    public class UserServices : IUserServices
    {
        readonly IUserRepositories _userRepositories;
        public UserServices(IUserRepositories userRepositories)
        {
            _userRepositories = userRepositories;
        }
        public async Task<bool> DeleteUsers(int request)
        {
            var result = await _userRepositories.DeleteAsync(request);
            return result;
        }

        public async Task<IEnumerable<PiUser>> GetUsers(string? request)
        {
            IEnumerable<PiUser> result = await _userRepositories.GetAsync(request);
            return result;
        }

        public async Task<bool> CreateOrUpdateUsers(UserCreateOrUpdateRequest request)
        {
            var result = await _userRepositories.CreateOrUpdateAsync(request);
            return result;
        }
    }
}
