﻿namespace BookShelf.Core.Interfaces;

public interface INotificationService
{
    Task SendAsync(string to, string subject, string message);
}
