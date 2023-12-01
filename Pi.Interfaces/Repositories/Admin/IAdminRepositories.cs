using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Users;
using Pi.Models.ResponseModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Interfaces.Repositories.Admin
{
    public interface IAdminRepositories
    {
        Task<IEnumerable<PiUser>> GetAsync(string? request);
    }
}
