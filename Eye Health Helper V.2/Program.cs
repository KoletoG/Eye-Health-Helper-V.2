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
            notification.AddText("EYE HEALTH");
            notification.AddAppLogoOverride(new Uri(@"G:\VS Projects\Eye Health Helper V.2\Eye Health Helper V.2\bin\eye_health.png"));
            notification.AddText("Please take a 20 second break,");
            notification.AddText("staring at something 10 metres away");
            notification.SetToastScenario(ToastScenario.Reminder);
            notification.SetToastDuration(ToastDuration.Long);
            doneNotification.AddText("EYE HEALTH");
            doneNotification.AddAppLogoOverride(new Uri(@"G:\VS Projects\Eye Health Helper V.2\Eye Health Helper V.2\bin\eye_health.png"));
            doneNotification.AddText("You can now continue looking at the screen.");
            doneNotification.AddText("Your eyes will thank you!");
            doneNotification.SetToastDuration(ToastDuration.Short);
            doneNotification.SetToastScenario(ToastScenario.Reminder);
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
    }
}
