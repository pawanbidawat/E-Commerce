using TemporalBoxApi.Models;

namespace TemporalBoxApi.Interfaces
{
    public interface IMailService
    {
        bool SendMail(EmailData emailData);

    }
}
