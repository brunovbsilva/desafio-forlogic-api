using Domain.Entities;
using Domain.Exceptions;
using Domain.SeedWork.Notification;
using Domain.Services.Requests;
using Domain.Tests.Mocks;
using Moq;

namespace Domain.Tests.Entities;

[Trait("Entity", "Person")]
public class PeopleTests
{
    private readonly PeopleMock _mock;
    private readonly Mock<INotification> _notification;

    public PeopleTests()
    {
        _mock = new ();
        _notification = new ();
    }

    [Fact]
    public void CreatePerson()
    {
        // Arrange
        var person = _mock.GetEntity();

        // Act
        var newPerson = Person.Factory.Create(
            person.Name,
            person.Email,
            person.Active,
            person.Age,
            person.Address,
            person.OtherInformation,
            person.Interests,
            person.Feelings,
            person.Values,
            _notification.Object
        );

        // Assert
        Assert.NotNull(newPerson);
        Assert.IsType<Person>(newPerson);
        Assert.Equal(person.Name, newPerson.Name);
        Assert.Equal(person.Email, newPerson.Email);
        Assert.Equal(person.Active, newPerson.Active);
        Assert.Equal(person.Age, newPerson.Age);
        Assert.Equal(person.Address, newPerson.Address);
        Assert.Equal(person.OtherInformation, newPerson.OtherInformation);
        Assert.Equal(person.Interests, newPerson.Interests);
        Assert.Equal(person.Feelings, newPerson.Feelings);
        Assert.Equal(person.Values, newPerson.Values);
    }

    [Fact]
    public void CreatePersonByRegisterRequest()
    {
        // Arrange
        var person = _mock.GetEntity();
        var request = new RegisterPersonRequest
        {
            Name = person.Name,
            Email = person.Email,
            Active = person.Active
        };

        // Act
        var newPerson = Person.Factory.Create(
            request,
            _notification.Object
        );

        // Assert
        Assert.NotNull(newPerson);
        Assert.IsType<Person>(newPerson);
        Assert.Equal(person.Name, newPerson.Name);
        Assert.Equal(person.Email, newPerson.Email);
        Assert.Equal(person.Active, newPerson.Active);
    }

    [Fact]
    public void CreatePersonWithInvalidParams()
    {
        // Arrange
        _notification.Setup(x => x.HasNotification).Returns(true);

        // Act
        var newPerson = () => Person.Factory.Create(
            string.Empty,
            string.Empty,
            false,
            null,
            null,
            null,
            null,
            null,
            null,
            _notification.Object
        );

        // Assert
        Assert.Throws<NotificationException>(newPerson);
    }
}
