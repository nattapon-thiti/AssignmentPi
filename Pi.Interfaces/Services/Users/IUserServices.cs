using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Interfaces.Services.Users
{
    public interface IUserServices
    {
        Task<string> GetUsers();
        Task<string> UpdateUsers();
        Task<string> DeleteUsers();
    }
}
