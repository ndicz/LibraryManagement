using LibraryManagementAPI.Models.DTOs.Book;
using LibraryManagementAPI.Models.Entites;
using LibraryManagementAPI.Models.Responses;
using LibraryManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService) => _bookService = bookService;

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Book>>> GetAll()
    {
        var books = await _bookService.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<Book>> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);
        if (book == null) return NotFound();
        return Ok(book);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<APIResponse<Book>>> Create(BookCreateUpdateDTO dto)
    {
        var result = await _bookService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<APIResponse<Book>>> Update(int id, BookCreateUpdateDTO dto)
    {
        var result = await _bookService.UpdateAsync(id, dto);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<APIResponse<string>>> Delete(int id)
    {
        var result = await _bookService.DeleteAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }
}