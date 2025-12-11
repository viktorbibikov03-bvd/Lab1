using Model;
using System;

namespace BVD
{
    /// <summary>
    /// Класс, в котором выполняется основная часть программы
    /// </summary>
    internal class Program
    {
        //TODO: RSDN +
        /// <summary>
        /// Точка входа в программу
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="Exception">Исключения</exception>
        static void Main(string[] args)
        {
            PersonList personList1 = new PersonList();
            PersonList personList2 = new PersonList();
            Console.WriteLine("Создение первого списка персон с помощью ввода");

            for (int i = 0; i < 3; i++)
            {
                personList1.AddPerson(ReadPerson());
            }
            Console.WriteLine();
            Console.WriteLine("Создение второго списка персон рандомно");

            for (int i = 0; i < 3; i++)
            {
                personList2.AddPerson(GetRandomPerson());
            }
            Console.WriteLine();
            Print(personList1, personList2);
            Console.ReadKey();

            personList1.AddPerson(GetRandomPerson());
            Console.WriteLine();
            personList2.AddPerson(personList1.GetPersonInIndex(1));
            Console.WriteLine("Добавление нового человека в первый список");

            Console.WriteLine("Копия второго человека из первого списка во второй");
            Print(personList1, personList2);
            Console.ReadKey();

            personList1.RemovePersonInIndex(1);
            Console.WriteLine("Удаление второго человека из первого списка");
            Print(personList1, personList2);
            Console.ReadKey();

            personList2.Clear();
            Console.WriteLine("Очистка второго списка");
            Print(personList1, personList2);
            Console.ReadKey();
        }

        /// <summary>
        /// Вывод списков людей
        /// </summary>
        /// <param name="personList1">Первый список людей</param>
        /// <param name="personList2">Второй список людей</param>
        internal static void Print(PersonList personList1, PersonList personList2)
        {
            Console.WriteLine("Список 1:");
            personList1.PersonInfoFromPersonList();
            Console.WriteLine("Список 2:");
            personList2.PersonInfoFromPersonList();
        }

        /// <summary>
        /// Создает экземпляр класса Person со значениями атрибутов, 
        /// введенными с клавиатуры 
        /// </summary>
        /// <returns>Человек</returns>
        public static Person ReadPerson()
        {
            //TODO: RSDN +
            Person personRead = new Person();

            var actionList = new List<PropertyHandlerTDO>
            {
                new PropertyHandlerTDO("имя",
                    new List<Type>
                        {
                            typeof(ArgumentNullException),
                            typeof(TypeAccessException),
                        },
                    () => { personRead.Name = Console.ReadLine(); }),
                new PropertyHandlerTDO("фамилию",
                    new List<Type>
                        {
                            typeof(ArgumentNullException),
                            typeof(TypeAccessException),
                        },
                    () => { personRead.Surname = Console.ReadLine(); }),
                new PropertyHandlerTDO("возраст",
                    new List<Type>
                        {
                            typeof(IndexOutOfRangeException)
                        },
                    () => { personRead.Age = int.Parse(
                        (Console.ReadLine())); }),
                new PropertyHandlerTDO("пол",
                    new List<Type>
                        {
                            typeof(ArgumentNullException),
                            typeof(ArgumentException),
                        },
                    () => { 
                        //TODO: RSDN +
                        string[] genderMaleList = {"мужчина",
                        "м", "1", "man", "m"};
                        string[] genderFemaleList = {"женщина",
                        "ж", "0", "woman", "w"};
                        string readGenderPerson = Console.ReadLine();
                        if (genderMaleList.Contains(
                            readGenderPerson.ToLower()))
                        {
                            personRead.Gender = Gender.Male;
                        }
                        else if (genderFemaleList.Contains(
                            readGenderPerson.ToLower()))
                        {
                            personRead.Gender = Gender.Female;
                        }
                        else
                        {
                            throw new ArgumentException(
                                "Для мужчин значения пола могут" +
                                " иметь значения 'мужчина', 'м', '1', " +
                                "'man', 'm'\n" +
                                 "Для женщин " +
                                 "значения пола могут" +
                                 " иметь значения 'женщина'" +
                                 ", 'ж', '0', 'woman', 'w'");
                        }
                          })

            };

            for (int i = 0; i < actionList.Count; i++)
            {
                PersonPropertiesHandler(actionList[i]);
            }

            Console.WriteLine(personRead.GetInfo());
            return personRead;
        }

        /// <summary>
        /// Метод распаковки actionList
        /// </summary>
        /// <param name="propertyHandelerDto">actionList</param>
        private static void PersonPropertiesHandler(
            PropertyHandlerTDO propertyHandelerDto)
        {
            var personField = propertyHandelerDto.PropertyName;
            var personTypes = propertyHandelerDto.ExceptionTypes;
            var personAction = propertyHandelerDto.PropertyHandlingAction;
            Console.WriteLine($"Введите {personField} персоны:");
            while (true)
            {
                try
                {
                    personAction.Invoke();
                    break;
                }
                catch (Exception exeption)
                {
                    Console.WriteLine(exeption.Message);
                    continue;
                }
            }
        }

        /// <summary>
        /// Создает экземпляр класса Person со случайными значениями атрибутов
        /// </summary>
        /// <returns>Персона</returns>
        public static Person GetRandomPerson()
        {
            //TODO: RSDN +
            string[] nameMaleList = { "Александр", "Дмитрий" };
            string[] nameFemaleList = { "Анастасия", "Екатерина" };
            string[] surnameMaleList = { "Иванов", "Петров" };
            string[] surnameFemaleList = { "Иванова", "Петрова" };
            Gender[] genderList = { Gender.Male, Gender.Female };

            Random random = new();

            int GenderIndex = random.Next(genderList.Length);

            string NamePerson = GenderIndex == 0
                ? nameMaleList[random.Next(nameMaleList.Length)]
                : nameFemaleList[random.Next(nameFemaleList.Length)];
            string SurnamePerson = GenderIndex == 0
                ? surnameMaleList[random.Next(surnameMaleList.Length)]
                : surnameFemaleList[random.Next(surnameFemaleList.Length)];

            int Age = random.Next(Person.MinAge, Person.MaxAge);
            Gender RandomPersonGender = genderList[GenderIndex];
            Person RandomPerson = new Person(NamePerson, SurnamePerson, 
                Age, RandomPersonGender);
            Console.WriteLine(RandomPerson.GetInfo());
            return RandomPerson;
        }
    }
}