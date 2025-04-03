using Domain.Exceptions;
using Domain.SeedWork.Notification;
using Domain.Services;

namespace Application.Services;

public abstract class BaseService(INotification notification) : IBaseService
{
    public void AddNotification(string message) => notification.AddNotification(message);
    public void ThrowNotificationException(string message)
    {
        AddNotification(message);
        throw new NotificationException();
    }
}
