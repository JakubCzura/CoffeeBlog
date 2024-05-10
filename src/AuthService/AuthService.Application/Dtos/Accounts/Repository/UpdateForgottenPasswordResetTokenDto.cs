namespace AuthService.Application.Dtos.Accounts.Repository
{
    /// <summary>
    /// Details to update forgotten password reset token and its expiration date.
    /// </summary>
    /// <param name="UserEmail">User's e-mail.</param>
    /// <param name="ForgottenPasswordResetToken">Token to validate user's identity.</param>
    /// <param name="ForgottenPasswordResetTokenExpiresAt">Expiration date of token.</param>
    public record UpdateForgottenPasswordResetTokenDto(string UserEmail, 
                                                       string ForgottenPasswordResetToken,
                                                       DateTime ForgottenPasswordResetTokenExpiresAt);
}