using ShortageManager.Enumerators;
using ShortageManager.Interfaces;
using ShortageManager.Models;
using ShortageManager.Services;

namespace TestProject1.TestFilterShortages;

public class FilterShortages
{
    private readonly IFilterService _filterService;
    private readonly List<Shortage> _shortages;

    public FilterShortages()
    {
        _filterService = new FilterService(); 
        _shortages = new List<Shortage>
        {
            new ()
            {
                Title = "Bathroom Shortage", Room = RoomEnum.Bathroom, Category = CategoryEnum.Electronics,
                CreatedOn = "2023-06-01", Priority = 1, UserId = Guid.NewGuid()
            },
            new ()
            {
                Title = "Kitchen Shortage", Room = RoomEnum.Kitchen, Category = CategoryEnum.Other,
                CreatedOn = "2023-06-15", Priority = 2, UserId = Guid.NewGuid()
            },
            new ()
            {
                Title = "Meeting room Shortage", Room = RoomEnum.MeetingRoom, Category = CategoryEnum.Food,
                CreatedOn = "2023-06-20", Priority = 1, UserId = Guid.NewGuid()
            }
        };
    }

    [Fact]
    public void FilterByTitle_ShouldReturnCorrectMatches()
    {
        List<Shortage> results = _filterService.FilterByTitle(_shortages, "room");
        Assert.Contains(_shortages[2], results);
    }

    [Fact]
    public void FilterByDate_ShouldReturnCorrectMatches()
    {
        List<Shortage> results =
            _filterService.FilterByDate(_shortages, DateTime.Parse("2023-06-01"), DateTime.Parse("2023-06-10"));
        Assert.Single(results);
        Assert.Contains(_shortages[0], results);
    }

    [Fact]
    public void FilterByCategory_ShouldReturnCorrectMatches()
    {
        List<Shortage> results = _filterService.FilterByCategory(_shortages, "Food");
        Assert.Single(results);
        Assert.Contains(_shortages[2], results);
    }

    [Fact]
    public void FilterByRoom_ShouldReturnCorrectMatches()
    {
        List<Shortage> results = _filterService.FilterByRoom(_shortages, "Kitchen");
        Assert.Single(results);
    }
}