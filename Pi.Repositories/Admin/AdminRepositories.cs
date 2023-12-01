using Microsoft.EntityFrameworkCore;
using Pi.Interfaces.Repositories.Admin;
using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Admin;
using Pi.Models.RequestModels.Users;
using Pi.Models.ResponseModels.Users;
using System;
using System.Collections.Generic;
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
        public async Task<PiAdmin> GetAsync(string? user, string? email)
        {
            try
            {
                return await _context.PiAdmins.FirstOrDefaultAsync(o => o.UserName.Equals(user) || o.Email.Equals(email));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> AddUser(PiAdmin user)
        {
            _context.PiAdmins.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> RegisterUser(RegisterUserRequest req)
        {
            try
            {
                if (GetAsync(req.UserName, req.Email) == null)
                {
                    PiAdmin user = new PiAdmin();
                    user.UserName = req.UserName;
                    user.Password = req.Password;
                    user.Email = req.Email;

                    //user.UpdatedUserId = .... // TODO : ONG - Update UserId
                    user.CreatedDate = DateTime.Now;

                    return await AddUser(user);
                }
                else
                {
                    throw new Exception("User alreay exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> GetCipherPasswd(LoginRequest req)
        {
            try
            {
                //var result = await _context.PiAdmins.ToListAsync();
                var user = await _context.PiAdmins.FirstOrDefaultAsync(o => o.Email == req.Email);
                if (user != null)
                {
                    return user.Password;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
