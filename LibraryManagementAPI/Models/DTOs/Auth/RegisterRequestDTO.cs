namespace LibraryManagementAPI.Models.DTOs.Auth;

public class RegisterRequestDTO
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}