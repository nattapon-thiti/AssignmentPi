using Pi.Interfaces.Repositories.Users;
using Pi.Interfaces.Services.Users;
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
        public Task<string> DeleteUsers()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateUsers()
        {
            throw new NotImplementedException();
        }
    }
}
