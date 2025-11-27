using LibraryManagementAPI.Models.DTOs.Loan;
using LibraryManagementAPI.Models.Entites;
using LibraryManagementAPI.Models.Responses;

namespace LibraryManagementAPI.Services.Interfaces;

public interface ILoanService
{
    Task<APIResponse<Loan>> BorrowAsync(int userId, LoanRequestDTO request);
    Task<APIResponse<Loan>> ReturnAsync(int userId, string role, int loanId);
    Task<IEnumerable<Loan>> GetMyLoansAsync(int userId);
    Task<IEnumerable<Loan>> GetAllLoansAsync();
}