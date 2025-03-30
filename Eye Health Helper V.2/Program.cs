using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.WinUI.Notifications;
using Microsoft.Win32;
namespace Eye_Health_Helper_V._2
{
    internal class Program
    {
        private static ToastContentBuilder notification = new ToastContentBuilder();
        private static ToastContentBuilder doneNotification = new ToastContentBuilder();
        static async Task Main(string[] args)
        {
            FillNotification(doneNotification, "You can now continue looking at the screen.", "Your eyes will thank you!", ToastDuration.Short);
            FillNotification(notification, "Please take a 20 seconds break.", "You should look at something 10 meters away from you.", ToastDuration.Long);
            await ShowNotificationsPeriodically();
        }
        private static async Task ShowNotificationsPeriodically()
        {
            while (true)
            {
                notification.Show();
                await Task.Delay(25000);
                doneNotification.Show();
                await Task.Delay(1200000); 
            }
        }
        private static void FillNotification(ToastContentBuilder notification, string text1, string text2, ToastDuration duration)
        {
            notification.AddText("Eye Health");
            notification.AddText(text1);
            notification.AddText(text2);
            notification.AddAppLogoOverride(new Uri(@"G:\VS Projects\Eye Health Helper V.2\Eye Health Helper V.2\bin\eye_health.png"));
            notification.SetToastDuration(duration);
            notification.SetToastScenario(ToastScenario.Reminder);
        }
    }
}
