using LibraryManagementAPI.Models.Entites;

namespace LibraryManagementAPI.Repositories.Interfaces;

public interface IUserBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task AddAsync(Book book);
    Task SaveChangesAsync();
}