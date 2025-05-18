using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MultiShop.WebUI.Models;

namespace MultiShop.WebUI.Controllers
{
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMail(MailRequest mailRequest)
        {
            MimeMessage mimeMessage = new MimeMessage();

            //burada mesajın kimden gönderildiği
            MailboxAddress mailboxAddressFrom = new MailboxAddress("MultiShop Katalog", "utkckr504@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            //Burada mesajın kime gönderildiği
            MailboxAddress mailboxAdressTo = new MailboxAddress("User", mailRequest.RecieverMail);
            mimeMessage.To.Add(mailboxAdressTo);

            //Burada mesajın içeriği
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailRequest.MessageContent;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            //burada mesajın konusu alındı
            mimeMessage.Subject = mailRequest.Subject;

            //SMTP protokolü kullanarak mail atmaya izin verme
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("utkckr504@gmail.com", "uombuxvotnmwpgix");
            client.Send(mimeMessage);
            client.Disconnect(true);
            return View();
        }
    }
}
