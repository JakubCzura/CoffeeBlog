using AuthService.Application.Dtos.UserDiagnostics;

namespace AuthService.Application.Interfaces.Persistence.Repositories;

/// <summary>
/// Interface for repository to perform database operations related to users' diagnostics.
/// </summary>
public interface IUserDiagnosticDataRepository
{
    /// <summary>
    /// Returns users' diagnostic data for the specified date.
    /// </summary>
    /// <param name="dataCollectedAt">Date when data is collected.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Diagnostic data about users.</returns>
    Task<GetUsersDiagnosticDataResultDto> GetUsersDiagnosticDataAsync(DateTime dataCollectedAt,
                                                                      CancellationToken cancellationToken);
}