using Moq;
using ShortageManager.Interfaces;
using ShortageManager.Models;

namespace TestProject1.TestDeleteShortage;

public class DeleteShortageTests
{
    private readonly Mock<IStorageManager> _storageManagerMock;
    private readonly Mock<IFilterService> _filterServiceMock;
    private readonly ShortageManager.Managers.ShortageManager _shortageManager;
    private readonly User _adminUser;
    private readonly User _regularUser;

    public DeleteShortageTests()
    {
        // Mocks
        _storageManagerMock = new Mock<IStorageManager>();
        _filterServiceMock = new Mock<IFilterService>();
        
        // Manager
        _shortageManager = new ShortageManager.Managers.ShortageManager(_storageManagerMock.Object, _filterServiceMock.Object);
        
        // Users
        _adminUser = new User { UserId = Guid.NewGuid(), IsAdmin = true };
        _regularUser = new User { UserId = Guid.NewGuid(), IsAdmin = false };
    }

    [Fact]
    public void DeleteShortage_ShouldNotCallSaveWhenShortageDoesNotExist()
    {
        // Arrange
        _storageManagerMock.Setup(s => s.LoadShortages()).Returns(new List<Shortage>());
        
        // Act
        _shortageManager.DeleteShortage(Guid.NewGuid().ToString(), _adminUser);
        
        // Assert
        _storageManagerMock.Verify(s => s.SaveShortages(It.IsAny<List<Shortage>>()), Times.Never);
    }

    [Fact]
    public void DeleteShortage_ShouldNotDeleteIfUserLacksPermission()
    {
        // Arrange
        var shortageId = Guid.NewGuid();
        var shortages = new List<Shortage> { new () { ShortageId = shortageId, UserId = Guid.NewGuid() } }; // Not owned by the regularUser
        
        _storageManagerMock.Setup(s => s.LoadShortages()).Returns(shortages);
        
        // Act
        _shortageManager.DeleteShortage(shortageId.ToString(), _regularUser);
        
        // Assert
        _storageManagerMock.Verify(s => s.SaveShortages(It.IsAny<List<Shortage>>()), Times.Never);
    }

    [Fact]
    public void DeleteShortage_ShouldDelete_WhenDataIsValid()
    {
        // Arrange
        Guid shortageId = Guid.NewGuid();
        List<Shortage> shortages = new List<Shortage> { new () { ShortageId = shortageId, UserId = _adminUser.UserId } }; // Owned by the adminUser
        
        _storageManagerMock.Setup(s => s.LoadShortages()).Returns(shortages);
        
        // Act
        _shortageManager.DeleteShortage(shortageId.ToString(), _adminUser);

        // Assert
        _storageManagerMock.Verify(s => s.SaveShortages(It.IsAny<List<Shortage>>()), Times.Once);
    }
}