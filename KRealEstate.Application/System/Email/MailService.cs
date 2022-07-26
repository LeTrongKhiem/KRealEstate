using KRealEstate.ViewModels.System.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace KRealEstate.Application.System.Email
{
    public class MailService : IEmailSender
    {
        private readonly MailSetting _mailSetting;
        private readonly ILogger<MailService> _logger;
        public MailService(MailSetting mailSetting, ILogger<MailService> logger)
        {
            _mailSetting = mailSetting;
            _logger = logger;
            _logger.LogInformation("Create SendMailService");
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Email);
            message.From.Add(new MailboxAddress(_mailSetting.DisplayName, _mailSetting.Email));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;
            message.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            try
            {
                smtp.Connect(_mailSetting.Host, _mailSetting.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSetting.Email, _mailSetting.Password);
                await smtp.SendAsync(message);
            }
            catch (Exception e)
            {
                // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
                Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await message.WriteToAsync(emailsavefile);

                _logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailsavefile);
                _logger.LogError(e.Message);
            }
            smtp.Disconnect(true);

            _logger.LogInformation("send mail to: " + email);
        }
    }

}
