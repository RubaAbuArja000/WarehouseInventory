using Warehouse.Core.Entities;
using Warehouse.Core.IRepositories;
using Warehouse.Shared.Services;

namespace Warehouse.Services;

public class UserService(IUserRepository _userRepository) : IUserService
{
    public async Task<List<User>> GetAllAsync() => await _userRepository.GetAllAsync();

    public async Task<User?> GetByIdAsync(int id) => await _userRepository.GetByIdAsync(id);

    public async Task<User?> GetByEmailAsync(string email) => await _userRepository.GetByEmailAsync(email);

    public async Task AddAsync(User user, string password)
    {
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        await _userRepository.AddAsync(user);
    }

    public async Task UpdateAsync(User user) => await _userRepository.UpdateAsync(user);

    public async Task DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user != null && user.Email != "admin@happywarehouse.com")
            await _userRepository.DeleteAsync(user);
    }

    public async Task ChangePasswordAsync(int id, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return;

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await _userRepository.UpdateAsync(user);
    }
}