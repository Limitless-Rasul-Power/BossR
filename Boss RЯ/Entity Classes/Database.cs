using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boss_RЯ.Entity_Classes
{
    public class Database
    {
        public readonly List<Worker> Workers = new List<Worker>();
        public readonly List<Employer> Employers = new List<Employer>();

        public long GetCurrentID()
        {
            long max = 0;

            if(Workers.Count > 0)
            max = Workers[Workers.Count - 1].ID;

            foreach (var worker in Workers)
            {
                if(worker.CV.Count > 0)
                {
                    if (worker.CV[worker.CV.Count - 1].ID > max)
                        max = worker.CV[worker.CV.Count - 1].ID;
                }
                
            }

            if(Employers.Count > 0)
            {
                if (Employers[Employers.Count - 1].ID > max)
                    max = Employers[Employers.Count - 1].ID;
            }
            


            foreach (var employer in Employers)
            {
                if(employer.Announcements.Count > 1)
                {
                    if (employer.Announcements[employer.Announcements.Count - 1].ID > max)
                        max = employer.Announcements[employer.Announcements.Count - 1].ID;
                }                
            }


            return max;
        }


        public long DatabaseCurrentID { get; private set; }

        public bool IsUserNameExist(string userName)
        {
            return Workers.Find(w => w.UserName == userName) != null || Employers.Find(e => e.UserName == userName) != null || string.IsNullOrWhiteSpace(userName);
        }
        public Database(in List<Worker> workers, in List<Employer> employers)
        {
            Workers = workers;
            Employers = employers;
            DatabaseCurrentID = GetCurrentID();
        }
    }

}
