using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models.Entites;
using LibraryManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryContext _context;

    public BookRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        List<Book> books = await _context.Books.ToListAsync();
        return books;
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        List<Book> books = await _context.Books.ToListAsync();

        foreach (Book book in books)
        {
            if (book.Id == id)
            {
                return book;
            }
        }

        return null;
    }

    public async Task AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
    }

    public void Remove(Book book)
    {
        _context.Books.Remove(book);
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}