using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;
public class NotificationManager : MonoBehaviour
{
    float _time;

    public NotificationManager(float time)
    {
        _time = time;
    }

    public NotificationManager()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        var notifChannel = new AndroidNotificationChannel()
        {
            Id = "reminder_notif_ch",
            Name = "Reminder Notification",
            Description = "Disturb the user",
            Importance = Importance.High
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notifChannel);

        var notification = new AndroidNotification();
        notification.Title = "Get your ship ready";
        notification.Text = "¡We have a galaxy to save!";
        notification.FireTime = DateTime.Now.AddSeconds(_time);
        notification.SmallIcon = "icon_reminders";

        AndroidNotificationCenter.SendNotification(notification, "reminder_notif_ch");
    }
}