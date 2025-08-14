using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace TuSaludMental.Web.Services
{
    public class ReminderWorker : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
