using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Interfaces.Repositories.Users
{
    public interface IUserRepositories
    {
        Task<IEnumerable<PiUser>> GetAsync();
        Task<bool> CreateOrUpdateAsync(UserCreateOrUpdateRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
