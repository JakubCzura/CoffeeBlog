using CoffeeBlog.Application.Dtos.Authentication;
using CoffeeBlog.Application.Interfaces.Helpers;
using CoffeeBlog.Domain.SettingsOptions.Authentication;
using CoffeeBlog.Infrastructure.Security.Authentication;
using CoffeeBlog.Infrastructure.UnitTests.HelpersForTests;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoffeeBlog.Infrastructure.UnitTests.Security.Authentication;

public class JwtServiceTests
{
    private readonly AuthenticationOptions _authenticationOptions = ConfigurationProviderHelper.InitializeConfiguration()
                                                                                               .GetSection(AuthenticationOptions.AppsettingsKey)
                                                                                               .Get<AuthenticationOptions>()!;

    private readonly DateTime _utcNow = DateTime.UtcNow;
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock = new();

    private readonly JwtService _jwtService;

    public JwtServiceTests()
    {
        _dateTimeProviderMock.Setup(x => x.UtcNow).Returns(_utcNow);
        _jwtService = new(Options.Create(_authenticationOptions), _dateTimeProviderMock.Object);
    }

    public static TheoryData<IEnumerable<string>, IEnumerable<Claim>> CreateToken_should_ReturnToken_when_RolesAndClaimsAreGiven_Data => new()
    {
        { new List<string>() { "User" }, new List<Claim>() { new("MyClaim", "MyClaimValue") } },
        { new List<string>() { "User", "Admin" }, new List<Claim>() { new("ClaimForTest", "ClaimForTestValue"), new("SuperClaim", "SuperClaimValue") } }
    };

    [Theory]
    [MemberData(nameof(CreateToken_should_ReturnToken_when_RolesAndClaimsAreGiven_Data))]
    public void CreateToken_should_ReturnToken_when_RolesAndClaimsAreGiven(IEnumerable<string> userRoles, IEnumerable<Claim> claims)
    {
        //Arrange
        CreateJwtTokenDto createJwtTokenUserDetailsDto = new(1, "Username", "email@email.com");

        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationOptions.Jwt.SecretKey)),
            ValidateIssuer = true,
            ValidIssuer = _authenticationOptions.Jwt.Issuer,
            ValidateAudience = true,
            ValidAudience = _authenticationOptions.Jwt.Audience,
            RequireExpirationTime = true,
            LifetimeValidator = (before, expires, token, param) => DateTime.Compare(expires!.Value, _utcNow.AddMinutes(_authenticationOptions.Jwt.LifetimeInMinutes)) <= 0,
            ValidateLifetime = true,
        };

        //Act
        string token = _jwtService.CreateToken(createJwtTokenUserDetailsDto, userRoles, claims);

        //Assert
        token.Should().NotBeNullOrEmpty();

        ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

        claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value.Should().Be(createJwtTokenUserDetailsDto.Id.ToString());
        claimsPrincipal.FindFirst(ClaimTypes.Name)!.Value.Should().Be(createJwtTokenUserDetailsDto.Username);
        claimsPrincipal.FindFirst(ClaimTypes.Email)!.Value.Should().Be(createJwtTokenUserDetailsDto.Email);

        IEnumerable<Claim> rolesFromToken = claimsPrincipal.FindAll(x => x.Type == ClaimTypes.Role);
        rolesFromToken.Select(x => x.Value).Should().BeEquivalentTo(userRoles);

        IEnumerable<Claim> claimsFromToken = claimsPrincipal.Claims.Where(x => claims.Any(y => y.Type == x.Type));
        claimsFromToken.Select(x => x.Value).Should().BeEquivalentTo(claims.Select(x => x.Value));
    }

    public static TheoryData<IEnumerable<string>?> CreateToken_should_ReturnToken_when_RolesOrClaimsAreEmpty_Data() => new()
    {
        null,
        new List<string>()
    };

    [Theory]
    [MemberData(nameof(CreateToken_should_ReturnToken_when_RolesOrClaimsAreEmpty_Data))]
    public void CreateToken_should_ReturnToken_when_RolesOrClaimsAreEmpty(IEnumerable<string>? userRoles)
    {
        //Arrange
        CreateJwtTokenDto createJwtTokenUserDetailsDto = new(1, "Username", "email@email.com");

        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationOptions.Jwt.SecretKey)),
            ValidateIssuer = true,
            ValidIssuer = _authenticationOptions.Jwt.Issuer,
            ValidateAudience = true,
            ValidAudience = _authenticationOptions.Jwt.Audience,
            RequireExpirationTime = true,
            LifetimeValidator = (before, expires, token, param) => DateTime.Compare(expires!.Value, _utcNow.AddMinutes(_authenticationOptions.Jwt.LifetimeInMinutes)) <= 0,
            ValidateLifetime = true,
        };

        //Act
        string token = _jwtService.CreateToken(createJwtTokenUserDetailsDto, userRoles);

        //Assert
        token.Should().NotBeNullOrEmpty();

        ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

        claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value.Should().Be(createJwtTokenUserDetailsDto.Id.ToString());
        claimsPrincipal.FindFirst(ClaimTypes.Name)!.Value.Should().Be(createJwtTokenUserDetailsDto.Username);
        claimsPrincipal.FindFirst(ClaimTypes.Email)!.Value.Should().Be(createJwtTokenUserDetailsDto.Email);

        //Roles can be null or empty for example when using [Authorize] attribute without roles
        claimsPrincipal.Claims.Should().HaveCount(6); //3 claims from CreateJwtTokenUserDetailsDto and 3 default claims, so no additional roles or claims are given
    }

    [Fact]
    public void CreateToken_should_ThrowArgumentNullException_when_DtoWithUserDetailsIsNull()
        => _jwtService.Invoking(x => x.CreateToken(null!))
                      .Should()
                      .Throw<ArgumentNullException>();
}