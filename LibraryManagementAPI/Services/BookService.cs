using LibraryManagementAPI.Models.DTOs.Book;
using LibraryManagementAPI.Models.Entites;
using LibraryManagementAPI.Models.Responses;
using LibraryManagementAPI.Repositories.Interfaces;
using LibraryManagementAPI.Services.Interfaces;

namespace LibraryManagementAPI.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepo;

    public BookService(IBookRepository bookRepo)
    {
        _bookRepo = bookRepo;
    }

    public Task<IEnumerable<Book>> GetAllAsync()
    {
        return _bookRepo.GetAllAsync();
    }

    public Task<Book?> GetByIdAsync(int id)
    {
        return _bookRepo.GetByIdAsync(id);
    }

    public async Task<APIResponse<Book>> CreateAsync(BookCreateUpdateDTO dto)
    {
        Book book = new Book();
        book.Title = dto.Title;
        book.Author = dto.Author;
        book.TotalCopies = dto.TotalCopies;
        book.AvailableCopies = dto.TotalCopies;

        await _bookRepo.AddAsync(book);
        await _bookRepo.SaveChangesAsync();

        return APIResponse<Book>.Ok(book, "Buku ditambahkan");
    }

    public async Task<APIResponse<Book>> UpdateAsync(int id, BookCreateUpdateDTO dto)
    {
        Book? existingBook = await _bookRepo.GetByIdAsync(id);
        if (existingBook == null)
        {
            return APIResponse<Book>.Fail("Buku tidak ditemukan");
        }

        existingBook.Title = dto.Title;
        existingBook.Author = dto.Author;

        int oldTotal = existingBook.TotalCopies;
        int newTotal = dto.TotalCopies;
        int difference = newTotal - oldTotal;

        existingBook.TotalCopies = newTotal;
        existingBook.AvailableCopies = existingBook.AvailableCopies + difference;

        if (existingBook.AvailableCopies < 0)
        {
            existingBook.AvailableCopies = 0;
        }

        await _bookRepo.SaveChangesAsync();

        return APIResponse<Book>.Ok(existingBook, "Buku diupdate");
    }

    public async Task<APIResponse<string>> DeleteAsync(int id)
    {
        Book? existingBook = await _bookRepo.GetByIdAsync(id);
        if (existingBook == null)
        {
            return APIResponse<string>.Fail("Buku tidak ditemukan");
        }

        _bookRepo.Remove(existingBook);
        await _bookRepo.SaveChangesAsync();

        return APIResponse<string>.Ok("Buku dihapus");
    }
}