using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eye_Health_Helper_V._2
{
    public class Interfaces
    {
        public interface INotificationsService
        {
            Task ShowNotificationsPeriodically(CancellationToken token);
        }
    }
}
