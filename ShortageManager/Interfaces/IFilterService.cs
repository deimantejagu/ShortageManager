using ShortageManager.Models;

namespace ShortageManager.Interfaces;

public interface IFilterService
{
    List<Shortage> FilterByTitle(List<Shortage> shortages, string title);

    List<Shortage> FilterByDate(List<Shortage> shortages, DateTime start, DateTime end);

    List<Shortage> FilterByCategory(List<Shortage> shortages, string category);

    List<Shortage> FilterByRoom(List<Shortage> shortages, string room);
}