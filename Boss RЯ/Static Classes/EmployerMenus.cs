using Boss_RЯ.Entity_Classes;
using Boss_RЯ.Helper_Static_Classes;
using Boss_RЯ.Static_class_about_Employer_Menu_Choices;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Boss_RЯ.Static_Classes
{
    public static class EmployerAboutWorkerSideSendJobOfferChoiceFilterMenuChoices
    {
        public const string BySpeciality = "1";
        public const string ByForeignLanguage = "2";
        public const string ByExperienceYear = "3";
        public const string BySkill = "4";
        public const string ByCity = "5";
        public const string ByAcceptScore = "6";
        public const string Back = "7";
    }
    public static class EmployerMenus
    {
        public static void EmployerAboutWorkerSideSendJobOfferChoiceFilterMenu(List<Worker> workers, Employer employer)
        {
            string[] aboutWorkerSideSendJobOfferChoiceFilterMenuChoices = Configuration.GetWorkerSideSendJobOfferChoiceFilterMenuChoices();
            string id = null, filterInputHelper = null;

            while (true)
            {
                Configuration.PrintMenu(aboutWorkerSideSendJobOfferChoiceFilterMenuChoices);
                Console.Write("\nEnter choice: ");
                string choice = Console.ReadLine();

                while (!Verify.IsChoiceCorrect(choice, aboutWorkerSideSendJobOfferChoiceFilterMenuChoices.Length))
                {
                    Console.WriteLine("\nEnter one of this choices: ");
                    choice = Console.ReadLine();
                }
                Console.Clear();

                if (choice == EmployerAboutWorkerSideSendJobOfferChoiceFilterMenuChoices.Back)
                    break;

                switch (choice)
                {
                    case EmployerAboutWorkerSideSendJobOfferChoiceFilterMenuChoices.BySpeciality:
                        {
                            Console.Write("Enter SpecialityL: ");
                            filterInputHelper = Console.ReadLine();
                        }
                        break;

                    case EmployerAboutWorkerSideSendJobOfferChoiceFilterMenuChoices.ByForeignLanguage:
                        {
                            Console.Write("Enter Language: ");
                            filterInputHelper = Console.ReadLine();
                        }
                        break;

                    case EmployerAboutWorkerSideSendJobOfferChoiceFilterMenuChoices.ByExperienceYear:
                        {
                            Console.Write("Enter Minimum Experience Year: ");
                            filterInputHelper = Console.ReadLine();

                            while (!Verify.IsNumberPositiveInteger(filterInputHelper))
                            {
                                Console.WriteLine("\nEnter positive integer: ");
                                filterInputHelper = Console.ReadLine();
                            }

                        }
                        break;

                    case EmployerAboutWorkerSideSendJobOfferChoiceFilterMenuChoices.BySkill:
                        {
                            Console.Write("Enter Skill: ");
                            filterInputHelper = Console.ReadLine();
                        }
                        break;

                    case EmployerAboutWorkerSideSendJobOfferChoiceFilterMenuChoices.ByCity:
                        {
                            Console.Write("Enter City: ");
                            filterInputHelper = Console.ReadLine();
                        }
                        break;

                    case EmployerAboutWorkerSideSendJobOfferChoiceFilterMenuChoices.ByAcceptScore:
                        {
                            Console.Write("Enter Minimum Accept Score: ");
                            filterInputHelper = Console.ReadLine();

                            while (!Verify.IsNumberPositiveInteger(filterInputHelper))
                            {
                                Console.WriteLine("\nEnter positive integer: ");
                                filterInputHelper = Console.ReadLine();
                            }

                        }
                        break;
                }
                Console.Clear();


                string normalRegexPattern = string.Format(@"\b{0}\b", Regex.Escape(filterInputHelper));
                string toUpperRegexPattern = string.Format(@"\b{0}\b", Regex.Escape(filterInputHelper.ToUpper()));
                string toLowerRegexPattern = string.Format(@"\b{0}\b", Regex.Escape(filterInputHelper.ToLower()));

                List<CV> compatibleWithFilterInputCVs = new List<CV>();


                foreach (var worker in workers)
                {
                    List<CV> cvFilterHelper = null;
                    if (choice == EmployerAboutWorkerSideSendJobOfferChoiceFilterMenuChoices.ByExperienceYear)
                    {
                        cvFilterHelper = worker.CV.FindAll(cv =>
                                                                    cv.EndWorkTime.Year - cv.StartWorkTime.Year >= int.Parse(filterInputHelper));
                    }

                    else if(choice == EmployerAboutWorkerSideSendJobOfferChoiceFilterMenuChoices.ByAcceptScore)
                    {
                        cvFilterHelper = worker.CV.FindAll(cv =>
                                                                    long.Parse(cv.AcceptScore) >= long.Parse(filterInputHelper));
                    }

                    else
                    {
                        cvFilterHelper = worker.CV.FindAll(cv => Regex.IsMatch(cv.ToString(), normalRegexPattern) ||
                                                                         Regex.IsMatch(cv.ToString(), toUpperRegexPattern) ||
                                                                         Regex.IsMatch(cv.ToString(), toLowerRegexPattern));
                    }

                    if (cvFilterHelper != null)
                        compatibleWithFilterInputCVs.AddRange(cvFilterHelper);
                }

                if (compatibleWithFilterInputCVs.Count == 0)
                    MessageBox.Show($"There is no compatible CV or CVs exist(s) for your search => \"{filterInputHelper}\"", "Passion.Love", MessageBoxButtons.OK);
                else
                {
                    Worker thisWorker = null;
                    while (thisWorker == null)
                    {
                        compatibleWithFilterInputCVs.ForEach(cv => Console.WriteLine($"{cv}\n"));

                        Console.Write("Which CV ID attracts you: ");
                        id = Console.ReadLine();

                        while (!Verify.IsNumberPositiveInteger(id))
                        {
                            Console.WriteLine("\nEnter one of this ID's: ");
                            id = Console.ReadLine();
                        }
                        Console.Clear();

                        foreach (var worker in workers)
                        {
                            if (worker.CV.Find(i => i.ID == long.Parse(id)) != null)
                            {
                                thisWorker = worker;
                                break;
                            }
                        }
                        Console.Clear();

                        if (thisWorker == null)
                            MessageBox.Show($"There is no ID {id} CV exist", "Passion.Love", MessageBoxButtons.OK);
                        else
                            break;
                    }


                    DialogResult resultFinal = MessageBox.Show("Do you want offer job to worker who contains this CV ?", "Passion.Love", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultFinal == DialogResult.Yes)
                    {
                        string cvID = id;

                        Announcement announcement = null;
                        while (announcement == null)
                        {
                            employer.ShowMoreAnnouncements();
                            Console.Write("Which ID work do you want to offer: ");
                            id = Console.ReadLine();

                            while (!Verify.IsNumberPositiveInteger(id))
                            {
                                Console.WriteLine("\nEnter one of this ID's: ");
                                id = Console.ReadLine();
                            }
                            Console.Clear();

                            announcement = employer.Announcements.Find(ann => ann.ID == long.Parse(id));

                            if (announcement == null)
                                MessageBox.Show($"There is no ID {id} announcement exist in your portfolio", "Passion.Love", MessageBoxButtons.OK);
                        }
                        Console.Clear();

                        if (thisWorker.JobOffers.Find(jo => jo.Message.Substring(0, jo.Message.IndexOf('\n') - 1) == $"ID: {long.Parse(id)}") != null
                            || thisWorker.acceptedAnnouncementID.Find(annID => annID == long.Parse(id)) != 0)
                            MessageBox.Show($"You offer this job to this CV before or this worker accepted this job before", "Passion.Love", MessageBoxButtons.OK);
                        else
                        {
                            thisWorker.JobOffers.Add(new Notification($"{announcement}\nTo Your CV ID: {cvID}"));
                            MessageBox.Show($"Successfully send to worker, work ID {announcement.ID}, Speciality: {announcement.Speciality}", "Passion.Love", MessageBoxButtons.OK);
                        }

                    }

                }

                Console.Clear();
            }

        }
        public static void EmployerAboutAnnouncementSideUpdateChoiceMenu(List<Worker> workers, Employer employer)
        {
            string[] aboutAnnouncementSideUpdateChoiceMenu = Configuration.GetEmployerAboutAnnouncementSideUpdateChoiceMenu();
            string id = null;

            while (true)
            {
                Console.Clear();
                employer.Announcements.ForEach(ann => ann.ShowMore());
                Console.Write("Which ID Announcement do you want to update: ");
                id = Console.ReadLine();

                while (!Verify.IsNumberPositiveInteger(id))
                {
                    Console.WriteLine("\nEnter positive integer: ");
                    id = Console.ReadLine();
                }
                Console.Clear();

                Announcement announcement = employer.Announcements.Find(ann => ann.ID == long.Parse(id));
                if (announcement == null)
                    MessageBox.Show($"There is no ID {id} announcement in your porfolio", "Passion.Love", MessageBoxButtons.OK);
                else
                {
                    while (true)
                    {
                        Console.Clear();
                        string announcementHelper = announcement.ToString();

                        announcement.ShowMore();
                        Configuration.PrintMenu(aboutAnnouncementSideUpdateChoiceMenu);
                        Console.Write("\nWhich option do you choose: ");
                        string choice = Console.ReadLine();

                        while (!Verify.IsChoiceCorrect(choice, aboutAnnouncementSideUpdateChoiceMenu.Length))
                        {
                            Console.WriteLine("\nEnter one of this choices: ");
                            choice = Console.ReadLine();
                        }
                        Console.Clear();

                        if (choice == EmployerAboutAnnouncementSideUpdateChoiceMenuChoices.Back)
                            break;

                        if (choice == EmployerAboutAnnouncementSideUpdateChoiceMenuChoices.Exit)
                            return;

                        switch (choice)
                        {
                            case EmployerAboutAnnouncementSideUpdateChoiceMenuChoices.Speciality:
                                {
                                    Console.Write("Enter speciality: ");
                                    string speciality = Console.ReadLine();

                                    DialogResult result = MessageBox.Show("Do you want to add this speciality to your announcement and don't delete other specialities ?", "Passion.Love", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    StringBuilder helper = new StringBuilder();

                                    try
                                    {
                                        helper.Append(", ")
                                              .Append(speciality);

                                        if (result == DialogResult.Yes)
                                            announcement.Speciality += helper.ToString();
                                        else
                                            announcement.Speciality = speciality;

                                        Console.Clear();
                                        MessageBox.Show("Speciailty updated successfully", "Passion.Love", MessageBoxButtons.OK);
                                    }
                                    catch (Exception caption)
                                    {
                                        Console.Clear();
                                        MessageBox.Show(caption.Message, "Passion.Love", MessageBoxButtons.OK);
                                        StreamFileWriteHelper.WriteLogErrorFile($"{caption.Message} => Employer: {employer.Name} {employer.Surname}");
                                    }
                                    Console.Clear();
                                }
                                break;

                            case EmployerAboutAnnouncementSideUpdateChoiceMenuChoices.City:
                                {
                                    Console.Write("Enter city: ");
                                    string city = Console.ReadLine();

                                    DialogResult result = MessageBox.Show("Do you want to add this city to your announcement and don't delete other cities ?", "Passion.Love", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    StringBuilder helper = new StringBuilder();

                                    try
                                    {
                                        helper.Append(", ")
                                              .Append(city);

                                        if (result == DialogResult.Yes)
                                            announcement.City += helper.ToString();
                                        else
                                            announcement.City = city;

                                        Console.Clear();
                                        MessageBox.Show("City updated successfully", "Passion.Love", MessageBoxButtons.OK);
                                    }
                                    catch (Exception caption)
                                    {
                                        Console.Clear();
                                        MessageBox.Show(caption.Message, "Passion.Love", MessageBoxButtons.OK);
                                        StreamFileWriteHelper.WriteLogErrorFile($"{caption.Message} => Employer: {employer.Name} {employer.Surname}");
                                    }
                                    Console.Clear();
                                }
                                break;

                            case EmployerAboutAnnouncementSideUpdateChoiceMenuChoices.PayCheck:
                                {
                                    Console.Write("Enter pay check: ");
                                    string payCheck = Console.ReadLine();
                                    try
                                    {
                                        announcement.PayCheck = payCheck;
                                        Console.Clear();
                                        MessageBox.Show("Pay check updated successfully", "Passion.Love", MessageBoxButtons.OK);
                                    }
                                    catch (Exception caption)
                                    {
                                        Console.Clear();
                                        MessageBox.Show(caption.Message, "Passion.Love", MessageBoxButtons.OK);
                                        StreamFileWriteHelper.WriteLogErrorFile($"{caption.Message} => Employer: {employer.Name} {employer.Surname}");
                                    }
                                    Console.Clear();
                                }
                                break;

                            case EmployerAboutAnnouncementSideUpdateChoiceMenuChoices.ExperienceYear:
                                {

                                    Console.Write("Enter experience year: ");
                                    string experienceYear = Console.ReadLine();

                                    try
                                    {
                                        announcement.ExperienceYear = experienceYear;
                                        Console.Clear();
                                        MessageBox.Show("Experience Year updated successfully", "Passion.Love", MessageBoxButtons.OK);
                                    }
                                    catch (Exception caption)
                                    {
                                        Console.Clear();
                                        MessageBox.Show(caption.Message, "Passion.Love", MessageBoxButtons.OK);
                                        StreamFileWriteHelper.WriteLogErrorFile($"{caption.Message} => Employer: {employer.Name} {employer.Surname}");
                                    }
                                    Console.Clear();
                                }
                                break;

                            case EmployerAboutAnnouncementSideUpdateChoiceMenuChoices.Description:
                                {
                                    Console.Write("Enter description: ");
                                    string description = Console.ReadLine();

                                    DialogResult result = MessageBox.Show("Do you want to add this description to your announcement and don't delete other descriptions ?", "Passion.Love", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    StringBuilder helper = new StringBuilder();

                                    try
                                    {
                                        helper.Append(", ")
                                              .Append(description);

                                        if (result == DialogResult.Yes)
                                            announcement.Description += helper.ToString();
                                        else
                                            announcement.Description = description;

                                        Console.Clear();
                                        MessageBox.Show("Description updated successfully", "Passion.Love", MessageBoxButtons.OK);
                                    }
                                    catch (Exception caption)
                                    {
                                        Console.Clear();
                                        MessageBox.Show(caption.Message, "Passion.Love", MessageBoxButtons.OK);
                                        StreamFileWriteHelper.WriteLogErrorFile($"{caption.Message} => Employer: {employer.Name} {employer.Surname}");
                                    }
                                    Console.Clear();
                                }
                                break;
                        }

                        foreach (var worker in workers)
                        {
                            worker.ChangeJobOfferWithMessage(announcementHelper, announcement.ToString());
                        }

                    }


                }

            }

        }
        public static void EmployerAboutAnnouncementSideMenu(List<Worker> workers, Employer employer)
        {
            string[] aboutAnnouncementSideMenu = Configuration.GetEmployerAboutAnnouncementSideMenu();
            string id = null;

            while (true)
            {
                Console.Clear();
                Configuration.PrintMenu(aboutAnnouncementSideMenu);
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();


                while (!Verify.IsChoiceCorrect(choice, aboutAnnouncementSideMenu.Length))
                {
                    Console.WriteLine("\nEnter one of this choices: ");
                    choice = Console.ReadLine();
                }
                Console.Clear();

                if (choice == EmployerAboutAnnouncementSideMenuChoices.Back)
                {
                    MessageBox.Show("See you soon", "Passion.Love", MessageBoxButtons.OK);
                    break;
                }

                switch (choice)
                {
                    case EmployerAboutAnnouncementSideMenuChoices.Add:
                        {
                            Console.Write("Enter speciality: ");
                            string speciality = Console.ReadLine();
                            Console.Write("Enter city: ");
                            string city = Console.ReadLine();
                            Console.Write("Enter pay check: ");
                            string payCheck = Console.ReadLine();
                            Console.Write("Enter experience year: ");
                            string experienceYear = Console.ReadLine();
                            Console.Write("Enter description: ");
                            string description = Console.ReadLine();

                            try
                            {
                                Announcement announcement = new Announcement(speciality, city, payCheck, experienceYear, description);
                                employer.Announcements.Add(announcement);
                                MessageBox.Show("Announcement Added Successfully", "Passion.Love", MessageBoxButtons.OK);
                            }
                            catch (Exception caption)
                            {
                                Console.Clear();
                                MessageBox.Show(caption.Message, "Passion.Love", MessageBoxButtons.OK);
                                StreamFileWriteHelper.WriteLogErrorFile($"{caption.Message} => Employer: {employer.Name} {employer.Surname}");
                            }


                        }
                        break;

                    case EmployerAboutAnnouncementSideMenuChoices.DeleteWithID:
                        {
                            if (employer.Announcements.Count == 0)
                                MessageBox.Show("There is no announcement in your porfolio", "Passion.Love", MessageBoxButtons.OK);
                            else
                            {
                                employer.Announcements.ForEach(ann => ann.ShowLess());
                                Console.Write("Which Announcement ID do you want to delete: ");
                                id = Console.ReadLine();

                                while (!Verify.IsNumberPositiveInteger(id))
                                {
                                    Console.WriteLine("\nEnter positive integer: ");
                                    id = Console.ReadLine();
                                }
                                Console.Clear();

                                Announcement announcement = employer.Announcements.Find(ann => ann.ID == long.Parse(id));

                                if (announcement == null)
                                    MessageBox.Show($"There is no ID {id} announcement exist in your portfolio", "Passion.Love", MessageBoxButtons.OK);
                                else
                                {
                                    DialogResult result = MessageBox.Show($"Do you want to delete ID {id} announcement in your portfolio ?", "Passion.Love", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (result == DialogResult.Yes)
                                    {
                                        foreach (var worker in workers)
                                        {
                                            Notification jobOffer = worker.JobOffers
                                                .Find(jo => jo.Message.Substring(0, jo.Message.IndexOf('\n') - 1) == $"ID: {long.Parse(id)}");

                                            if (jobOffer != null)
                                                worker.JobOffers.Remove(jobOffer);
                                        }

                                        foreach (var note in announcement.appliedWorkersCV)
                                        {
                                            foreach (var worker in workers)
                                            {
                                                if (worker.CV.Find(cv => $"ID: {cv.ID}" == note.Substring(0, note.IndexOf('\n') - 1)) != null)
                                                {
                                                    worker.AddNotification(new Notification($"Your applied announcement ID {announcement.ID}, Speciality: {announcement.Speciality} deleted by employer {employer.Name} {employer.Surname}."));
                                                }
                                            }
                                        }

                                        employer.Announcements.Remove(announcement);
                                        MessageBox.Show($"Announcement ID {id} deleted successfully", "Passion.Love", MessageBoxButtons.OK);
                                    }

                                }
                            }


                        }
                        break;

                    case EmployerAboutAnnouncementSideMenuChoices.UpdateWithID:
                        {
                            if (employer.Announcements.Count == 0)
                                MessageBox.Show("There is no Announcement in your portfolio", "Passion.love", MessageBoxButtons.OK);
                            else
                                EmployerAboutAnnouncementSideUpdateChoiceMenu(workers, employer);
                        }
                        break;
                }

            }

        }
        public static void EmployerAboutWorkerSideMenu(List<Worker> workers, Employer employer)
        {
            string[] aboutWorkerSideMenu = Configuration.GetEmployerAboutWorkerSideMenu();
            string id = null;

            while (true)
            {
                Console.Clear();
                Configuration.PrintMenu(aboutWorkerSideMenu);
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();


                while (!Verify.IsChoiceCorrect(choice, aboutWorkerSideMenu.Length))
                {
                    Console.WriteLine("\nEnter one of this choices: ");
                    choice = Console.ReadLine();
                }
                Console.Clear();

                if (choice == EmployerAboutWorkerSideMenuChoices.Back)
                {
                    MessageBox.Show("See you soon", "Passion.Love", MessageBoxButtons.OK);
                    break;
                }

                switch (choice)
                {
                    case EmployerAboutWorkerSideMenuChoices.ShowIncomingRequestsFromWorkers:
                        {
                            List<Announcement> appliedAnnouncements = employer.Announcements.FindAll(ann => ann.appliedWorkersCV.Count > 0);

                            if (employer.Announcements.Count == 0)
                                MessageBox.Show("There is no announcement in your porfolio", "Passion.Love", MessageBoxButtons.OK);
                            else if (appliedAnnouncements.Count == 0)
                                MessageBox.Show("There is no request from any worker to your announcements", "Passion.Love", MessageBoxButtons.OK);
                            else
                            {
                                appliedAnnouncements.ForEach(ann => ann.ShowMore());
                                Console.Write("Enter id to show requests: ");
                                id = Console.ReadLine();

                                while (!Verify.IsNumberPositiveInteger(id))
                                {
                                    Console.WriteLine("\nEnter positive integer: ");
                                    id = Console.ReadLine();
                                }
                                Console.Clear();


                                Announcement announcement = appliedAnnouncements.Find(ann => ann.ID == long.Parse(id));

                                if (announcement == null)
                                    MessageBox.Show($"There is no ID {id} announcement exist in your portofolio", "Passion.Love", MessageBoxButtons.OK);
                                else
                                {
                                    long announcementID = long.Parse(id);
                                    announcement.appliedWorkersCV.ForEach(app => Console.WriteLine($"{app}\n"));
                                    Console.Write("Enter which CV do you choose with ID: ");
                                    id = Console.ReadLine();

                                    while (!Verify.IsNumberPositiveInteger(id))
                                    {
                                        Console.WriteLine("\nEnter positive integer: ");
                                        id = Console.ReadLine();
                                    }
                                    Console.Clear();

                                    string apply = announcement.appliedWorkersCV.Find(app => app.Substring(0, app.IndexOf('\n') - 1) == $"ID: {long.Parse(id)}");

                                    if (apply == null)
                                        MessageBox.Show($"There is no ID {id} apply exist in your announcement", "Passion.Love", MessageBoxButtons.OK);
                                    else
                                    {
                                        DialogResult result = MessageBox.Show("Do you want to Accept worker (who contains this CV ) to your work ?", "Passion.Love", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                                        foreach (var worker in workers)
                                        {
                                            CV cv = worker.CV.Find(c => c.ID == long.Parse(id));
                                            if (cv != null)
                                            {
                                                if (result == DialogResult.Cancel)
                                                    worker.AddNotification(new Notification($"Employer saw your CV to announcement ID {announcement.ID}, Speciality: {announcement.Speciality} but didn't do any operations."));
                                                else if (result == DialogResult.Yes)
                                                {
                                                    worker.AddNotification(new Notification($"Employer Accepted your CV to announcement ID {announcement.ID}, Speciality: {announcement.Speciality}."));
                                                    worker.acceptedAnnouncementID.Add(announcementID);
                                                }
                                                else
                                                    worker.AddNotification(new Notification($"Employer Rejected your CV to announcement ID {announcement.ID}, Speciality: {announcement.Speciality}."));

                                                if (result != DialogResult.Cancel)
                                                    announcement.appliedWorkersCV.Remove(cv.ToString());

                                                break;
                                            }
                                        }

                                        MessageBox.Show($"Operation {result} done successfully", "Passion.Love", MessageBoxButtons.OK);

                                    }

                                }


                            }

                        }
                        break;

                    case EmployerAboutWorkerSideMenuChoices.SendJobOfferToWorkersWhoAreInTheSystem:
                        {
                            bool isAnyCvExistInTheSystem = false;
                            foreach (var worker in workers)
                            {
                                if (worker.CV.Count > 0)
                                    isAnyCvExistInTheSystem = true;
                            }

                            if (employer.Announcements.Count == 0)
                                MessageBox.Show("There is no announcement in your porfolio", "Passion.Love", MessageBoxButtons.OK);
                            else if (!isAnyCvExistInTheSystem)
                                MessageBox.Show("There isn't any CV in the system right now", "Passion.Love", MessageBoxButtons.OK);
                            else
                            {
                                DialogResult result = MessageBox.Show("Do you want to filter CVs ?", "Passion.Love", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (result == DialogResult.Yes)
                                {
                                    EmployerAboutWorkerSideSendJobOfferChoiceFilterMenu(workers, employer);
                                }
                                else
                                {
                                    foreach (var worker in workers)
                                    {
                                        worker.CV.ForEach(cv => Console.WriteLine($"{cv}\n"));
                                    }

                                    Console.Write("Which ID CV attracts you: ");
                                    id = Console.ReadLine();

                                    while (!Verify.IsNumberPositiveInteger(id))
                                    {
                                        Console.WriteLine("\nEnter one of this ID's: ");
                                        id = Console.ReadLine();
                                    }
                                    Console.Clear();

                                    Worker thisWorker = null;
                                    foreach (var worker in workers)
                                    {
                                        if (worker.CV.Find(i => i.ID == long.Parse(id)) != null)
                                        {
                                            thisWorker = worker;
                                            break;
                                        }
                                    }
                                    Console.Clear();

                                    if (thisWorker == null)
                                        MessageBox.Show($"There is no ID {id} CV exist", "Passion.Love", MessageBoxButtons.OK);
                                    else
                                    {
                                        DialogResult resultFinal = MessageBox.Show("Do you want offer job to worker who contains this CV ?", "Passion.Love", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                        if (resultFinal == DialogResult.Yes)
                                        {
                                            string cvID = id;

                                            employer.ShowMoreAnnouncements();
                                            Console.Write("Which ID work do you want to offer: ");
                                            id = Console.ReadLine();

                                            while (!Verify.IsNumberPositiveInteger(id))
                                            {
                                                Console.WriteLine("\nEnter one of this ID's: ");
                                                id = Console.ReadLine();
                                            }
                                            Console.Clear();

                                            if (thisWorker.JobOffers.Find(jo => jo.Message.Substring(0, jo.Message.IndexOf('\n') - 1) == $"ID: {long.Parse(id)}") != null
                                                || thisWorker.acceptedAnnouncementID.Find(annID => annID == long.Parse(id)) != 0)
                                                MessageBox.Show($"You offer this job to this CV before or this worker accepted this job before", "Passion.Love", MessageBoxButtons.OK);
                                            else
                                            {
                                                Announcement announcement = employer.Announcements.Find(ann => ann.ID == long.Parse(id));

                                                if (announcement == null)
                                                    MessageBox.Show($"There is no ID {id} announcement exist in your portfolio", "Passion.Love", MessageBoxButtons.OK);
                                                else
                                                {
                                                    thisWorker.JobOffers.Add(new Notification($"{announcement}\nTo Your CV ID: {cvID}"));
                                                    MessageBox.Show($"Successfully send to worker, work ID {announcement.ID}, Speciality: {announcement.Speciality}", "Passion.Love", MessageBoxButtons.OK);
                                                }
                                            }

                                        }

                                    }


                                }


                            }

                        }
                        break;

                    case EmployerAboutWorkerSideMenuChoices.ShowNotificationsAboutJobOffers:
                        {
                            if (employer.notesFromJobOffers.Count == 0)
                                MessageBox.Show("There is no notification respond to job offers.", "Passion.Love", MessageBoxButtons.OK);
                            else
                            {
                                employer.notesFromJobOffers.ForEach(n => n.Show());
                                MessageBox.Show("All done.", "Passion.Love", MessageBoxButtons.OK);

                            }
                        }
                        break;

                }

            }

        }
    }
}
