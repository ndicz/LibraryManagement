namespace LibraryManagementAPI.Models.DTOs.Auth;

public class LoginRequestDTO
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}