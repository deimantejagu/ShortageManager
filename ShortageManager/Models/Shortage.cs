using ShortageManager.Enumerators;

namespace ShortageManager.Models;

public class Shortage :  IComparable<Shortage>
{
    public Guid ShortageId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public RoomEnum Room { get; set; }
    public CategoryEnum Category { get; set; }
    public int? Priority { get; set; }
    public string CreatedOn { get; set; }

    public int CompareTo(Shortage other)
    {
        return other.Priority.Value.CompareTo(Priority.Value);
    }
}