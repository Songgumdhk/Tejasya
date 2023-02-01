using System.Threading.Tasks;
using Tejasya.Models;

namespace Tejasya.Service
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);
    }
}