using Boss_RЯ.Static_Classes;
using System;

namespace Boss_RЯ.Abstract_Classes
{
    public abstract class Person : UniqueID
    {
        private string _name;
        private string _surname;
        private string _age;

        public Person(in string name, in string surname, in string age)
        {
            Name = name;
            Surname = surname;
            Age = age;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = Verify.IsDataContainsOnlyLetters(value) ? value : throw new InvalidOperationException("Name contains space and letters.");
            }
        }
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = Verify.IsDataContainsOnlyLetters(value) ? value : throw new InvalidOperationException("Surname contains space and letters.");
            }
        }
        public string Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = Verify.IsDataContainsOnlyIntegers(value) && Verify.IsPersonAgeMoreThanSeventeen(value) ? value : throw new InvalidOperationException("Age must contain numbers more than 17");
            }
        }

    }
}
