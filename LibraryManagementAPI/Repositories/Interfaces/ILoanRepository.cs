using LibraryManagementAPI.Models.Entites;

namespace LibraryManagementAPI.Repositories.Interfaces;

public interface ILoanRepository
{
    Task<Loan?> GetByIdAsync(int id);
    Task<IEnumerable<Loan>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Loan>> GetAllWithUserAndBookAsync();
    Task AddAsync(Loan loan);
    Task SaveChangesAsync();
}