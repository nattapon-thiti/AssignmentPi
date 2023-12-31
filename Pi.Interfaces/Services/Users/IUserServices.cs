﻿using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Users;
using Pi.Models.ResponseModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Interfaces.Services.Users
{
    public interface IUserServices
    {
        Task<IEnumerable<PiUser>> GetUsers(string? request);
        Task<CreateUserResponse> CreateOrUpdateUser(UserCreateOrUpdateRequest request);
        Task<bool> DeleteUser(int request);
    }
}
