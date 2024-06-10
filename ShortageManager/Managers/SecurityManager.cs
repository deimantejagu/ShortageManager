using ShortageManager.Interfaces;
using ShortageManager.Models;

namespace ShortageManager.Managers;

public class SecurityManager
{
    private readonly IUserManager _userManager;

    public SecurityManager(IUserManager userManager)
    {
        _userManager = userManager;
    }

    public User LoginOrRegister()
    {
        Console.WriteLine("Welcome to the shortage manager!");
        
        while (true)
        {
            Console.WriteLine("Login or Register (L/R): ");
            string? command = Console.ReadLine();
            if (string.IsNullOrEmpty(command) || (command.ToUpper() != "R" && command.ToUpper() != "L"))
            {
                Console.WriteLine("Please enter a valid command.");
                continue;
            }
            
            if (command.ToUpper() == "L")
            {
                // Handle Login
                Console.WriteLine("Enter username: ");
                string? username = Console.ReadLine();
                
                Console.WriteLine("Enter password: ");
                string? password = Console.ReadLine();
                
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Username and password cannot be empty.");
                    continue;
                }

                User user = _userManager.Login(username, password);
                if (user != null)
                {
                    return user;
                }
                Console.WriteLine("Invalid username or password");
            }
            else
            {
                // Handle Register
                string? role;
                bool isAdmin = false;
                while (true)
                {
                    Console.WriteLine("Are you and administrator? (yes/no) ");
                    role = Console.ReadLine();
                    
                    if (string.IsNullOrEmpty(role) || (role.ToUpper() != "YES" && role.ToUpper() != "NO"))
                    {
                        Console.WriteLine("Please select the valid value.");
                    }
                    else
                    {
                        if (role.ToUpper() == "YES")
                        {
                            isAdmin = true;
                        }
                        break;
                    }
                }

                string? username;
                string? password;
                    
                while (true)
                {
                    Console.WriteLine("Enter username: ");
                    username = Console.ReadLine();
                
                    Console.WriteLine("Enter password: ");
                    password = Console.ReadLine();
                    
                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    {
                        Console.WriteLine("Username and password cannot be empty.");
                    }
                    else
                    {
                        break;
                    }
                }

                bool success = _userManager.Register(username, password, isAdmin);
                if (success)
                {
                    Console.WriteLine("Registration successful. Please Login.");
                }
                else
                {
                    Console.WriteLine("Registration failed. User already exists.");
                }
            }
        }
    }
}