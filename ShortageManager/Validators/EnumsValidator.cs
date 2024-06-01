using ShortageManager.Enumerators;

namespace ShortageManager.Validators;

public class EnumsValidator
{
    public static RoomEnum GetValidRoomType()
    {
        bool validInput = false;

        while (!validInput)
        {
            Console.WriteLine("Enter shortage room type: ");
            string roomString = Console.ReadLine();

            try
            {
                RoomEnum room = (RoomEnum)Enum.Parse(typeof(RoomEnum), roomString, true);
                validInput = true;
                
                return room; 
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid room type. Available options:");
            }
        }

        throw new InvalidOperationException("No valid room type entered."); 
    }

    public static CategoryEnum GetValidCategoryType()
    {
        bool validInput = false;

        while (!validInput)
        {
            Console.WriteLine("Enter shortage category type: ");
            string categoryString = Console.ReadLine();

            try
            {
                var category = (CategoryEnum)Enum.Parse(typeof(CategoryEnum), categoryString, true);
                validInput = true;
                
                return category;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid category type. Available options:");
            }
        }

        throw new InvalidOperationException("No valid category type entered."); 
    }
}