using LibraryManagementAPI.Models.DTOs.Book;
using LibraryManagementAPI.Models.Entites;
using LibraryManagementAPI.Models.Responses;

namespace LibraryManagementAPI.Services.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task<APIResponse<Book>> CreateAsync(BookCreateUpdateDTO dto);
    Task<APIResponse<Book>> UpdateAsync(int id, BookCreateUpdateDTO dto);
    Task<APIResponse<string>> DeleteAsync(int id);
}