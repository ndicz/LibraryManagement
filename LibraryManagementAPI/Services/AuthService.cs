using LibraryManagementAPI.Helpers;
using LibraryManagementAPI.Models.DTOs.Auth;
using LibraryManagementAPI.Models.Entites;
using LibraryManagementAPI.Models.Responses;
using LibraryManagementAPI.Repositories.Interfaces;
using LibraryManagementAPI.Services.Interfaces;

namespace LibraryManagementAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly PasswordHasser _hasher;   // pastikan nama helper ini benar
    private readonly JwtService _jwt;

    public AuthService(IUserRepository userRepo, PasswordHasser hasher, JwtService jwt)
    {
        _userRepo = userRepo;
        _hasher = hasher;
        _jwt = jwt;
    }

    public async Task<APIResponse<string>> RegisterAsync(RegisterRequestDTO request)
    {
        var existingUser = await _userRepo.GetByUsernameAsync(request.Username);
        if (existingUser != null)
        {
            return APIResponse<string>.Fail("Username sudah digunakan");
        }

        var hashedPassword = _hasher.Hash(request.Password);

        var user = new User
        {
            Username = request.Username,
            PasswordHash = hashedPassword,
            Role = "Member"
        };

        await _userRepo.AddAsync(user);
        await _userRepo.SaveChangesAsync();

        return APIResponse<string>.Ok("Registrasi berhasil");
    }

    public async Task<APIResponse<string>> LoginAsync(LoginRequestDTO request)
    {
        var user = await _userRepo.GetByUsernameAsync(request.Username);
        if (user == null)
        {
            return APIResponse<string>.Fail("Username / password salah");
        }

        var passwordValid = _hasher.Verify(request.Password, user.PasswordHash);
        if (!passwordValid)
        {
            return APIResponse<string>.Fail("Username / password salah");
        }

        // cukup panggil instance JwtService
        var token = _jwt.GenerateToken(user);

        return APIResponse<string>.Ok(token, "Login berhasil");
    }
}