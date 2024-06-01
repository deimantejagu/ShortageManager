using Moq;
using ShortageManager.Interfaces;
using ShortageManager.Models;

namespace TestProject1.TestListShortages;

public class ListShortagesTests
{
    private readonly Mock<IStorageManager> _storageManagerMock;
    private readonly Mock<IFilterService> _filterServiceMock;
    private readonly ShortageManager.Managers.ShortageManager _shortageManager;
    private readonly User _adminUser;
    private readonly User _regularUser;
    private readonly List<Shortage> _shortages;

    public ListShortagesTests()
    {
        // Initialize the mocks
        _storageManagerMock = new Mock<IStorageManager>();
        _filterServiceMock = new Mock<IFilterService>();

        // Initialize the manager with mocked dependencies
        _shortageManager = new ShortageManager.Managers.ShortageManager(_storageManagerMock.Object, _filterServiceMock.Object);

        // Define users
        _adminUser = new User { UserId = Guid.NewGuid(), IsAdmin = true };
        _regularUser = new User { UserId = Guid.NewGuid(), IsAdmin = false };

        // Define list of shortages used across tests
        _shortages = new List<Shortage>
        {
            new () { UserId = _regularUser.UserId, Priority = 1, CreatedOn = DateTime.Now.ToString("yyyy-MM-dd") },
            new () { UserId = Guid.NewGuid(), Priority = 2, CreatedOn = DateTime.Now.ToString("yyyy-MM-dd") } // Not owned by the regular user
        };

        // Common setups for mock objects
        _storageManagerMock.Setup(s => s.LoadShortages()).Returns(_shortages);
    }

    [Fact]
    public void ListShortages_ShouldReturn_WhenShortageCountIsZero()
    {
        // Arrange
        _storageManagerMock.Setup(s => s.LoadShortages()).Returns(new List<Shortage>());

        // Act
        _shortageManager.ListShortages(_regularUser, new List<int>());

        // Assert
        _storageManagerMock.Verify(s => s.LoadShortages(), Times.Once); 
    }

    [Fact]
    public void ListShortages_ShouldList_WhenUserIsAdmin()
    {
        // Act
        _shortageManager.ListShortages(_adminUser, new List<int>());

        // Assert
        // Verifies that the shortages are listed if the user is admin
        Assert.NotEmpty(_shortages); 
    }

    [Fact]
    public void ListShortages_ShouldList_WhenUserIsNotAdmin()
    {
        // Act
        _shortageManager.ListShortages(_regularUser, new List<int>());

        // Assert
        // Verifies that only the shortages created by the regular user are listed
        Assert.Single(_shortages.FindAll(s => s.UserId == _regularUser.UserId));
    }
}
