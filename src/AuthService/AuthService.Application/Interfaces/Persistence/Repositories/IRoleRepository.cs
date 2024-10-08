﻿using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to <see cref="Role"/>.
/// </summary>
public interface IRoleRepository : IBaseRepository<Role>
{
    /// <summary>
    /// Returns all roles' names that are assigned to the user with given id.
    /// </summary>
    /// <param name="userId">User's id.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>User's all roles' names or empty collection if user with given id doesn't exist in database.</returns>
    Task<List<string>> GetAllRolesNamesByUserId(int userId,
                                                CancellationToken cancellationToken);
}