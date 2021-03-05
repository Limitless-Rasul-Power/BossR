using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace Boss_RЯ.Helper_Static_Classes
{

    namespace Network
    {
        public static class MailHelper
        {
            public static readonly string MyEmail = "r.osmanl@mail.ru";
            public const int SMTPPort = 587;
            public const string host = "smtp.mail.ru";
            public static void SendEmail(in string to, in string body)
            {
                try
                {
                    string header = $"<h1 style = \"color:green;\"> Passion.Love Header </h1>";
                    MailMessage message = new MailMessage(MyEmail, to, "Passion.Love Coo inc ©", $"{header}{body}")
                    {
                        IsBodyHtml = true
                    };

                    SmtpClient smtpClient = new SmtpClient($"{host}", SMTPPort)
                    {
                        UseDefaultCredentials = true,
                        Credentials = new NetworkCredential("YourRealEmail", "YourRealPassword"),
                        EnableSsl = true
                    };
                    smtpClient.Send(message);
                }
                catch (Exception caption)
                {
                    Console.Clear();
                    MessageBox.Show(caption.Message, "Passion.Love Coo inc ©", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }

}
