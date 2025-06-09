using Microsoft.Extensions.Logging;
using NSubstitute;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Users.Users.Queries;
using Vehicles.Domain.Users.Models;
using Xunit;

namespace Vehicles.Tests.QueryHandlers.Users;

public class GetUserByIdHandlerTests
{
    private readonly IUnitOfWork _unitOfWorkMock;

    public GetUserByIdHandlerTests()
    {
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public async Task GetUserById_WithExistingUser_ShouldReturnUser()
    {
        var userId = 2;
        var handler = new GetUserByIdHandler(_unitOfWorkMock, Substitute.For<ILogger<GetUserByIdHandler>>());
        var query = new GetUserById(userId);

        var expectedResult = new User() { Name = "OlegRegular", Email = "oleg@example.com" };
        
        _unitOfWorkMock.UserRepository.GetByIdAsync(Arg.Any<int>()).Returns(expectedResult);
        
        var actualResult = await handler.Handle(query, default);
        
        Assert.Equal(expectedResult.Name, actualResult.Name);
        Assert.Equal(expectedResult.Email, actualResult.Email);
        
        await _unitOfWorkMock.UserRepository.Received(1).GetByIdAsync(Arg.Is<int>(x => x == userId));
    }
    
    [Fact]
    public async Task GetUserById_WithNonExistingUser_ShouldThrowKeyNotFoundException()
    {
        var userId = 1;
        var handler = new GetUserByIdHandler(_unitOfWorkMock, Substitute.For<ILogger<GetUserByIdHandler>>());
        var query = new GetUserById(userId);
        
        _unitOfWorkMock.UserRepository.GetByIdAsync(Arg.Any<int>()).Returns((User?)null);
        
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(query, default));

        Assert.Equal($"User with id: {userId} not found", exception.Message);
    }

}