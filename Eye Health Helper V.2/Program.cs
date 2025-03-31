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
using static Eye_Health_Helper_V._2.Interfaces;
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
       
    }
}
