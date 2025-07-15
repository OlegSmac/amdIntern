using Microsoft.Extensions.Logging;
using NSubstitute;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Users.Users.Commands;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Tests.CommandHandlers.Users;

public class CreateUserHandlerTests
{
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IUserRepository _userRepositoryMock;

    public CreateUserHandlerTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        
        _unitOfWorkMock.UserRepository.Returns(_userRepositoryMock);
        
        _unitOfWorkMock.ExecuteTransactionAsync(Arg.Any<Func<Task>>())
            .Returns(callInfo =>
            {
                var func = callInfo.Arg<Func<Task>>();
                return func();
            });
    }

    [Fact]
    public async Task CreateUser_ValidCommand_ShouldCreateUser()
    {
        /*var handler = new CreateUserHandler(_unitOfWorkMock, Substitute.For<ILogger<CreateUserHandler>>());
        var command = new CreateUser("Oleg", "oleg@email.com", "password");

        var actualResult = await handler.Handle(command, default);

        Assert.Equal(command.Name, actualResult.Name);
        Assert.Equal(command.Email, actualResult.Email);

        await _userRepositoryMock.Received(1)
            .CreateAsync(Arg.Is<User>(x =>
                x.Name == command.Name &&
                x.Email == command.Email));*/

        await _unitOfWorkMock.Received(1).SaveAsync();
    }
}
