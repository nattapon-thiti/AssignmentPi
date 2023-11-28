using Pi.Interfaces.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Repositories.Users
{
    public class UsersRepositories : IUserRepositories
    {
        public UsersRepositories()
        {

        }

        public Task<string> CreateOrUpdateAsync(string? id)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteAsync(string? id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAsync(string? id)
        {
            throw new NotImplementedException();
        }
    }
}
