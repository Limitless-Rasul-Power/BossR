using Boss_RЯ.Abstract_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Boss_RЯ.Entity_Classes
{
    public class Worker : User
    {
        public readonly List<CV> CV = new List<CV>();
        public readonly List<Notification> aboutAnnouncement = new List<Notification>();
        public readonly List<Notification> JobOffers = new List<Notification>();
        public readonly List<long> acceptedAnnouncementID = new List<long>();
        public Worker(in string userName, in string password, in string name, in string surname, in string age, in string city, in string phone)
            : base(userName, password, name, surname, age, city, phone)
        {
        }

        public bool IsNotificationExist()
        {
            return aboutAnnouncement.Count > 0;
        }


        public void CommonForAnnouncementAndJobOffers(List<Notification> note)
        {
            for (int i = 0; i < note.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                note[i].Show();
                Console.WriteLine();
            }
        }
        public void ShowNotifications()
        {
            if (!IsNotificationExist())
                MessageBox.Show("There is no notification in your portfolio", "Passion.love", MessageBoxButtons.OK);
            else
            {
                CommonForAnnouncementAndJobOffers(aboutAnnouncement);
            }
        }

        public void ShowJobOffers()
        {
            if (JobOffers.Count == 0)
                MessageBox.Show("There is no job offers in your portfolio", "Passion.love", MessageBoxButtons.OK);
            else
            {
                CommonForAnnouncementAndJobOffers(JobOffers);
            }

        }

        public void RemoveJobOfferWithMessage(string message)
        {
            if (JobOffers.Count > 0)
            {
                for (int i = 0; i < JobOffers.Count; i++)
                {
                    if (JobOffers[i].Message == message)
                    {
                        JobOffers.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public void ChangeJobOfferWithMessage(string oldMessage, string newMessage)
        {
            if (JobOffers.Count > 0)
            {
                for (int i = 0; i < JobOffers.Count; i++)
                {
                    if (JobOffers[i].Message.Contains(oldMessage))
                    {
                        string[] message = JobOffers[i].Message.Split('\n');
                        JobOffers[i].Message = $"{newMessage}\n{message[message.Length - 1]}";
                    }
                }
            }
        }
        public void AddNotification(Notification note)
        {
            if (note != null)
            {
                aboutAnnouncement.Add(note);
            }
        }

        public void ShowMoreInfoAboutCVs()
        {
            if (IsCVExist())
            {
                for (int i = 0; i < CV.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) CV INFO\n");
                    CV[i].ShowMore();
                }
            }
            else
                Console.WriteLine("There is no CV in your portfolio");
        }

        public void ShowLessInfoAboutCVs()
        {
            if (IsCVExist())
            {
                for (int i = 0; i < CV.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) CV INFO\n");
                    CV[i].ShowLess();
                    Console.WriteLine();
                }
            }
            else
                Console.WriteLine("There is no CV in your portfolio");
        }

        public bool IsCVNotNull(in CV addedCv)
        {
            return addedCv != null;
        }

        public bool IsDeleteCVCorrect(int id)
        {
            if (id > 0)
            {
                CV removedCv = CV.Find(c => c.ID == id);
                if (removedCv == null)
                    throw new InvalidOperationException($"There is no ID {id} CV exists in your portfolio.");

                return true;
            }

            throw new InvalidOperationException("Id must be more than 0.");
        }

        public bool IsCVIDExist(int id)
        {
            return CV.Find(c => c.ID == id) != null;
        }
        public bool IsCVExist()
        {
            return CV.Count > 0;
        }

    }

}
