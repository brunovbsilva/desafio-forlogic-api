using Domain.SeedWork.Notification;
using Domain.Services;

namespace Application.Services
{
    public class PeopleService(INotification notification) : BaseService(notification), IPeopleService
    {
    }
}
