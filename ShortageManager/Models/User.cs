namespace ShortageManager.Models;

public class User
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAdmin { get; set; }
}