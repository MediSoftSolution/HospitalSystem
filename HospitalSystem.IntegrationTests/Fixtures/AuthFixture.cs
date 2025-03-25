using AutoMapper;
using HospitalSystem.Application.Features.Auth.Commands.Login;
using HospitalSystem.Application.Features.Auth.Rules;
using HospitalSystem.Application.Interfaces.Tokens;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

public class AuthFixture
{
    public Mock<AuthRules> AuthRulesMock { get; }
    public Mock<UserManager<User>> UserManagerMock { get; }
    public Mock<RoleManager<Role>> RoleManagerMock { get; }
    public Mock<IMapper> MapperMock { get; }
    public Mock<IUnitOfWork> UnitOfWorkMock { get; }
    public Mock<IHttpContextAccessor> HttpContextAccessorMock { get; }
    public Mock<IConfiguration> ConfigurationMock { get; }
    public Mock<ITokenService> TokenServiceMock { get; }
    public LoginCommandHandler Handler { get; }

    public AuthFixture()
    {
        AuthRulesMock = new Mock<AuthRules>();
        UserManagerMock = MockUserManager<User>();
        RoleManagerMock = new Mock<RoleManager<Role>>(
            Mock.Of<IRoleStore<Role>>(),
            null, null, null, null);
        MapperMock = new Mock<IMapper>();
        UnitOfWorkMock = new Mock<IUnitOfWork>();
        HttpContextAccessorMock = new Mock<IHttpContextAccessor>();
        ConfigurationMock = new Mock<IConfiguration>();
        TokenServiceMock = new Mock<ITokenService>();

        HttpContextAccessorMock
            .Setup(x => x.HttpContext)
            .Returns(Mock.Of<HttpContext>);

        var mockMapper = new Mock<IMapper>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        Handler = new LoginCommandHandler(
            UserManagerMock.Object,
            ConfigurationMock.Object,
            TokenServiceMock.Object,
            AuthRulesMock.Object,
            mockMapper.Object,
            mockUnitOfWork.Object,
            HttpContextAccessorMock.Object
        );
    }

    private static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
    {
        var store = new Mock<IUserStore<TUser>>();
        return new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
    }
}
