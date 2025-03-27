using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.WinUI.Notifications;
namespace Eye_Health_Helper_V._2
{
    internal class Program
    {
        private static ToastContentBuilder notification = new ToastContentBuilder();
        static void Main(string[] args)
        {
            notification.AddText("EYE HEALTH");
            notification.AddText("Please take a 20 second break,");
            notification.AddText("staring at something 10 metres away");
            notification.SetToastScenario(ToastScenario.Reminder);
            notification.SetToastDuration(ToastDuration.Long);
            notification.AddHeroImage(new Uri(""));
            while (true)
            {
                notification.Show();
                System.Threading.Thread.Sleep(12000000);
            }
        }
    }
}
