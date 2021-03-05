using Boss_RЯ.Abstract_Classes;
using Boss_RЯ.Static_Classes;
using System;

namespace Boss_RЯ.Entity_Classes
{
    public class CV : AdditionalRequirement
    {
        private string _school;
        private string _acceptScore;
        private string _skills;
        private string _workedCompanies;
        private string _foreignLanguage;
        public bool _IsHadHonorDiploma = false;
        private string _gitLink;
        private string _linkedin;
        private DateTime _endWorkTime;
        public DateTime StartWorkTime { get; set; }


        public CV(in string speciality, in string city, in string school, in string linkedin, in string gitLink, in string foreignLanguage,
            in string workedCompanies, in string skills, in string acceptScore, in DateTime started, in DateTime ended)
            : base(speciality, city)
        {
            School = school;
            Linkedin = linkedin;
            GitLink = gitLink;
            ForeignLanguage = foreignLanguage;
            WorkedCompanies = workedCompanies;
            Skills = skills;
            AcceptScore = acceptScore;
            StartWorkTime = started;
            EndWorkTime = ended;
        }


        public string School
        {
            get
            {
                return _school;
            }
            set
            {
                _school = (!string.IsNullOrWhiteSpace(value)) ? value : throw new InvalidOperationException("School is null.");
            }
        }
        public string Linkedin
        {
            get
            {
                return _linkedin;
            }
            set
            {
                _linkedin = Verify.IsDataCorrectFormat(value) && (!Verify.IsDataContainsOnlyIntegers(value)) ? value : throw new InvalidOperationException("Linkedin is not correct format.");
            }
        }
        public string GitLink
        {
            get
            {
                return _gitLink;
            }
            set
            {
                _gitLink = (!Verify.IsDataContainsOnlyIntegers(value)) ? value : throw new InvalidOperationException("Git Link doesn't contain only numbers");
            }
        }
        public string ForeignLanguage
        {
            get
            {
                return _foreignLanguage;
            }
            set
            {
                _foreignLanguage = !Verify.IsDataContainsOnlyIntegers(value) ? value : throw new InvalidOperationException("Language doesn't contain only integers.");
            }
        }
        public string WorkedCompanies
        {
            get
            {
                return _workedCompanies;
            }
            set
            {
                _workedCompanies = Verify.IsDataCorrectFormat(value) ? value : throw new InvalidOperationException("Data is not correct format");
            }
        }
        public string Skills
        {
            get
            {
                return _skills;
            }
            set
            {
                _skills = (!Verify.IsDataContainsOnlyIntegers(value)) ? value : throw new InvalidOperationException("Data contains only integers");
            }
        }
        public string AcceptScore
        {
            get
            {
                return _acceptScore;
            }
            set
            {
                _acceptScore = Verify.IsDataContainsOnlyIntegers(value) && Verify.IsNumberPositiveInteger(value) ? value : throw new InvalidOperationException("Acceptance score must be number");
            }
        }
        public DateTime EndWorkTime
        {
            get
            {
                return _endWorkTime;
            }
            set
            {
                _endWorkTime = Verify.IsEndTimeBiggerThanOrEqualFromStartTime(StartWorkTime, value) ? value : throw new InvalidOperationException("End time must be bigger or equal from start time");
            }
        }

        public override string ToString()
        {
            return
$@"ID: {ID}
Speciality: {Speciality}
City: {City}
School: {School}
Linkedin: {Linkedin}
GitLink: {GitLink}
About Foreign Language: {ForeignLanguage}
Worked Companies: {WorkedCompanies}
Skills: {Skills}
University Accept Score: {AcceptScore}
From Started Work Time {StartWorkTime.ToLongDateString()}  till  {EndWorkTime.ToLongDateString()}
";
        }
        public void ShowMore()
        {
            Console.WriteLine($"ID: {ID}\nSpeciality: {Speciality}\nCity: {City}\nSchool: {School}\nLinkedin: {Linkedin}\nGitLink: {GitLink}\nAbout Foreign Language: {ForeignLanguage}");
            Console.WriteLine($"Worked Companies: {WorkedCompanies}\nSkills: {Skills}\nUniversity Accept Score: {AcceptScore}");
            Console.WriteLine($"From Started Work Time {StartWorkTime.ToLongDateString()}  till  {EndWorkTime.ToLongDateString()}");
            Console.WriteLine("====================================================\n");
        }

        public void ShowLess()
        {
            Console.WriteLine($"ID: {ID}, Speciality: {Speciality}, City: {City}, School: {School}");
        }

    }

}
