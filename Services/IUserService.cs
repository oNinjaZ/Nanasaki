using System.Collections.Generic;
using System.Threading.Tasks;
using Nanasaki.Models;

namespace Nanasaki.Services;

#nullable enable
public interface IUserService
{
    public Task<bool> CreateAsync(User user);
    public Task<bool> DeleteAsync(string id);
    public Task<User?> GetUserByIdAsync(string id);
    public Task<bool> UpdateUsernameAsync(string id, string newUsername);
    public Task<IEnumerable<User>> GetAllAsync();
    
}