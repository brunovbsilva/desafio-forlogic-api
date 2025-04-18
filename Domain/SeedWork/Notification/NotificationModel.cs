﻿namespace Domain.SeedWork.Notification;

public class NotificationModel (string message)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Message { get; private set; } = message;
}