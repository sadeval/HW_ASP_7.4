using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

public class FeedbackController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(string subject, string message, string email)
    {
        if (string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message) || string.IsNullOrEmpty(email))
        {
            ViewBag.Error = "Все поля обязательны для заполнения.";
            return View();
        }

        try
        {
            // Настройка SMTP-клиента
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential("sadeval2011@gmail.com", "your_password"),
                EnableSsl = true
            };

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(email),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mail.To.Add("sadeval2011@gmail.com");

            smtpClient.Send(mail);
            ViewBag.Success = "Сообщение успешно отправлено.";
        }
        catch (Exception ex)
        {
            ViewBag.Error = "Ошибка при отправке сообщения: " + ex.Message;
        }

        return View();
    }
}
