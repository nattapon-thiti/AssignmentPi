using Pi.Interfaces.Repositories.Users;
using Pi.Interfaces.Services.Users;
using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Users;
using Pi.Models.ResponseModels.Users;
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
            try
            {
                var result = await _userRepositories.DeleteAsync(request);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<PiUser>> GetUsers(string? request)
        {
            try
            {
                IEnumerable<PiUser> result = await _userRepositories.GetAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CreateUserResponse> CreateOrUpdateUsers(UserCreateOrUpdateRequest request)
        {
            try
            {
                var result = await _userRepositories.CreateOrUpdateAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
