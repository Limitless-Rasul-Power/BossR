using Boss_RЯ.Abstract_Classes;
using Boss_RЯ.Static_Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boss_RЯ.Entity_Classes
{
    public class Announcement : AdditionalRequirement
    {
        public readonly List<string> viewedWorkerUserNames = new List<string>();
        public readonly List<string> appliedWorkersCV = new List<string>();
        private string _payCheck;
        private string _experienceYear;
        private string _description;
        public DateTime PublishTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now.AddMonths(1);

        public Announcement(in string speciality, in string city, in string payCheck, in string experienceYear, in string description)
            : base(speciality, city)
        {
            PayCheck = payCheck;
            ExperienceYear = experienceYear;
            Description = description;
        }

        public void IncreaseView(in string userName)
        {
            if ((!string.IsNullOrWhiteSpace(userName)))
            {
                if (!viewedWorkerUserNames.Contains(userName))
                    viewedWorkerUserNames.Add(userName);
            }
        }

        private void CommonPrintWords()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Announcement View Count: {viewedWorkerUserNames.Count}");
            Console.ResetColor();
            Console.Write($"Announcement ID: {ID}, Speciality: {Speciality}, ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"PayCheck: {int.Parse(PayCheck):C} $");
            Console.ResetColor();
        }
        public void ShowLess()
        {
            CommonPrintWords();
            Console.WriteLine("===============================\n");
        }

        public void ShowMore()
        {
            CommonPrintWords();
            Console.WriteLine($"City: {City}\nExperience Year: { ExperienceYear}\n\nDescription: { Description}\nPublish Time: {PublishTime.ToLongDateString()}\nEnd Time: {EndTime.ToLongDateString()}");
            Console.WriteLine("===============================\n");
        }

        public string PayCheck
        {
            get
            {
                return _payCheck;
            }
            set
            {
                _payCheck = Verify.IsNumberPositiveInteger(value) ? value : throw new InvalidOperationException("Pay check must contain only positive numbers");
            }
        }
        public string ExperienceYear
        {
            get
            {
                return _experienceYear;
            }
            set
            {
                _experienceYear = Verify.IsDataContainsOnlyIntegers(value) || double.TryParse(value, out _) ? value : throw new InvalidOperationException("Experience year must be integer");
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = !Verify.IsDataContainsOnlyIntegers(value) ? value : throw new InvalidOperationException("Description must contain letters too.");
            }
        }

        public override string ToString()
        {
            return
$@"ID: {ID}
Speciality: {Speciality}
Pay Check: {PayCheck} $
City: {City}
Description: {Description}
";
        }

    }
}
