using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Interfaces.Services.Users
{
    public interface IUserServices
    {
        Task<IEnumerable<PiUser>> GetUsers();
        Task<bool> CreateOrUpdateUsers(UserCreateOrUpdateRequest request);
        Task<bool> DeleteUsers(int request);
    }
}
