using Microsoft.EntityFrameworkCore;
using Warehouse.Core.Entities;
using Warehouse.Core.IRepositories;
using Warehouse.DataAccess.Persistence;

namespace Warehouse.DataAccess.Repositories;

public class UserRepository(DatabaseContext _context) : IUserRepository
{
    public async Task<List<User>> GetAllAsync() => await _context.Users.ToListAsync();
    public async Task<User?> GetByIdAsync(int id) => await _context.Users.FindAsync(id);
    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}