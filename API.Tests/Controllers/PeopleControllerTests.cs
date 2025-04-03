using API.Controllers;
using Bogus;
using Domain.Common;
using Domain.Entities.Dtos;
using Domain.Services;
using Domain.Services.Requests;
using Domain.Services.Responses;
using Domain.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.Tests.Controllers;

public class PeopleControllerTests
{
    private readonly Mock<IPeopleService> _service;
    private readonly PeopleController _controller;
    private readonly Faker _faker;
    private readonly PeopleMock _peopleMock;

    public PeopleControllerTests()
    {
        _service = new();
        _controller = new(_service.Object);
        _faker = new("pt_BR");
        _peopleMock = new();
    }

    [Fact]
    public async Task RegisterPersonAsync_ShouldCallService()
    {
        // Arrange
        var request = new RegisterPersonRequest
        {
            Name = _faker.Person.FullName,
            Email = _faker.Internet.Email(),
            Active = _faker.Random.Bool(),
            Age = _faker.Random.Int(1, 100),
            Address = _faker.Address.FullAddress(),
            OtherInformation = _faker.Lorem.Sentence(),
            Interests = _faker.Lorem.Sentence(),
            Feelings = _faker.Lorem.Sentence(),
            Values = _faker.Lorem.Sentence()
        };
        _service.Setup(x => x.RegisterPersonAsync(It.IsAny<RegisterPersonRequest>()))
            .ReturnsAsync(new GenericResponse<PersonDto>(_peopleMock.GetEntity()));

        // Act
        var result = await _controller.RegisterPersonAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        _service.Verify(v => v.RegisterPersonAsync(It.IsAny<RegisterPersonRequest>()), Times.Once);
    }

    [Fact]
    public async Task ListPersonAsync_ShouldCallService()
    {
        // Arrange
        var people = _peopleMock.GetEnumerableWithEntities(_faker.Random.Int(5, 10)).Select(x => (PersonDto)x).ToList();
        _service.Setup(x => x.ListPersonAsync())
            .ReturnsAsync(new GenericResponse<List<PersonDto>>(people));

        // Act
        var result = await _controller.ListPersonAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        _service.Verify(v => v.ListPersonAsync(), Times.Once);
    }

    [Fact]
    public async Task GetHomeScoreAsync_ShouldCallService()
    {
        // Arrange
        var scores = new HomeScoreResponse
        {
            TotalScore = _faker.Random.Int(0, 100),
            PendingScore = _faker.Random.Int(0, 100),
            LastMonthScore = _faker.Random.Int(0, 100),
        };
        _service.Setup(x => x.GetHomeScoreAsync())
            .ReturnsAsync(new GenericResponse<HomeScoreResponse>(scores));

        // Act
        var result = await _controller.GetHomeScoreAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        _service.Verify(v => v.GetHomeScoreAsync(), Times.Once);
    }
}
