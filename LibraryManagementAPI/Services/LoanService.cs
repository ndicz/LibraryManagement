using LibraryManagementAPI.Models.DTOs.Loan;
using LibraryManagementAPI.Models.Entites;
using LibraryManagementAPI.Models.Responses;
using LibraryManagementAPI.Repositories.Interfaces;
using LibraryManagementAPI.Services.Interfaces;

namespace LibraryManagementAPI.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepo;
    private readonly IBookRepository _bookRepo;

    public LoanService(ILoanRepository loanRepo, IBookRepository bookRepo)
    {
        _loanRepo = loanRepo;
        _bookRepo = bookRepo;
    }

    public async Task<APIResponse<Loan>> BorrowAsync(int userId, LoanRequestDTO request)
    {
        Book? book = await _bookRepo.GetByIdAsync(request.BookId);
        if (book == null)
        {
            return APIResponse<Loan>.Fail("Buku tidak ditemukan");
        }

        if (book.AvailableCopies <= 0)
        {
            return APIResponse<Loan>.Fail("Stok buku habis");
        }

        Loan loan = new Loan();
        loan.BookId = book.Id;
        loan.UserId = userId;
        loan.LoanDate = DateTime.UtcNow;

        book.AvailableCopies = book.AvailableCopies - 1;

        await _loanRepo.AddAsync(loan);
        await _loanRepo.SaveChangesAsync();

        return APIResponse<Loan>.Ok(loan, "Peminjaman berhasil");
    }

    public async Task<APIResponse<Loan>> ReturnAsync(int userId, string role, int loanId)
    {
        Loan? loan = await _loanRepo.GetByIdAsync(loanId);
        if (loan == null)
        {
            return APIResponse<Loan>.Fail("Data peminjaman tidak ditemukan");
        }

        bool isPeminjam = loan.UserId == userId;
        bool isAdmin = role == "Admin";

        if (!isPeminjam && !isAdmin)
        {
            return APIResponse<Loan>.Fail("Tidak punya akses");
        }

        if (loan.IsReturned)
        {
            return APIResponse<Loan>.Fail("Buku sudah dikembalikan");
        }

        loan.ReturnDate = DateTime.UtcNow;
        loan.Book.AvailableCopies = loan.Book.AvailableCopies + 1;

        await _loanRepo.SaveChangesAsync();

        return APIResponse<Loan>.Ok(loan, "Pengembalian berhasil");
    }

    public Task<IEnumerable<Loan>> GetMyLoansAsync(int userId)
    {
        return _loanRepo.GetByUserIdAsync(userId);
    }

    public Task<IEnumerable<Loan>> GetAllLoansAsync()
    {
        return _loanRepo.GetAllWithUserAndBookAsync();
    }
}