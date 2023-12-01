using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Users;
using Pi.Models.ResponseModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Interfaces.Repositories.Users
{
    public interface IUserRepositories
    {
        Task<IEnumerable<PiUser>> GetAsync(string? request);
        Task<PiUser> GetAsync(int request);
        Task<CreateUserResponse> CreateOrUpdateAsync(UserCreateOrUpdateRequest request);
        Task<PiUser> CreateUser(UserCreateOrUpdateRequest request);
        Task<bool> UpdateUser(UserCreateOrUpdateRequest request, PiUser user);
        Task<bool> DeleteAsync(int id);
    }
}
