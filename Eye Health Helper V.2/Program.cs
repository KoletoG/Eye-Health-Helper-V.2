using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.WinUI.Notifications;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Services.Description;
using static Eye_Health_Helper_V._2.Program;
namespace Eye_Health_Helper_V._2
{
    internal class Program
    {
        private static readonly IServiceProvider services;
        static Program()
        {
            var collection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
            collection.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog(); // Use Serilog as the logging provider
            });
            collection.AddSingleton<INotificationsService, NotificationService>();
            services=collection.BuildServiceProvider();
        }
        static async Task Main(string[] args)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            var notificationService = services.GetRequiredService<INotificationsService>();
            try
            {
                logger.LogInformation("App started");
                var cts = new CancellationTokenSource();
                _ = notificationService.ShowNotificationsPeriodically(cts.Token);
                Console.WriteLine("Eye Health Helper is running... Press Enter to exit.");
                Console.ReadLine();
                cts.Cancel();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                logger.LogError(e.ToString());
            }
            finally
            {
               logger.LogInformation("App has stopped");
                Log.CloseAndFlush();
            }
        }
        interface INotificationsService
        {
            Task ShowNotificationsPeriodically(CancellationToken token);
        }
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
}
