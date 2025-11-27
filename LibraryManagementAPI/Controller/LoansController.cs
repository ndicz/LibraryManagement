using System.Security.Claims;
using LibraryManagementAPI.Models.DTOs.Loan;
using LibraryManagementAPI.Models.Entites;
using LibraryManagementAPI.Models.Responses;
using LibraryManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controller;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService) => _loanService = loanService;
    
    [HttpPost]
    public async Task<ActionResult<APIResponse<Loan>>> Borrow(LoanRequestDTO request)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdStr, out var userId))
            return Unauthorized(APIResponse<Loan>.Fail("User tidak valid"));

        var result = await _loanService.BorrowAsync(userId, request);
        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
    [HttpPost("{id:int}/return")]
    public async Task<ActionResult<APIResponse<Loan>>> Return(int id)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdStr, out var userId))
            return Unauthorized(APIResponse<Loan>.Fail("User tidak valid"));

        var role = User.FindFirstValue(ClaimTypes.Role) ?? string.Empty;

        var result = await _loanService.ReturnAsync(userId, role, id);
        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("me")]
    public async Task<ActionResult<IEnumerable<Loan>>> GetMyLoans()
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdStr, out var userId))
            return Unauthorized();

        var loans = await _loanService.GetMyLoansAsync(userId);
        return Ok(loans);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<Loan>>> GetAllLoans()
    {
        var loans = await _loanService.GetAllLoansAsync();
        return Ok(loans);
    }
}