using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models.Entites;
using LibraryManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Repositories;

public class UserRepository :  IUserRepository
{
    private readonly LibraryContext _context;

    public UserRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        List<User> users = await _context.Users.ToListAsync();
        
        foreach (User user in users)
        {
            if (user.Username == username)
            {
                return user;
            }
        }
        
        return null;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        List<User> users = await _context.Users.ToListAsync();
        
        foreach (User user in users)
        {
            if (user.Id == id)
            {
                return user;
            }
        }
        
        return null;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}