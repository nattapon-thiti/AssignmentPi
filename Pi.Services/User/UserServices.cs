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

namespace Pi.Services.User
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepositories _userRepositories;
        public UserServices(IUserRepositories userRepositories)
        {
            _userRepositories = userRepositories;
        }
        public async Task<bool> DeleteUser(int request)
        {
            try
            {
                var result = await _userRepositories.DeleteAsync(request);
                return result;
            }
            catch (Exception ex)
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
        public async Task<CreateUserResponse> CreateOrUpdateUser(UserCreateOrUpdateRequest request)
        {
            try
            {
                CreateUserResponse result = new CreateUserResponse();

                if (request?.Id != null)
                {
                    #region create new user
                    PiUser user = await _userRepositories.GetAsync(request.Id.Value);
                    if (user != null)
                    {
                        bool updatedUser = await _userRepositories.UpdateUser(request, user);
                        result = new CreateUserResponse(updatedUser, user, $"User id {user.Id} updated");
                    }
                    else
                    {
                        result = new CreateUserResponse(false, user, $"User id {request.Id} not found");
                    }
                    return result;
                    #endregion
                }
                else
                {
                    #region create new user
                    PiUser createdUser = await _userRepositories.CreateUser(request);
                    return new CreateUserResponse(true, createdUser, $"User id {createdUser.Id} created");
                    #endregion
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
