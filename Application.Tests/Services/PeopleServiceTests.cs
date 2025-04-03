using Application.Services;
using Bogus;
using Domain.Common;
using Domain.Entities.Dtos;
using Domain.Exceptions;
using Domain.Repository;
using Domain.SeedWork.Notification;
using Domain.Services;
using Domain.Services.Requests;
using Domain.Services.Responses;
using Domain.Tests.Mocks;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Tests.Services;

[Trait("Service", "People")]
public class PeopleServiceTests
{
    private readonly Mock<INotification> _notification;
    private readonly Mock<IPeopleRepository> _repository;
    private readonly Mock<IDistributedCache> _cache;
    private readonly IPeopleService _service;
    private readonly Faker _faker;
    private readonly PeopleMock _peopleMock;

    public PeopleServiceTests()
    {
        _notification = new();
        _repository = new();
        _cache = new();

        _service = new PeopleService(_notification.Object, _repository.Object, _cache.Object);
        _faker = new("pt_BR");
        _peopleMock = new();
    }

    [Fact]
    public async Task GetHomeScoreAsync_ShouldReturnHomeScoreResponse()
    {
        // Arrange
        var expectedResponse = new HomeScoreResponse
        {
            TotalScore = _faker.Random.Int(1, 100),
            PendingScore = _faker.Random.Int(1, 100),
            LastMonthScore = _faker.Random.Int(1, 100)
        };
        _repository.Setup(x => x.GetHomeScoreAsync()).ReturnsAsync(expectedResponse);

        // Act
        var response = await _service.GetHomeScoreAsync();

        // Assert
        Assert.NotNull(response);
        Assert.IsType<GenericResponse<HomeScoreResponse>>(response);
        Assert.Equal(expectedResponse, response.Result);
    }

    [Fact]
    public async Task ListPersonAsync_ShouldReturnListOfPersonDto()
    {
        // Arrange
        var personList = _peopleMock.GetEnumerableWithEntities(_faker.Random.Int(5, 10));
        _repository.Setup(x => x.GetAll()).Returns(personList.AsQueryable());

        // Act
        var response = await _service.ListPersonAsync();

        // Assert
        Assert.NotNull(response);
        Assert.IsType<GenericResponse<List<PersonDto>>>(response);
        Assert.Equal(personList.Count(), response.Result?.Count);
    }

    #region RegisterPersonAsync
    [Fact]
    public async Task RegisterPersonAsync_ShouldReturnPersonDto()
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
            Feelings = _faker.Lorem.Sentence(),
            Interests = _faker.Lorem.Sentence(),
            Values = _faker.Lorem.Sentence(),
        };

        // Act
        var response = await _service.RegisterPersonAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.IsType<GenericResponse<PersonDto>>(response);
        Assert.Equal(request.Name, response.Result?.Name);
        Assert.Equal(request.Email, response.Result?.Email);
        Assert.Equal(request.Active, response.Result?.Active);
    }

    [Fact]
    public async Task RegisterPersonAsync_MinimalConditions()
    {
        // Arrange
        var request = new RegisterPersonRequest
        {
            Name = _faker.Person.FullName,
            Email = _faker.Internet.Email(),
            Active = _faker.Random.Bool()
        };

        // Act
        var response = await _service.RegisterPersonAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.IsType<GenericResponse<PersonDto>>(response);
        Assert.Equal(request.Name, response.Result?.Name);
        Assert.Equal(request.Email, response.Result?.Email);
        Assert.Equal(request.Active, response.Result?.Active);
    }

    [Fact]
    public async Task RegisterPersonAsync_ThrowsErrorOnInvalidProperties()
    {
        // Arrange
        var request = new RegisterPersonRequest
        {
            Name = string.Empty,
            Email = string.Empty,
            Active = _faker.Random.Bool(),
        };
        _notification.Setup(x => x.HasNotification).Returns(true);

        // Act
        var response = async () => await _service.RegisterPersonAsync(request);

        // Assert
        await Assert.ThrowsAsync<NotificationException>(response);
        #endregion
    }
}
