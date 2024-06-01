using Newtonsoft.Json;
using ShortageManager.Interfaces;
using ShortageManager.Models;
using ShortageManager.Services;

namespace ShortageManager.Managers;

public class UserManager : IUserManager
{
    private List<User> _users;
    private const string FilePath = @"JsonDB\\users.json";
    private PasswordHashing _passwordHashing;

    public UserManager(PasswordHashing passwordHashing)
    {
        _users = LoadUsers();
        _passwordHashing = passwordHashing;
    }

    public bool Register(string userName, string password, bool isAdmin)
    {
        User newUser = new User();
        Guid userId = Guid.NewGuid();
        newUser.UserId = userId;
        newUser.UserName = userName;
        string hashedPassword = _passwordHashing.HashPassword(password);
        newUser.PasswordHash = hashedPassword;
        newUser.IsAdmin = isAdmin;

        try
        {
            SaveUser(newUser);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Registration failed: {ex.Message}");
            
            return false;
        }
    }

    public User Login(string userName, string password)
    {
        User user = _users.Find(u => u.UserName == userName);

        if (user != null &&  _passwordHashing.VerifyPassword(password, user.PasswordHash))
        {
            Console.WriteLine("Login successful!");
            
            return user;
        }
        
        Console.WriteLine("Invalid username or password.");
        
        return null;
    }

    private List<User> LoadUsers()
    {
        if (!File.Exists(FilePath))
        {
            return new List<User>();
        }

        string json = File.ReadAllText(FilePath);

        List<User> users = JsonConvert.DeserializeObject<List<User>>(json);

        return users ?? new List<User>();
    }

    private void SaveUser(User user)
    {
        _users.Add(user);

        var json = JsonConvert.SerializeObject(_users, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }
}