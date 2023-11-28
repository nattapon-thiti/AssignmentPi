using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Interfaces.Repositories.Users
{
    public interface IUserRepositories
    {
        Task<string> GetAsync(string? id);
        Task<string> CreateOrUpdateAsync(string? id);
        Task<string> DeleteAsync(string? id);
    }
}
