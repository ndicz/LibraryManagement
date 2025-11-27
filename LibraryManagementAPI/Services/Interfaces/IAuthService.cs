using LibraryManagementAPI.Models.DTOs.Auth; // WAJIB
using LibraryManagementAPI.Models.Responses;

namespace LibraryManagementAPI.Services.Interfaces;

public interface IAuthService
{
    Task<APIResponse<string>> RegisterAsync(RegisterRequestDTO request);
    Task<APIResponse<string>> LoginAsync(LoginRequestDTO request);
}