using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.WinUI.Notifications;
using Microsoft.Win32;
namespace Eye_Health_Helper_V._2
{
    internal class Program
    {
        private static readonly CancellationTokenSource cts = new CancellationTokenSource();
        private static readonly string fullPath = Path.GetFullPath("eye_health.png");
        static async Task Main(string[] args)
        {
            Console.WriteLine("Eye Health Helper is running... Press Enter to exit.");
            _ = ShowNotificationsPeriodically(cts.Token);
            Console.ReadLine();
            cts.Cancel();
        }
        private static async Task ShowNotificationsPeriodically(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                ShowNotification("Please take a 20 seconds break.", "You should look at something 10 meters away from you.", ToastDuration.Long);
                await Task.Delay(25000,cancellationToken);
                ShowNotification("You can now continue looking at the screen.", "Your eyes will thank you!", ToastDuration.Short);
                await Task.Delay(1200000, cancellationToken); 
            }
        }
        private static void ShowNotification(string text1, string text2, ToastDuration duration)
        {
            new ToastContentBuilder()
            .AddText("Eye Health")
            .AddText(text1)
            .AddText(text2)
            .AddAppLogoOverride(new Uri(fullPath,UriKind.Absolute))
            .SetToastDuration(duration)
            .SetToastScenario(ToastScenario.Reminder)
            .Show();
        }
    }
}
