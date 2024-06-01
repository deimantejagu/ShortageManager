using Moq;
using ShortageManager.Models;
using ShortageManager.Enumerators;
using ShortageManager.Interfaces;

namespace TestProject1.TestAddShortage;

public class AddShortageTests
{
    private readonly Mock<IStorageManager> _storageManagerMock;
    private readonly Mock<IFilterService> _filterServiceMock;
    private readonly ShortageManager.Managers.ShortageManager _shortageManager;
    private readonly User _user;

    public AddShortageTests()
    {
        _storageManagerMock = new Mock<IStorageManager>();
        _filterServiceMock = new Mock<IFilterService>();
        _shortageManager = new ShortageManager.Managers.ShortageManager(_storageManagerMock.Object, _filterServiceMock.Object);
        _user = new User { UserId = Guid.NewGuid(), IsAdmin = false };
    }

    [Fact]
    public void AddShortage_ShouldSaveNewShortage_WhenValidData()
    {
        // Act
        _shortageManager.AddShortage(_user, "Test Title", "Test Name", RoomEnum.Kitchen, CategoryEnum.Food, 5);
    
        // Assert
        _storageManagerMock.Verify(s => s.SaveShortage(It.IsAny<Shortage>()), Times.Once);
    }

    [Fact]
    public void AddShortage_ShouldThrowException_WhenExistingShortageWithHigherPriority()
    {
        // Arrange
        List<Shortage> shortages = new List<Shortage> { new Shortage { Title = "Title", Room = RoomEnum.Kitchen, Priority = 10 } };
        _storageManagerMock.Setup(s => s.LoadShortages()).Returns(shortages);
        _storageManagerMock.Setup(s => s.SaveShortage(It.IsAny<Shortage>())).Throws<InvalidOperationException>();
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _shortageManager.AddShortage(_user, "Title", "Name", RoomEnum.Kitchen, CategoryEnum.Food, 5));
    }
}