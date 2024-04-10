using AuthService.Domain.Entities;
using AuthService.Domain.Exceptions;
using AuthService.Infrastructure.IntegrationTests.HelpersForTests;
using AuthService.Infrastructure.Persistence.DatabaseContext;
using AuthService.Infrastructure.Persistence.Repositories;
using FluentAssertions;

namespace AuthService.Infrastructure.IntegrationTests.Persistence.Repositories;

[Collection(TestingConstants.TestingCollectionName)]
public class UserRepositoryTests : IAsyncLifetime
{
    private readonly Func<Task> _resetDatabase;
    private readonly AuthServiceDbContext _authServiceDbContext;
    private readonly UserRepository _userRepository;

    public UserRepositoryTests(TestingDatabaseFixture testingDatabaseFixture)
    {
        _resetDatabase = testingDatabaseFixture.ResetDatabaseAsync;
        _authServiceDbContext = testingDatabaseFixture.AuthServiceDbContext;
        _userRepository = new UserRepository(_authServiceDbContext);
    }

    public Task InitializeAsync()
        => Task.CompletedTask;

    public async Task DisposeAsync()
        => await _resetDatabase();

    [Fact]
    public async Task CreateAsync_should_AddNewUserToDatabase_when_EntityIsSpecified()
    {
        // Arrange
        User user = new()
        {
            Username = "usernameTest",
            Email = "emailtest@email.com",
            Password = "dsadasd@#!@#dsaldasn@#!#"
        };

        // Act
        int result = await _userRepository.CreateAsync(user);

        // Assert
        result.Should().BePositive();
        User createdUser = _authServiceDbContext.Users.First(x => x.Id == user.Id);
        createdUser.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task CreateAsync_should_ThrowNullEntityException_when_EntityIsNull()
        => await _userRepository.Invoking(x => x.CreateAsync(null!))
                                .Should()
                                .ThrowAsync<NullEntityException>();

    [Fact]
    public async Task GetAsync_should_ReturnUserFromDatabase_when_UserExistsInDatabase()
    {
        // Arrange
        User user = new()
        {
            Username = "usernameTest",
            Email = "emailtest@email.com",
            Password = "dsadasd@#!@#dsaldasn@#!#"
        };

        _authServiceDbContext.Add(user);
        _authServiceDbContext.SaveChanges();

        // Act
        User? result = await _userRepository.GetAsync(user.Id);

        // Assert
        result.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task GetAsync_should_ReturnNull_when_UserDoesNotExistInDatabase()
    {
        // Arrange
        int invalidId = 999999;

        User user = new()
        {
            Username = "usernameTest",
            Email = "emailtest@email.com",
            Password = "dsadasd@#!@#dsaldasn@#!#"
        };

        _authServiceDbContext.Add(user);
        _authServiceDbContext.SaveChanges();

        // Act
        User? result = await _userRepository.GetAsync(invalidId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllAsync_should_ReturnAllUsersFromDatabase_when_UsersExistInDatabase()
    {
        // Arrange
        List<User> users =
        [
            new()
            {
                Username = "usernameTest",
                Email = "emailtest@email.com",
                Password = "dsadasd@#!@#dsaldasn@#!#"
            },
            new()
            {
                Username = "usernameTest2",
                Email = "email2test@email.com",
                Password = "ds23212adasd@#!@#dsaldasn@#!#"
            }
        ];

        _authServiceDbContext.AddRange(users);
        _authServiceDbContext.SaveChanges();

        // Act
        List<User> result = await _userRepository.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(users);
    }

    [Fact]
    public async Task UpdateAsync_should_UpdateUserInDatabase_when_EntityIsSpecified()
    {
        // Arrange
        User user = new()
        {
            Username = "usernameTest",
            Email = "emailtest@email.com",
            Password = "dsad@##@!#"
        };

        _authServiceDbContext.Add(user);
        _authServiceDbContext.SaveChanges();

        // Act
        User userToUpdate = _authServiceDbContext.Users.First(x => x.Id == user.Id);
        userToUpdate.Username = "newUsername";
        userToUpdate.Email = "newemail@email.com";
        userToUpdate.Password = "kjfds@#!@#djsa#@!#!@DASDA";

        int result = await _userRepository.UpdateAsync(userToUpdate);

        // Assert
        result.Should().BePositive();
        User updatedUser = _authServiceDbContext.Users.First(x => x.Id == result);
        updatedUser.Should().BeEquivalentTo(userToUpdate);
    }

    [Fact]
    public async Task UpdateAsync_should_ThrowNullEntityException_when_EntityIsNull()
        => await _userRepository.Invoking(x => x.UpdateAsync(null!))
                                .Should()
                                .ThrowAsync<NullEntityException>();

    [Fact]
    public async Task DeleteAsync_should_DeleteUserFromDatabase_when_UserExistsInDatabase()
    {
        // Arrange
        User user = new()
        {
            Username = "usernameTest",
            Email = "email@email.com",
            Password = "dsad@##@!#"
        };

        _authServiceDbContext.Add(user);
        _authServiceDbContext.SaveChanges();

        // Act
        int result = await _userRepository.DeleteAsync(user.Id);

        // Assert
        result.Should().BePositive();
        User? deletedUser = _authServiceDbContext.Users.FirstOrDefault(x => x.Id == user.Id);
        deletedUser.Should().BeNull();
    }
}