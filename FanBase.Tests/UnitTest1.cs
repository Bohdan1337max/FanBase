using Castle.Components.DictionaryAdapter.Xml;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using WarehouseManagementSystem.Controllers;
using WarehouseManagementSystem.DataBase;
using WarehouseManagementSystem.DataTransferModels;
using WarehouseManagementSystem.Models;
using WarehouseManagementSystem.Repositories;

namespace FanBase.Tests;

public class Tests
{
    private Mock<IAuthRepository> _mockRepository;
    private AuthController _authController;

    [SetUp]
    public void SetUp()
    {
        var contextOptions = new DbContextOptionsBuilder<WmsDbContext>()
            .UseInMemoryDatabase("test")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
        var context = new WmsDbContext(contextOptions);
        _mockRepository = new Mock<IAuthRepository>();
        _authController = new AuthController(context,_mockRepository.Object);
    }
    private const string Jwt =
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
    [Test]
    public void SignUp_WithValidInput_ShouldReturnJwtToken()
    {
        // Arrange
        _mockRepository.Setup(x => x.RegisterNewUser("a", "b", "c")).Returns(Jwt);
        var testInput = new SignUpRequest
        {
            Email = "a",
            UserName = "b",
            Password = "c"
        };
        
        // Act
        var result = _authController.SignUp(testInput);
        var okResult = result as OkObjectResult;
        
        // Assert
        okResult.Should().NotBeNull();
        (okResult!.Value as SignUpResponse)!.Token.Should().Be(Jwt);
    }

    [Test]
    public void SignUp_WhenUserAlreadyExists_ShouldReturnBadRequest()
    {
        // Arrange 
        _mockRepository.Setup(x => 
                x.RegisterNewUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns((string?)null);

        // Act
        var result = _authController.SignUp(new SignUpRequest());
        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
        (result as BadRequestObjectResult)!.Value.Should().Be("User already exist");
    }
    
}