using Warehouse.Core.Entities;

namespace Warehouse.Shared.Services;

public interface IUserService
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user, string password);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task ChangePasswordAsync(int id, string newPassword);
}