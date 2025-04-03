using Domain.Entities;
using Domain.SeedWork.Notification;
using Moq;

namespace Domain.Tests.Mocks;

internal class PeopleMock : BaseEntityMock<Person>
{
    private readonly Mock<INotification> _notification = new ();
    public override Person GetEntity()
    {
        return Person.Factory.Create(
            _faker.Person.FullName,
            _faker.Internet.Email(),
            _faker.Random.Bool(),
            _faker.Random.Int(0, 100),
            _faker.Address.FullAddress(),
            _faker.Lorem.Sentence(),
            _faker.Lorem.Sentence(),
            _faker.Lorem.Sentence(),
            _faker.Lorem.Sentence(),
            _notification.Object
        );
    }
}
