using ShortageManager.Interfaces;
using ShortageManager.Models;

namespace ShortageManager.Services;

public class FilterService : IFilterService
{
    public List<Shortage> FilterByTitle(List<Shortage> shortages, string title)
    {
        return shortages.Where(shortage => 
            shortage.Title.ToLower().Contains(title.ToLower())
        ).ToList();
    }
    
    public List<Shortage> FilterByDate(List<Shortage> shortages, DateTime start, DateTime end)
    {
        while (true)
        {
            if (start < end)
            {
                break;
            }
            
            Console.WriteLine("Start date cannot be after end date.");
        }

        return shortages.Where(shortage =>
        {
            DateTime createdOn = DateTime.Parse(shortage.CreatedOn);
            return DateTime.Compare(createdOn, start) >= 0 &&
                   DateTime.Compare(createdOn, end) <= 0;
        }).ToList();
    }

    public List<Shortage> FilterByCategory(List<Shortage> shortages, string category)
    {
        return shortages.FindAll(s => s.Category.ToString() == category);
    }
    
    public List<Shortage> FilterByRoom(List<Shortage> shortages, string room)
    {
        return shortages.FindAll(s => s.Room.ToString() == room);
    }
}