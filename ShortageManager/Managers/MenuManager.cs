using ShortageManager.Enumerators;
using ShortageManager.Models;
using ShortageManager.Validators;

namespace ShortageManager.Managers;

public static class MenuManager
{
    public static void CloseProgram()
    {
        Console.WriteLine("Shortage manager is closed");
        Environment.Exit(0);
    }

    public static void AddNewShortage(User user, ShortageManager shortageManager)
    {
        Console.WriteLine("Enter shortage title: ");
        string? title = Console.ReadLine();
        Console.WriteLine("Enter shortage name: ");
        string? name = Console.ReadLine();

        RoomEnum room = EnumsValidator.GetValidRoomType();
        CategoryEnum category = EnumsValidator.GetValidCategoryType();
                    
        Console.WriteLine("Enter shortage priority (1-10): ");
        int priority = Convert.ToInt32(Console.ReadLine());
        int priorityValue = new PriorityValidator(priority).GetPriority();

        shortageManager.AddShortage(user, title, name, room, category, priorityValue);
    }

    public static void DeleteShortage(User user, ShortageManager shortageManager)
    {
        Console.WriteLine("Enter shortage ID: ");
        string id = Console.ReadLine();
        shortageManager.DeleteShortage(id, user);
    }

    public static void ListAllShortages(User user, ShortageManager shortageManager)
    {
        string choice;
        bool isFilter = false;
        while (true)
        {
            Console.WriteLine("Do you want to select filters? (yes/no): ");
            choice = Console.ReadLine();
        
            if (string.IsNullOrEmpty(choice) || (choice.ToUpper() != "YES" && choice.ToUpper() != "NO"))
            {
                Console.WriteLine("Please select the valid value.");
            }
            else
            {
                if (choice.ToUpper() == "YES")
                {
                    isFilter = true;
                }
                break;
            }
        }
        
        List<int> filters = [];
        if (isFilter)
        {
            Console.WriteLine("Select filters. If all filters was selected, press 0 filter shortages:\n" +
                              "[0] Filter shortages\n" +
                              "[1] Filter by title\n" +
                              "[2] Filter by date\n" + 
                              "[3] Filter by category\n" +
                              "[4] Filter by room\n");

            while (true) 
            {
                var filter = Convert.ToInt32(Console.ReadLine());

                if (filter is < 0 or > 4) 
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                else
                {
                    filters.Add(filter);
                }

                if (filter == 0)
                {
                    break;
                }
            }
        }
        shortageManager.ListShortages(user, filters);
    }
}