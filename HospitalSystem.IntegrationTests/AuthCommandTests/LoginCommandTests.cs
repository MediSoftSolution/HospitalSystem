using HospitalSystem.Application.Features.Auth.Commands.Login;
using HospitalSystem.Application.Features.Auth.Exceptions;
using HospitalSystem.Application.Features.Auth.Rules;
using HospitalSystem.Application.Interfaces.Tokens;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using Xunit;

public class LoginCommandTests : IClassFixture<AuthFixture>
{
    private readonly AuthFixture _fixture;

    public LoginCommandTests(AuthFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenLoginIsSuccessful()
    {
        // Arrange
        var user = new User { Email = "test@example.com" };
        var roles = new List<string> { "Admin" };
        var token = new JwtSecurityToken();
        string refreshToken = "dummy_refresh_token";

        _fixture.UserManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _fixture.UserManagerMock.Setup(um => um.CheckPasswordAsync(user, It.IsAny<string>()))
            .ReturnsAsync(true);
        _fixture.UserManagerMock.Setup(um => um.GetRolesAsync(user))
            .ReturnsAsync(roles);
        _fixture.TokenServiceMock.Setup(ts => ts.CreateToken(user, roles))
            .ReturnsAsync(token);
        _fixture.TokenServiceMock.Setup(ts => ts.GenerateRefreshToken())
            .Returns(refreshToken);
        _fixture.ConfigurationMock.Setup(cfg => cfg["JWT:RefreshTokenValidityInDays"])
            .Returns("7");

        var request = new LoginCommandRequest
        {
            Email = "test@example.com",
            Password = "Password123!"
        };

        // Act
        var response = await _fixture.Handler.Handle(request, default);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(refreshToken, response.RefreshToken);
        Assert.NotNull(response.Token);
        Assert.Equal(token.ValidTo, response.Expiration);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenEmailOrPasswordIsInvalid()
    {
        // Arrange
        _fixture.UserManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null);

        var request = new LoginCommandRequest
        {
            Email = "invalid@example.com",
            Password = "WrongPassword"
        };

        // Act & Assert
        await Assert.ThrowsAsync<EmailOrPasswordShouldNotBeInvalidException>(() =>
            _fixture.Handler.Handle(request, default));
    }
}
