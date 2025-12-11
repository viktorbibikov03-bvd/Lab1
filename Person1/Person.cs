using System;
using System.Text.RegularExpressions;

namespace Model
{
    using System.Xml.Linq;

    /// <summary>
    /// Информация о человеке
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Имя человека
        /// </summary>
        private string _name;

        /// <summary>
        /// Фамилия человека
        /// </summary>
        private string _surname;

        /// <summary>
        /// Возраст человека
        /// </summary>
        private int _age;

        /// <summary>
        /// Пол человека
        /// </summary>
        private Gender _gender;

        //TODO: naming
        /// <summary>
        /// Флаг, что у экземпляра задано либо имя либо фаилия
        /// </summary>
        private bool _flagname = false;

        /// <summary>
        /// Cоздает экземпляр <see cref="Person">
        /// </summary>
        public Person() { }

        /// <summary>
        /// Метод для обращения к private полям
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <exception cref="Exception">Исключение</exception>
        public Person(string name, string surname, int age, Gender gender) 
        {
            Name = name;
            Surname = surname;
            Age = age;
            Gender = gender;
        }

        /// <summary>
        /// Регулярные выражения для проверки имени, фамиии и возраста
        /// </summary>
        /// //TODO: RSDN
        private Regex CheckRussian = new Regex(@"^[А-Яа-яёЁ]+(\-[А-Яа-яёЁ]+)?$");
        //TODO: XML
        //TODO: RSDN
        private Regex CheckEnglish = new Regex(@"^[A-Za-z]+(\-[A-Za-z]+)?$");
        //TODO: XML
        //TODO: RSDN
        private Regex CheckDigital = new Regex(@"^[0-9]+$");

        /// <summary>
        /// Проверка имени и фамилии на корректность
        /// </summary>
        /// <param name="nameOrSurname">Имя или фамилия</param>
        /// <returns>true - Данные корректны, false - некорерктны</returns>
        private bool CheckNameAndSurname(string nameOrSurname)
        {
            return CheckRussian.IsMatch(nameOrSurname) || 
                CheckEnglish.IsMatch(nameOrSurname);
        }

        /// <summary>
        /// Проверка символов имени и фамилии на идентичность алфавита
        /// </summary>
        /// <param name="сheckName">Имя</param>
        /// <param name="сheckSurname">Фамилия</param>
        /// <returns></returns>
        private bool CheckLanguage(string сheckName, string сheckSurname)
        {
            return (CheckEnglish.IsMatch(сheckName) && 
                CheckEnglish.IsMatch(сheckSurname)) || 
                (CheckRussian.IsMatch(сheckName) && 
                CheckRussian.IsMatch(сheckSurname));
        }

        /// <summary>
        /// Проверка возраста на отсутствие символов
        /// </summary>
        /// <param name="AgeUser">Возраст</param>
        /// <returns>true - Данные корректны, false - некорерктны</returns>
        private bool CheckAge(string AgeUser)
        {
            return (CheckDigital.IsMatch(AgeUser));
        }

        /// <summary>
        /// Проверка корректности ввода имени
        /// </summary>
        public string Name
        { 
            get { return _name; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception($"Введите имя!");
                }

                if (!CheckNameAndSurname(value))
                {
                    throw new Exception("Имя должно содержать только русские" +
                        " или только английские буквы!");
                }

                if (_flagname && !CheckLanguage(value, _surname))
                {
                    throw new Exception("Имя и фамилия должны состоять " +
                        "из букв одного алфавита!");
                }

                _name = System.Globalization.CultureInfo.CurrentCulture.
                    TextInfo.ToTitleCase(value.ToLower());
                _flagname = true;
            }
        }

        /// <summary>
        /// Проверка корректности ввода фамилии
        /// </summary>
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception($"Введите фамилию!");
                }

                if (!CheckNameAndSurname(value))
                {
                    throw new Exception("Фамилия должна содержать только буквы!");
                }

                if (_flagname && !CheckLanguage(_name, value))
                {
                    throw new Exception("Имя и фамилия должны состоять " +
                        "из букв одного алфавита!");
                }

                _surname = System.Globalization.CultureInfo.CurrentCulture.
                    TextInfo.ToTitleCase(value.ToLower());
                _flagname= true;
            }
        }

        /// <summary>
        /// Минимальный возраст человека
        /// </summary>
        public const int MinAge = 0;
        
        /// <summary>
        /// Максимальный возраст человека
        /// </summary>
        public const int MaxAge = 123;
        
        /// <summary>
        /// Проверка корректности ввода возраста
        /// </summary>
        public int Age
        {
            get { return _age; }
            set
            {
                if (string.IsNullOrEmpty(Convert.ToString(value))) 
                {
                    throw new Exception("Введите возраст!");
                }

                if (!CheckAge(Convert.ToString(value)))
                {
                    throw new Exception("Возраст должен состоять из цифр!");
                }

                if (value < MinAge || value > MaxAge)
                {
                    throw new Exception($"{nameof(Age)} должен быть в дипазоне" +
                        $" от {MinAge} до {MaxAge}");
                }
                _age = value;
            }
        }

        /// <summary>
        /// Пол человека
        /// </summary>
        public Gender Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
            }
        }

        /// <summary>
        /// Проверка пола человека
        /// </summary>
        /// <param name="person">Человек</param>
        /// <returns>Пол человека</returns>
        public string GenderPerson(Person person) 
        {
            string gender = string.Empty;

            switch (person.Gender)
            {
                case Gender.Male:
                { 
                    gender = "Мужчина"; 
                    break;
                }
                case Gender.Female:
                {
                    gender = "Женщина"; 
                    break;
                }
                default:
                {
                    gender = "Введите свой пол!"; 
                    break;
                }
            }
            return $"Человек {person.Name} {person.Surname} - {gender}";
        }

        /// <summary>
        /// Информация о человеке
        /// </summary>
        /// <returns>Информация о человеке</returns>
        public string GetInfo()
        {
            return $"Человек по имени {_name} с фамилией {_surname}, " +
                $"возраст которого {_age} имеет {_gender} пол";
        }
    }
}