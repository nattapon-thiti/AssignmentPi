using Microsoft.EntityFrameworkCore;
using Pi.Interfaces.Repositories.Users;
using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Users;
using Pi.Models.ResponseModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Repositories.Users
{
    public class UsersRepositories : IUserRepositories
    {
        private readonly PiContext _context;
        public UsersRepositories(PiContext context)
        {
            _context = context;
        }
        public async Task<CreateUserResponse> CreateOrUpdateAsync(UserCreateOrUpdateRequest request)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    CreateUserResponse result = new CreateUserResponse();
                    #region update user
                    if (request?.Id != null)
                    {
                        PiUser user = await GetAsync(request.Id.Value);
                        if (user != null)
                        {
                            user.GivenName = (string.IsNullOrWhiteSpace(request.GivenName)) ? user.GivenName : request.GivenName;
                            user.Email = (string.IsNullOrWhiteSpace(request.Email)) ? user.Email : request.Email;
                            await _context.SaveChangesAsync();
                            result = new CreateUserResponse(true, user, $"User id {user.Id} updated");
                        }
                        else
                        {
                            result = new CreateUserResponse(false, user, $"User id {request.Id} not found");
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
                        await _context.SaveChangesAsync();
                        result = new CreateUserResponse(true, user, $"User id {user.Id} created");
                    }
                    #endregion

                    await transaction.CommitAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var user = await _context.PiUsers.FindAsync(id);
                if (user != null)
                {
                    _context.PiUsers.Remove(user);
                    return await _context.SaveChangesAsync() > 0;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public async Task<PiUser> GetAsync(int request)
        {
            try
            {
                return await _context.PiUsers.FindAsync(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
