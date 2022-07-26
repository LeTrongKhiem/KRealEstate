namespace KRealEstate.Application.System.Email
{
    public interface IMailService
    {
        Task SendMailAsync(string email, string subject, string htmlMessage);
    }
}
