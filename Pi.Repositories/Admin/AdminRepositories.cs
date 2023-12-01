using Microsoft.EntityFrameworkCore;
using Pi.Interfaces.Repositories.Admin;
using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Users;
using Pi.Models.ResponseModels.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Repositories.Admin
{
    public class AdminRepositories : IAdminRepositories
    {
        readonly PiContext _context;
        public AdminRepositories(PiContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PiUser>> GetAsync(string? request)
        {
            try
            {
                return await _context.PiUsers.Where(o => request == null || o.GivenName.Contains(request)).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
