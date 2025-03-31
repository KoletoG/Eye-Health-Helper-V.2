using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.WinUI.Notifications;
using Microsoft.Extensions.Logging;
using static Eye_Health_Helper_V._2.Interfaces;

namespace Eye_Health_Helper_V._2
{
    public class NotificationService : INotificationsService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly string _fullPath;
        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
            _fullPath = Path.GetFullPath("eye_health.png");
        }
        public async Task ShowNotificationsPeriodically(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    ShowNotification("Please take a 20 seconds break.", "You should look at something 10 meters away from you.", ToastDuration.Long);
                    await Task.Delay(25000, cancellationToken);
                    ShowNotification("You can now continue looking at the screen.", "Your eyes will thank you!", ToastDuration.Short);
                    await Task.Delay(1200000, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Service has been cancelled");
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
        }
        private void ShowNotification(string text1, string text2, ToastDuration duration)
        {
            try
            {
                new ToastContentBuilder()
                .AddText("Eye Health")
                .AddText(text1)
                .AddText(text2)
                .AddAppLogoOverride(new Uri(_fullPath, UriKind.Absolute))
                .SetToastDuration(duration)
                .SetToastScenario(ToastScenario.Reminder)
                .Show();
            }
            catch (UriFormatException e)
            {
                _logger.LogError(e.Message);
            }
            catch (FileNotFoundException e)
            {
                _logger.LogError(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
        }
    }
}
