using ShortageManager.Models;

namespace ShortageManager.Interfaces;

public interface IUserManager
{
    User Login(string username, string password);
    bool Register(string userName, string password, bool isAdmin);
}