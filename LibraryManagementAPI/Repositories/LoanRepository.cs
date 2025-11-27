using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models.Entites;
using LibraryManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryContext _context;

    public LoanRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<Loan?> GetByIdAsync(int id)
    {
        List<Loan> loans = await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.User)
            .ToListAsync();
        
        foreach (Loan loan in loans)
        {
            if (loan.Id == id)
            {
                return loan;
            }
        }
        
        return null;
    }

    public async Task<IEnumerable<Loan>> GetByUserIdAsync(int userId)
    {
        List<Loan> loans = await _context.Loans
            .Include(l => l.Book)
            .ToListAsync();
        
        List<Loan> result = new List<Loan>();
        foreach (Loan loan in loans)
        {
            if (loan.UserId == userId)
            {
                result.Add(loan);
            }
        }
        
        return result;
    }

    public async Task<IEnumerable<Loan>> GetAllWithUserAndBookAsync()
    {
        List<Loan> loans = await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.User)
            .ToListAsync();
        
        return loans;
    }

    public async Task AddAsync(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
    
}