using LibraryManagementAPI.Models.Entites;

namespace LibraryManagementAPI.Repositories.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task AddAsync(Book book);
    void Remove(Book book);
    Task SaveChangesAsync();
}