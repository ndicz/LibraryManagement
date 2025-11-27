namespace LibraryManagementAPI.Models.DTOs.Book;

public class BookCreateUpdateDTO
{
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public int TotalCopies { get; set; }
}