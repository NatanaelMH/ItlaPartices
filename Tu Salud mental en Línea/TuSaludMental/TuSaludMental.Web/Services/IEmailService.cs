using System.Threading.Tasks;

namespace TuSaludMental.Web.Services
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string htmlBody);
    }
}
