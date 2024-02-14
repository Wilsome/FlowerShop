using FlowerShop.Dto;
using FlowerShop.Models;
using FlowerShop.ViewModels;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace FlowerShop.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(CartListWithGrandTotal request)
        {
            
            MimeMessage email = new();
            email.From.Add(MailboxAddress.Parse("fredy.rohan@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("fredy.rohan@ethereal.email"));
            email.Subject = "Order confirmation";
            email.Body = new TextPart(TextFormat.Html) { Text = $"Thank you for you purchase of {request.ShoppingCarts.Count} items, totaling {request.OrderTotal:C}" }; 

            using SmtpClient smtp = new();
            smtp.Connect("smtp.ethereal.email",587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("fredy.rohan@ethereal.email", "eSeGQvbpsmzXq1Jxfh");
            smtp.Send(email);
            smtp.Disconnect(true);

        }
    }
}
