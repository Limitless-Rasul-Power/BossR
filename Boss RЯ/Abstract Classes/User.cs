using Boss_RЯ.Static_Classes;
using System;

namespace Boss_RЯ.Abstract_Classes
{
    public abstract class User : Person
    {
        private string _city;
        private string _phone;
        private string _userName;
        private string _password;

        public User(in string userName, in string password, in string name, in string surname, in string age, in string city, in string phone)
            : base(name, surname, age)
        {
            UserName = userName;
            Password = password;
            City = city;
            Phone = phone;
        }

        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = Verify.IsPhoneNumberCorrectFormat(value) ? value : throw new InvalidOperationException("Phone is not correct format");
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
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {

                _password = Verify.IsPasswordStrong(value) ? value : throw new InvalidOperationException("Password is not strong");
            }
        }
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = Verify.IsDataCorrectFormat(value) ? value : throw new InvalidOperationException("User name is not correct format");
            }
        }

    }
}
