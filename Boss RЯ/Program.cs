using System;
using System.IO;
using System.Windows.Forms;

using Boss_RЯ.Abstract_Classes;
using Boss_RЯ.Entity_Classes;
using Boss_RЯ.Helper_Static_Classes;
using Boss_RЯ.Static_class_about_Employer_Menu_Choices;
using Boss_RЯ.Static_class_about_Worker_Menu_Choices;
using Boss_RЯ.Static_Classes;
using Boss_RЯ.Static_Classes_about_Common_Menu_Choices;


namespace Boss_RЯ
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.Title = "Passion.Love";
            ConsoleConfiguration.DisableXButton();
            Console.CursorVisible = false;

            Database database = null;
            if (File.Exists(JsonFileHelper.fileName))
            {
                JsonFileHelper.JSONDeSerialization(ref database);
                UniqueID.id = database.DatabaseCurrentID;
            }
            else
                database = new Database(Configuration.GetWorkersAndSetCVs(), Configuration.GetEmployersAndSetAnnouncements());

            string[] bossFirstMenu = Configuration.GetBossFirstMenu();
            string[] userHeadMenu = Configuration.GetUserHeadMenu();

            string[] employerSecondMenu = Configuration.GetEmployerSecondMenu();
            string[] userSecondMenu = Configuration.GetUserSecondMenu();

            string userName = null, password = null, choice = null;

            while (true)
            {
                Console.Clear();
                Configuration.PrintEntryWord();
                Configuration.PrintMenu(bossFirstMenu);
                Console.Write("Enter choice: ");
                choice = Console.ReadLine();

                while (!Verify.IsChoiceCorrect(choice, bossFirstMenu.Length))
                {
                    Console.WriteLine("\nEnter one of this choices: ");
                    choice = Console.ReadLine();
                }
                Console.Clear();

                if (choice == BossFirstMenuChoices.Exit)
                {
                    MessageBox.Show("Thank you visiting", "Passion.Love", MessageBoxButtons.OK);
                    JsonFileHelper.JSONSerialization(database);
                    break;
                }

                if (choice == BossFirstMenuChoices.Employer)
                {
                    while (true)
                    {
                        bool isConfirmedEmployer = false;
                        Employer employer = null;

                        Console.Clear();
                        Configuration.PrintMenu(userHeadMenu);
                        Console.Write("Enter choice: ");
                        choice = Console.ReadLine();

                        while (!Verify.IsChoiceCorrect(choice, userHeadMenu.Length))
                        {
                            Console.WriteLine("\nEnter one of this choices: ");
                            choice = Console.ReadLine();
                        }
                        Console.Clear();

                        if (choice == UserHeadMenuChoices.Exit)
                            break;

                        Console.Write("Enter username: ");
                        userName = Console.ReadLine().Trim();

                        if (database.IsUserNameExist(userName) && choice == UserHeadMenuChoices.Register)
                        {
                            Console.WriteLine("\nUsername exists in database or username is empty or white space, Enter again: ");
                            userName = Console.ReadLine();
                        }

                        Console.Write("Enter password: ");
                        password = Console.ReadLine().Trim();

                        while ((!Verify.IsPasswordStrong(password)) && choice == UserHeadMenuChoices.Register)
                        {
                            Console.WriteLine("\nPassword must contain at least 8 characters");
                            password = Console.ReadLine().Trim();
                        }
                        password = password.GetHashCode().ToString();
                        Console.Clear();

                        if (choice == UserHeadMenuChoices.Register)
                        {
                            Console.Write("Enter name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter surname: ");
                            string surname = Console.ReadLine();
                            Console.Write("Enter age: ");
                            string age = Console.ReadLine();

                            while (!Verify.IsPersonAgeMoreThanSeventeen(age))
                            {
                                Console.WriteLine("Age must be more than 17, enter: ");
                                age = Console.ReadLine();
                            }

                            Console.Write("Enter city: ");
                            string city = Console.ReadLine();

                            Console.Write("Enter phone number: ");
                            string phone = Console.ReadLine();

                            while (!Verify.IsPhoneNumberCorrectFormat(phone))
                            {
                                Console.WriteLine("\nPhone number is not correct format: ");
                                phone = Console.ReadLine();
                            }

                            try
                            {
                                employer = new Employer(userName, password, name, surname, age, city, phone);
                                database.Employers.Add(employer);
                                isConfirmedEmployer = true;
                            }
                            catch (Exception caption)
                            {
                                Console.Clear();
                                MessageBox.Show(caption.Message, "Passion.Love", MessageBoxButtons.OK);
                                StreamFileWriteHelper.WriteLogErrorFile($"{caption.Message} => Employer Side");
                            }
                        }
                        else if (choice == UserHeadMenuChoices.Login)
                        {
                            employer = database.Employers.Find(e => e.UserName == userName && e.Password == password);
                            if (employer != null)
                                isConfirmedEmployer = true;
                        }
                        else
                        {
                            MessageBox.Show("See you next time goodbye.", "Passion.Love", MessageBoxButtons.OK);
                            break;
                        }

                        Console.Clear();

                        if (!isConfirmedEmployer)
                            MessageBox.Show("Wrong Credentials", "Passion.Love", MessageBoxButtons.OK);
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"Welcome {employer.Name} {employer.Surname} to Passion.Love");

                            while (true)
                            {
                                Configuration.PrintMenu(employerSecondMenu);
                                Console.Write("Enter choice: ");
                                choice = Console.ReadLine();

                                while (!Verify.IsChoiceCorrect(choice, employerSecondMenu.Length))
                                {
                                    Console.WriteLine("\nEnter one of this choices: ");
                                    choice = Console.ReadLine();
                                }
                                Console.Clear();

                                if (choice == EmployerSecondMenuChocies.AnnouncementSide)
                                {
                                    EmployerMenus.EmployerAboutAnnouncementSideMenu(database.Workers, employer);
                                }
                                else if (choice == EmployerSecondMenuChocies.WorkerSide)
                                {
                                    EmployerMenus.EmployerAboutWorkerSideMenu(database.Workers, employer);
                                }
                                else
                                {
                                    MessageBox.Show($"Good Luck {employer.Name} {employer.Surname}", "Passion.Love", MessageBoxButtons.OK);
                                    isConfirmedEmployer = false;
                                    break;
                                }
                                Console.Clear();
                            }
                            if (choice == EmployerSecondMenuChocies.Exit)
                                break;
                        }

                    }
                }
                else if (choice == BossFirstMenuChoices.Worker)
                {
                    while (true)
                    {
                        bool isConfirmedUser = false;
                        Worker worker = null;

                        Console.Clear();
                        Configuration.PrintMenu(userHeadMenu);
                        Console.Write("Enter choice: ");
                        choice = Console.ReadLine();

                        while (!Verify.IsChoiceCorrect(choice, userHeadMenu.Length))
                        {
                            Console.WriteLine("\nEnter one of this choices: ");
                            choice = Console.ReadLine();
                        }
                        Console.Clear();

                        if (choice == UserHeadMenuChoices.Exit)
                            break;

                        Console.Write("Enter username: ");
                        userName = Console.ReadLine().Trim();

                        if (database.IsUserNameExist(userName) && choice == UserHeadMenuChoices.Register)
                        {
                            Console.WriteLine("\nUsername exists in database or username is empty or white space, Enter again: ");
                            userName = Console.ReadLine();
                        }

                        Console.Write("Enter password: ");
                        password = Console.ReadLine().Trim();

                        while ((!Verify.IsPasswordStrong(password)) && choice == UserHeadMenuChoices.Register)
                        {
                            Console.WriteLine("\nPassword must contain at least 8 characters");
                            password = Console.ReadLine().Trim();
                        }
                        password = password.GetHashCode().ToString();
                        Console.Clear();

                        if (choice == UserHeadMenuChoices.Register)
                        {
                            Console.Write("Enter name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter surname: ");
                            string surname = Console.ReadLine();
                            Console.Write("Enter age: ");
                            string age = Console.ReadLine();

                            while (!Verify.IsPersonAgeMoreThanSeventeen(age))
                            {
                                Console.WriteLine("Age must be more than 17, enter: ");
                                age = Console.ReadLine();
                            }

                            Console.Write("Enter city: ");
                            string city = Console.ReadLine();

                            Console.Write("Enter phone number: ");
                            string phone = Console.ReadLine();

                            while (!Verify.IsPhoneNumberCorrectFormat(phone))
                            {
                                Console.WriteLine("\nPhone number is not correct format: ");
                                phone = Console.ReadLine();
                            }

                            try
                            {
                                worker = new Worker(userName, password, name, surname, age, city, phone);
                                database.Workers.Add(worker);
                                isConfirmedUser = true;
                            }
                            catch (Exception caption)
                            {
                                Console.Clear();
                                MessageBox.Show(caption.Message, "Passion.Love", MessageBoxButtons.OK);
                                StreamFileWriteHelper.WriteLogErrorFile($"{caption.Message} => Worker Side");
                            }
                        }
                        else if (choice == UserHeadMenuChoices.Login)
                        {
                            worker = database.Workers.Find(w => w.UserName == userName && w.Password == password);
                            if (worker != null)
                                isConfirmedUser = true;
                        }
                        else
                        {
                            MessageBox.Show("See you next time goodbye.", "Passion.Love", MessageBoxButtons.OK);
                            break;
                        }

                        Console.Clear();

                        if (!isConfirmedUser)
                            MessageBox.Show("Wrong Credentials", "Passion.Love", MessageBoxButtons.OK);
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"Welcome {worker.Name} {worker.Surname} to Passion.Love");
                            while (true)
                            {
                                Configuration.PrintMenu(userSecondMenu);
                                Console.Write("Enter choice: ");
                                choice = Console.ReadLine();

                                while (!Verify.IsChoiceCorrect(choice, userSecondMenu.Length))
                                {
                                    Console.WriteLine("\nEnter one of this choices: ");
                                    choice = Console.ReadLine();
                                }
                                Console.Clear();

                                if (choice == WorkerSecondMenuChoices.Announcements)
                                {
                                    WorkerMenus.WorkerAnnouncementSideMenu(worker, database.Employers);
                                }
                                else if (choice == WorkerSecondMenuChoices.CV)
                                {
                                    WorkerMenus.WorkerCVSideMenu(worker, database.Employers);
                                }
                                else
                                {
                                    MessageBox.Show($"Good Luck {worker.Name} {worker.Surname}", "Passion.Love", MessageBoxButtons.OK);
                                    isConfirmedUser = false;
                                    break;
                                }
                                Console.Clear();
                            }

                            if (choice == WorkerSecondMenuChoices.Exit)
                                break;
                        }


                    }
                }
                else
                {
                    Configuration.PrintMission();
                }

            }

        }

    }
}
