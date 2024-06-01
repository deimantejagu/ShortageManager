using ShortageManager.Models;

namespace ShortageManager.Interfaces;

public interface IStorageManager
{
    void SaveShortage(Shortage shortage);
    void SaveShortages(List<Shortage> shortagesAfterDeletion);
    List<Shortage> LoadShortages();
}