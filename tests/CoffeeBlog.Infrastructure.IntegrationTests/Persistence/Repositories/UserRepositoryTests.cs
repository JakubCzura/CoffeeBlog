using CoffeeBlog.Domain.Entities;
using CoffeeBlog.Domain.Exceptions;
using CoffeeBlog.Infrastructure.IntegrationTests.HelpersForTests;
using CoffeeBlog.Infrastructure.Persistence.DatabaseContext;
using CoffeeBlog.Infrastructure.Persistence.Repositories;
using FluentAssertions;

namespace CoffeeBlog.Infrastructure.IntegrationTests.Persistence.Repositories;

[Collection(TestingConstants.TestingCollectionName)]
public class UserRepositoryTests : IAsyncLifetime
{
    private readonly Func<Task> _resetDatabase;
    private readonly CoffeeBlogDbContext _coffeeBlogDbContext;
    private readonly UserRepository _userRepository;

    public UserRepositoryTests(TestingDatabaseFixture coffeeBlogDatabaseFixture)
    {
        _resetDatabase = coffeeBlogDatabaseFixture.ResetDatabaseAsync;
        _coffeeBlogDbContext = coffeeBlogDatabaseFixture.CoffeeBlogContext;
        _userRepository = new UserRepository(_coffeeBlogDbContext);
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
        User createdUser = _coffeeBlogDbContext.Users.First(x => x.Id == user.Id);
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

        _coffeeBlogDbContext.Add(user);
        _coffeeBlogDbContext.SaveChanges();

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

        _coffeeBlogDbContext.Add(user);
        _coffeeBlogDbContext.SaveChanges();

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

        _coffeeBlogDbContext.AddRange(users);
        _coffeeBlogDbContext.SaveChanges();

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

        _coffeeBlogDbContext.Add(user);
        _coffeeBlogDbContext.SaveChanges();

        // Act
        User userToUpdate = _coffeeBlogDbContext.Users.First(x => x.Id == user.Id);
        userToUpdate.Username = "newUsername";
        userToUpdate.Email = "newemail@email.com";
        userToUpdate.Password = "kjfds@#!@#djsa#@!#!@DASDA";

        int result = await _userRepository.UpdateAsync(userToUpdate);

        // Assert
        result.Should().BePositive();
        User updatedUser = _coffeeBlogDbContext.Users.First(x => x.Id == result);
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

        _coffeeBlogDbContext.Add(user);
        _coffeeBlogDbContext.SaveChanges();

        // Act
        int result = await _userRepository.DeleteAsync(user.Id);

        // Assert
        result.Should().BePositive();
        User? deletedUser = _coffeeBlogDbContext.Users.FirstOrDefault(x => x.Id == user.Id);
        deletedUser.Should().BeNull();
    }
}