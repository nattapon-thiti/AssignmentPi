using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Admin;
using Pi.Models.RequestModels.Users;
using Pi.Models.ResponseModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Interfaces.Services.Admin
{
    public interface IAdminServices
    {
        Task<string> GenToken();
        Task<bool> RegisterUser(RegisterUserRequest req);
        Task<string> ValidateLogin(LoginRequest req);
    }
}
