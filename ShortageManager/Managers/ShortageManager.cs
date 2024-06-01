using ShortageManager.Enumerators;
using ShortageManager.Interfaces;
using ShortageManager.Models;

namespace ShortageManager.Managers;

public class ShortageManager
{
    private readonly IStorageManager _storageManager;
    private readonly IFilterService _filterService;
    
    public ShortageManager(IStorageManager storageManager, IFilterService filterService)
    {
        _storageManager = storageManager;
        _filterService = filterService;
    }
    
    public void AddShortage(User user, string title, string name, RoomEnum room, CategoryEnum category, int priority)
    {
        Shortage newShortage = new Shortage();
        Guid shortageId = Guid.NewGuid();
        newShortage.ShortageId = shortageId;
        newShortage.UserId = user.UserId;
        newShortage.Title = title;
        newShortage.Name = name;
        newShortage.Room = room;
        newShortage.Category = category;
        newShortage.Priority = priority;
        newShortage.CreatedOn = DateTime.Now.Date.ToString("yyyy-MM-dd");
        
        try
        {
            _storageManager.SaveShortage(newShortage);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to add shortage: " + ex.Message);
            throw;
        }
    }

    public void DeleteShortage(string id, User user)
    {
        List<Shortage> shortages = _storageManager.LoadShortages();
        Guid shortageId = Guid.Parse(id);
        Shortage shortageToDelete = shortages.Find(s => s.ShortageId == shortageId);
        
        if (shortageToDelete == null)
        {
            Console.WriteLine($"Shortage with ID '{shortageId}' not found.\n");
            return;
        }

        if (user.UserId != shortageToDelete.UserId && !user.IsAdmin)
        {
            Console.WriteLine("You are not authorized to delete this shortage.\n");
            return;
        }

        shortages.Remove(shortageToDelete);
        Console.WriteLine($"Shortage with ID '{shortageId}' deleted successfully.\n");
        
        _storageManager.SaveShortages(shortages);
    }

    private List<Shortage> FilterShortages(List<Shortage> shortages, List<int> filters)
    {
        List<Shortage> filteredShortages = shortages;
        
        foreach (int filter in filters)
        {
            if (filter == 1)
            {
                Console.WriteLine("Write title to filter by: ");
                string title = Console.ReadLine();
                
                filteredShortages = _filterService.FilterByTitle(filteredShortages, title);
            }
            
            if (filter == 2)
            {
                Console.WriteLine("Write start date to filter by: ");
                string startDateString = Console.ReadLine();
                DateTime startDate = DateTime.Parse(startDateString);
                
                Console.WriteLine("Write end date to filter by: ");
                string endDateString = Console.ReadLine();
                DateTime endDate = DateTime.Parse(endDateString);
                
                filteredShortages = _filterService.FilterByDate(filteredShortages, startDate, endDate);
            }
            
            if (filter == 3)
            {
                Console.WriteLine("Write category to filter by: ");
                string category = Console.ReadLine();
                
                filteredShortages = _filterService.FilterByCategory(filteredShortages, category);
            }
            
            if (filter == 4)
            {
                Console.WriteLine("Write room to filter by: ");
                string room = Console.ReadLine();
                
                filteredShortages = _filterService.FilterByRoom(filteredShortages, room);
            }
        }

        return filteredShortages;
    }

    public void ListShortages(User user, List<int> filters)
    {
        List<Shortage> shortages = _storageManager.LoadShortages();
        if (filters.Count > 0)
        {
            shortages = FilterShortages(shortages, filters);
        }
        shortages.Sort();
        List<Shortage> userCreatedShortages = shortages.FindAll(s => s.UserId == user.UserId);
        
        if (shortages.Count == 0)
        {
            Console.WriteLine("There are no shortages currently.");
            return;
        }
        
        Console.WriteLine("{0,-36} | {1,-20} | {2,-20} | {3,-20} | {4,-20} | {5, 10} | {6, 10}", "ID", "Title", "Name", "Room", "Category", "Priority", "Date");
        string separatorLine = string.Join("", Enumerable.Repeat("-", 160));
        Console.WriteLine(separatorLine);
        
        if (user.IsAdmin)
        {
            foreach (Shortage shortage in shortages)
            {
                Console.WriteLine("{0,-36} | {1,-20} | {2,-20} | {3,-20} | {4,-20} | {5, 10} | {6, 10}", 
                    shortage.ShortageId, shortage.Title, shortage.Name, shortage.Room, shortage.Category, shortage.Priority, shortage.CreatedOn);
            }
        }
        else
        {
            foreach (Shortage shortage in userCreatedShortages)
            {
                Console.WriteLine("{0,-36} | {1,-20} | {2,-20} | {3,-20} | {4,-20} | {5, 10} | {6, 10}", 
                    shortage.ShortageId, shortage.Title, shortage.Name, shortage.Room, shortage.Category, shortage.Priority, shortage.CreatedOn);
            }
        }
    }
}