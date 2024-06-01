namespace ShortageManager.Validators;

public class PriorityValidator
{
    private int _priority;
    
    public PriorityValidator(int priority)
    {
        ValidatePriority(priority);
        _priority = priority;
    }
    
    private static void ValidatePriority(int priority)
    {
        if (priority is < 1 or > 10)
        {
            throw new Exception("Priority value is not valid");
        }
    }

    public int GetPriority()
    {
        return _priority;
    }
}