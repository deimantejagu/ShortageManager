using Moq;
using ShortageManager.Interfaces;
using ShortageManager.Models;

namespace TestProject1.TestLoginAndRegister;

public class LogInAndRegisterTests
{
    private readonly Mock<IUserManager> _userManagerMock;

    public LogInAndRegisterTests()
    {
        _userManagerMock = new Mock<IUserManager>();
    }

    [Fact]
    public void LoginOrRegister_ShouldLoginSuccessfully()
    {
        // Arrange
        User user = new User { UserId = Guid.NewGuid(), UserName = "testuser", IsAdmin = false };
        _userManagerMock.Setup(x => x.Login("testuser", "password")).Returns(user);

        // Act
        User result = _userManagerMock.Object.Login("testuser", "password");

        // Assert
        Assert.Equal(user.UserId, result.UserId); // Check that the correct user is returned
    }

    [Fact]
    public void LoginOrRegister_ShouldRegisterUser()
    {
        // Arrange
        _userManagerMock.Setup(x => x.Register("newuser", "newpass", false)).Returns(true);

        // Act
        bool registerResult = _userManagerMock.Object.Register("newuser", "newpass", false);

        // Assert
        Assert.True(registerResult); // Check that true is returned
    }
}