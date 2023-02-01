using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Tejasya.Models;

namespace Tejasya.Service
{
    public class EmailService : IEmailService
    {

        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpconfig;


        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolers( "Hello {{UserName}} This is test email Subject frmm tejasya web app", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolers (GetEmailBody("TestEmail"),userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);
        }


        public async Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolers("Hello {{UserName}}, Confirm your email id", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolers(GetEmailBody("EmailConfirm"), userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);
        }


        public EmailService(IOptions<SMTPConfigModel> smtpconfig)
        {
            _smtpconfig = smtpconfig.Value;
        }


        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpconfig.SenderAddress, _smtpconfig.DisplayName),
                IsBodyHtml = _smtpconfig.IsBodyHTMl
            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }
            NetworkCredential networkCredential = new NetworkCredential(_smtpconfig.UserName, _smtpconfig.Password);
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpconfig.Host,
                Port = _smtpconfig.Port,
                EnableSsl = _smtpconfig.EnableSSL,
                UseDefaultCredentials = _smtpconfig.UserDefaultCredential,
                Credentials = networkCredential
            };
            mail.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mail);
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }

        private string UpdatePlaceHolers(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs !=null)
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }

            return text;

        }

    }
}
