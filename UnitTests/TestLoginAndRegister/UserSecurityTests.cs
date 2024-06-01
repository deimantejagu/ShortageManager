using ShortageManager.Services;

namespace TestProject1.TestLoginAndRegister;

public class UserSecurityTests
{
    private readonly PasswordHashing _passwordHashing;

    public UserSecurityTests()
    {
        _passwordHashing = new PasswordHashing();
    }

    [Fact]
    public void HashPassword_ShouldReturnHashedPassword()
    {
        // Arrange
        string password = "testpassword";

        // Act
        string hashedPassword = _passwordHashing.HashPassword(password);

        // Assert
        Assert.NotEqual(password, hashedPassword); // Ensure that the hashed password is different from the plain password
    }

    [Fact]
    public void VerifyPassword_ShouldReturnTrueForCorrectPassword()
    {
        // Arrange
        string password = "testpassword";
        string hashedPassword = _passwordHashing.HashPassword(password);

        // Act
        bool result = _passwordHashing.VerifyPassword(password, hashedPassword);

        // Assert
        Assert.True(result); // Ensure that verification returns true for the correct password
    }

    [Fact]
    public void VerifyPassword_ShouldReturnFalseForIncorrectPassword()
    {
        // Arrange
        string password = "testpassword";
        string wrongPassword = "wrongpassword";
        string hashedPassword = _passwordHashing.HashPassword(password);

        // Act
        bool result = _passwordHashing.VerifyPassword(wrongPassword, hashedPassword);

        // Assert
        Assert.False(result); // Ensure that verification returns false for an incorrect password
    }

    [Fact]
    public void HashPassword_ShouldBeUnique()
    {
        // Arrange
        string password = "testpassword";

        // Act
        string hashedPassword1 = _passwordHashing.HashPassword(password);
        string hashedPassword2 = _passwordHashing.HashPassword(password);

        // Assert
        Assert.NotEqual(hashedPassword1, hashedPassword2); // Ensure that the hashed passwords are unique
    }
}