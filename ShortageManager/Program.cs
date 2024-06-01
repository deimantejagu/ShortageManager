using ShortageManager.Enumerators;
using ShortageManager.Managers;
using ShortageManager.Models;
using ShortageManager.Services;
using Microsoft.Extensions.DependencyInjection;
using ShortageManager.Interfaces;

namespace ShortageManager;

public class Program
{
    private const int EXIT_PROGRAM = 0;
    private const int ADD_SHORTAGE = 1;
    private const int DELETE_SHORTAGE = 2;
    private const int LIST_SHORTAGES = 3;
    private const int LOG_OUT = 4;
    
    public static void Main()
    {
        // Setup DI
        var services = new ServiceCollection();
        ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();

        // Resolve services
        var appManager = serviceProvider.GetRequiredService<SecurityManager>();
        var shortageManager = serviceProvider.GetRequiredService<Managers.ShortageManager>();
        
        User currentUser = appManager.LoginOrRegister();
        RunApplication(currentUser, shortageManager);
    }
    
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<PasswordHashing>();  
        services.AddSingleton<SecurityManager>();  
        services.AddSingleton<Managers.ShortageManager>();
        services.AddSingleton<IUserManager, UserManager>(); 
        services.AddSingleton<IStorageManager, StorageManager>();
        services.AddSingleton<IFilterService, FilterService>();
    }
    
    private static void RunApplication(User user, Managers.ShortageManager shortageManager)
    {
        Console.WriteLine($"Welcome {user.UserName}, Administrator: {user.IsAdmin}");

        while (true)
        {
            Console.WriteLine("Select one of the commands:\n" +
                              "[0] Exit program\n" +
                              "[1] Add new shortage\n" +
                              "[2] Delete shortage\n" + 
                              "[3] List all shortages\n" +
                              "[4] Log out\n");
            int? command;
            while (true) 
            {
                command = Convert.ToInt32(Console.ReadLine());

                if (command is > 0 and < 4) 
                {
                    break;
                }
                
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            
            switch (command)
            {
                case EXIT_PROGRAM:
                    MenuManager.CloseProgram();
                    break;
                case ADD_SHORTAGE:
                    MenuManager.AddNewShortage(user, shortageManager);
                    break;
                case DELETE_SHORTAGE:
                    MenuManager.DeleteShortage(user, shortageManager);
                    break;
                case LIST_SHORTAGES:
                    MenuManager.ListAllShortages(user, shortageManager);
                    break;
                case LOG_OUT:
                    Main();
                    break;
            }
        }
    }
}
