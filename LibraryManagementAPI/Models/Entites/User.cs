namespace LibraryManagementAPI.Models.Entites;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string Role { get; set; } = "Member"; // "Admin" / "Member"
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}