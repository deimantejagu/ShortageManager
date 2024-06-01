using ShortageManager.Models;
using Newtonsoft.Json;
using ShortageManager.Interfaces;

namespace ShortageManager.Managers;

public class StorageManager : IStorageManager
{
    private List<Shortage> _shortages;
    private const string FilePath = @"JsonDB\\shortages.json";

    public StorageManager()
    {
        _shortages = LoadShortages();
    }

    public void SaveShortage(Shortage shortage)
    {
        int existingShortageIndex = _shortages.FindIndex(s =>
            s.Title == shortage.Title &&
            s.Room == shortage.Room);

        if (existingShortageIndex >= 0)
        {
            if (shortage.Priority > _shortages[existingShortageIndex].Priority)
            {
                _shortages[existingShortageIndex] = shortage;
                Console.WriteLine("Existing shortage with lower priority updated.");
            }
            else
            {
                Console.WriteLine("This shortage already exists with higher or equal priority.");
                return;
            }
        }
        else
        {
            _shortages.Add(shortage);
            Console.WriteLine("Shortage was successfully added");
        }

        var json = JsonConvert.SerializeObject(_shortages, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }
    
    public void SaveShortages(List<Shortage> shortagesAfterDeletion)
    {
        var json = JsonConvert.SerializeObject(shortagesAfterDeletion, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }

    public List<Shortage> LoadShortages()
    {
        if (!File.Exists(FilePath))
        {
            return new List<Shortage>();
        }

        string json = File.ReadAllText(FilePath);

        List<Shortage> shortages = JsonConvert.DeserializeObject<List<Shortage>>(json);

        return shortages ?? new List<Shortage>();
    }
}