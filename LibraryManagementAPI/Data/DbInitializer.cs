using LibraryManagementAPI.Helpers;
using LibraryManagementAPI.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Data;

public class DbInitializer
{
    public static async Task SeedAsync(LibraryContext context, PasswordHasser hasher)
    {
        await context.Database.MigrateAsync();

        // Cek admin tanpa LINQ
        bool adminExists = false;
        List<User> users = await context.Users.ToListAsync();
        foreach (User u in users)
        {
            if (u.Role == "Admin")
            {
                adminExists = true;
                break;
            }
        }

        if (!adminExists)
        {
            User admin = new User();
            admin.Username = "admin";
            admin.PasswordHash = hasher.Hash("Admin123!");
            admin.Role = "Admin";

            await context.Users.AddAsync(admin);
        }

        // Cek buku tanpa LINQ
        bool hasBooks = false;
        List<Book> books = await context.Books.ToListAsync();
        if (books.Count > 0)
        {
            hasBooks = true;
        }

        if (!hasBooks)
        {
            Book book1 = new Book();
            book1.Title = "Clean Code";
            book1.Author = "Robert C. Martin";
            book1.TotalCopies = 5;
            book1.AvailableCopies = 5;

            Book book2 = new Book();
            book2.Title = "The Pragmatic Programmer";
            book2.Author = "Andrew Hunt";
            book2.TotalCopies = 3;
            book2.AvailableCopies = 3;

            await context.Books.AddAsync(book1);
            await context.Books.AddAsync(book2);
        }

        await context.SaveChangesAsync();
    }
}