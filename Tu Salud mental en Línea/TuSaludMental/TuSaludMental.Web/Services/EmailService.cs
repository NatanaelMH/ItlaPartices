using System.Threading.Tasks;

namespace TuSaludMental.Web.Services
{
    public class EmailService : IEmailService
    {
        public Task SendAsync(string to, string subject, string htmlBody)
        {
            return Task.CompletedTask;
        }
    }
}
