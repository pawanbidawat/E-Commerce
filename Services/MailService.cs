

using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using TemporalBoxApi.Configuration;
using TemporalBoxApi.Interfaces;
using TemporalBoxApi.Models;

namespace TemporalBoxApi.Services
{
    public class MailService : IMailService
    {
        
        private readonly IConfiguration _configuration;
        private readonly MailSettings _mailSettings;
        public MailService( IConfiguration configuration ,IOptions<MailSettings> mailsetting)
        {            
            _configuration = configuration;
            _mailSettings = mailsetting.Value;
        }

        public bool SendMail(EmailData mailData)
       {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("rohankumawat.pinkcity@gmail.com");
                message.To.Add(new MailAddress(mailData.EmailToId));
                message.Subject = mailData.EmailSubject;                
                message.Body = mailData.EmailBody;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp-relay.brevo.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("rohankumawat.pinkcity@gmail.com", "fvD6nHztdKw5qRhy");
                smtp.EnableSsl = true;
                smtp.Send(message);
                smtp.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                // Exception Details
                return false;
            }
        }

   
    }
}


//MimeMail example for sending mail
//public bool SendMail(EmailData mailData)
//{
//    try
//    {
//        using (MimeMessage emailMessage = new MimeMessage())
//        {
//            MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
//            emailMessage.From.Add(emailFrom);
//            MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
//            emailMessage.To.Add(emailTo);

//            emailMessage.Subject = mailData.EmailSubject;

//            BodyBuilder emailBodyBuilder = new BodyBuilder();
//            emailBodyBuilder.HtmlBody = mailData.EmailBody;

//            emailMessage.Body = emailBodyBuilder.ToMessageBody();
//            //this is the SmtpClient from the Mailkit.Net.Smtp namespace, not the System.Net.Mail one
//            using (SmtpClient mailClient = new SmtpClient())
//            {
//                mailClient.Connect(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
//                mailClient.Authenticate(_mailSettings.UserName, _mailSettings.Password);
//                mailClient.Send(emailMessage);
//                mailClient.Disconnect(true);
//            }
//        }

//        return true;
//    }
//    catch (Exception ex)
//    {
//        // Exception Details
//        return false;
//    }
//}
