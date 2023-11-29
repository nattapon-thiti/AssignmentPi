using Microsoft.EntityFrameworkCore;
using Pi.Interfaces.Repositories.Users;
using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Repositories.Users
{
    public class UsersRepositories : IUserRepositories
    {
        readonly PiContext _context;
        public UsersRepositories(PiContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateOrUpdateAsync(UserCreateOrUpdateRequest request)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    #region update user
                    if (request?.Id != null)
                    {
                        PiUser result = await GetAsync(request.Id.Value);
                        if (result != null)
                        {
                            result.GivenName = (string.IsNullOrWhiteSpace(request.GivenName)) ? result.GivenName : request.GivenName;
                            result.Email = (string.IsNullOrWhiteSpace(request.Email)) ? result.Email : request.Email;
                        }
                    }
                    #endregion

                    #region create new user
                    else
                    {
                        var user = new PiUser()
                        {
                            GivenName = request.GivenName,
                            Email = request.Email,
                        };
                        await _context.PiUsers.AddAsync(user);
                    }
                    #endregion

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    //throw;
                    return false;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entityToDelete = await _context.PiUsers.FindAsync(id);
            if (entityToDelete != null)
            {
                _context.PiUsers.Remove(entityToDelete);
                return await _context.SaveChangesAsync() > 0;
            }
            else { return false; }
        }

        public async Task<IEnumerable<PiUser>> GetAsync()
        {
            IEnumerable<PiUser> result = await _context.PiUsers.ToListAsync();
            return result;
        }
        public async Task<PiUser> GetAsync(int request)
        {
            PiUser result = await _context.PiUsers.FindAsync(request);
            return result;
        }
    }
}
