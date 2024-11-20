using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HospitalSystem.Application.Features.Auth.Commands.Register;
using HospitalSystem.Application.Features.Auth.Rules;
using HospitalSystem.Application.Interfaces.AutoMapper;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using HospitalSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace HospitalSystem.Application.Tests.Features.Auth.Commands
{
    public class RegisterCommandTests : IClassFixture<AuthFixture>
    {
        private readonly RegisterCommandHandler _handler;
        private readonly AuthFixture _fixture;

        public RegisterCommandTests(AuthFixture fixture)
        {
            _fixture = fixture;
            _handler = new RegisterCommandHandler(
                _fixture.AuthRulesMock.Object,
                _fixture.UserManagerMock.Object,
                _fixture.RoleManagerMock.Object,
                _fixture.MapperMock.Object,
                _fixture.UnitOfWorkMock.Object,
                _fixture.HttpContextAccessorMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldRegisterUser_WhenRequestIsValid()
        {
            // Arrange
            var request = new RegisterCommandRequest
            {
                Email = "test@example.com",
                Password = "Test@123"
            };

            var user = new User
            {
                Email = request.Email,
                UserName = request.Email
            };

            _fixture.MapperMock
                .Setup(m => m.Map<User>(It.IsAny<RegisterCommandRequest>(), null))
                .Returns(user);

            _fixture.AuthRulesMock
                .Setup(r => r.UserShouldNotBeExist(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            _fixture.UserManagerMock
                .Setup(um => um.FindByEmailAsync(request.Email))
                .ReturnsAsync((User)null);

            _fixture.UserManagerMock
                .Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _fixture.RoleManagerMock
                .Setup(rm => rm.RoleExistsAsync("user"))
                .ReturnsAsync(false);

            _fixture.RoleManagerMock
                .Setup(rm => rm.CreateAsync(It.IsAny<Role>()))
                .ReturnsAsync(IdentityResult.Success);

            _fixture.UserManagerMock
                .Setup(um => um.AddToRoleAsync(It.IsAny<User>(), "user"))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);

            _fixture.AuthRulesMock.Verify(r => r.UserShouldNotBeExist(It.IsAny<User>()), Times.Once);
            _fixture.UserManagerMock.Verify(um => um.CreateAsync(It.IsAny<User>(), request.Password), Times.Once);
            _fixture.RoleManagerMock.Verify(rm => rm.RoleExistsAsync("user"), Times.Once);
            _fixture.RoleManagerMock.Verify(rm => rm.CreateAsync(It.IsAny<Role>()), Times.Once);
            _fixture.UserManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), "user"), Times.Once);
        }
    }
}