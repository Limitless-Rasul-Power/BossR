using Boss_RЯ.Static_Classes;
using System;

namespace Boss_RЯ.Abstract_Classes
{
    public abstract class AdditionalRequirement : UniqueID
    {
        private string _speciality;
        private string _city;

        public AdditionalRequirement(in string speciality, in string city)
        {
            Speciality = speciality;
            City = city;
        }

        public string Speciality
        {
            get
            {
                return _speciality;
            }
            set
            {
                _speciality = Verify.IsDataCorrectFormat(value) ? value : throw new InvalidOperationException("Speciality is not correct format");
            }
        }
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = Verify.IsDataContainsOnlyLetters(value) ? value : throw new InvalidOperationException("City contains space and letters");
            }
        }
    }
}
