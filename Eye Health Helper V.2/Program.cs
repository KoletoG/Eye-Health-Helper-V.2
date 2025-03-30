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
        static async Task Main(string[] args)
        {
            await ShowNotificationsPeriodically();
        }
        private static async Task ShowNotificationsPeriodically()
        {
            while (true)
            {
                ShowNotification("You can now continue looking at the screen.", "Your eyes will thank you!", ToastDuration.Short);
                await Task.Delay(25000);
                ShowNotification("Please take a 20 seconds break.", "You should look at something 10 meters away from you.", ToastDuration.Long);
                await Task.Delay(1200000); 
            }
        }
        private static void ShowNotification(string text1, string text2, ToastDuration duration)
        {
            new ToastContentBuilder()
            .AddText("Eye Health")
            .AddText(text1)
            .AddText(text2)
            .AddAppLogoOverride(new Uri(@"G:\VS Projects\Eye Health Helper V.2\Eye Health Helper V.2\bin\eye_health.png"))
            .SetToastDuration(duration)
            .SetToastScenario(ToastScenario.Reminder)
            .Show();
        }
    }
}
