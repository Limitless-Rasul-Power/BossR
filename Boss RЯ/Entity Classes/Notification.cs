using System;

namespace Boss_RЯ.Entity_Classes
{
    public class Notification
    {
        private readonly DateTime date = DateTime.Now;
        public string Message { get; set; }

        public Notification(string message)
        {
            Message = message;
        }

        public void Show()
        {
            Console.Write($"Message\n{Message}\nSend Date: {date:F}\n================\n\n");
        }

    }

}
