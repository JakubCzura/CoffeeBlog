﻿using PostManager.Domain.Exceptions;
using PostManager.Domain.Models.Users;

namespace PostManager.Application.Interfaces.Security.CurrentUsers;

/// <summary>
/// Interface to deliver information about current signed in, authorized user.
/// </summary>
public interface ICurrentUserContext
{
    /// <summary>
    /// Returns information about current authorized user.
    /// </summary>
    /// <returns>Information about user who has successfully signed in and is authorized.</returns>
    /// <exception cref="UserUnauthorizedException">When user is unauthorized.</exception>
    public CurrentAuthorizedUser GetCurrentAuthorizedUser();
}