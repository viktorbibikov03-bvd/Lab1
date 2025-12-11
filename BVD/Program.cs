using Model;
using System;

namespace BVD
{
    /// <summary>
    /// Класс, в котором выполняется основная часть программы
    /// </summary>
    internal class Program
    {
        //TODO: RSDN
        /// <summary>
        /// Основной метод для выполнения программы
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="Exception"></exception>
        static void Main(string[] args)
        {
            PersonList PersonList1 = new PersonList();
            PersonList PersonList2 = new PersonList();
            Console.WriteLine("Создение первого списка персон с помощью ввода");

            for (int i = 0; i < 3; i++)
            {
                PersonList1.AddPerson(ReadPerson());
            }
            Console.WriteLine();
            Console.WriteLine("Создение второго списка персон рандомно");

            for (int i = 0; i < 3; i++)
            {
                PersonList2.AddPerson(GetRandomPerson());
            }
            Console.WriteLine();
            Print(PersonList1, PersonList2);
            Console.ReadKey();

            PersonList1.AddPerson(GetRandomPerson());
            Console.WriteLine();
            PersonList2.AddPerson(PersonList1.GetPersonInIndex(1));
            Console.WriteLine("Добавление нового человека в первый список");

            Console.WriteLine("Копия второго человека из первого списка во второй");
            Print(PersonList1, PersonList2);
            Console.ReadKey();

            PersonList1.RemovePersonInIndex(1);
            Console.WriteLine("Удаление второго человека из первого списка");
            Print(PersonList1, PersonList2);
            Console.ReadKey();

            PersonList2.Clear();
            Console.WriteLine("Очистка второго списка");
            Print(PersonList1, PersonList2);
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
            //TODO: RSDN
            Person PersonRead = new Person();

            var actionList = new List<PropertyHandlerTDO>
            {
                new PropertyHandlerTDO("имя",
                    new List<Type>
                        {
                           typeof(ArgumentNullException),
                           typeof(TypeAccessException),
                        },
                    () => { PersonRead.Name = Console.ReadLine(); }),
                new PropertyHandlerTDO("фамилию",
                    new List<Type>
                        {
                           typeof(ArgumentNullException),
                           typeof(TypeAccessException),
                        },
                    () => { PersonRead.Surname = Console.ReadLine(); }),
                new PropertyHandlerTDO("возраст",
                    new List<Type>
                        {
                           typeof(IndexOutOfRangeException)
                        },
                    () => { PersonRead.Age = int.Parse(
                        (Console.ReadLine())); }),
                new PropertyHandlerTDO("пол",
                    new List<Type>
                        {
                           typeof(ArgumentNullException),
                           typeof(ArgumentException),
                        },
                    () => { 
                        //TODO: RSDN
                        string[] gender_male_list = ["мужчина", 
                        "м", "1", "man", "m"];
                            string[] gender_female_list = ["женщина", 
                        "ж", "0", "woman", "w"];
                            string ReadGenderPerson = Console.ReadLine();
                            if (gender_male_list.Contains(
                                ReadGenderPerson.ToLower()))
                            {
                                PersonRead.Gender = Gender.Male;
                            }
                            else if (gender_female_list.Contains(
                                ReadGenderPerson.ToLower()))
                            {
                                PersonRead.Gender = Gender.Female;
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

            Console.WriteLine(PersonRead.GetInfo());
            return PersonRead;
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
            //TODO: RSDN
            string[] NameMaleList = ["Александр", "Дмитрий", "Сергей", "Иван", "Максим",
                                        "Николай", "Павел", "Роман", "Игорь", "Андрей",
                                        "Владимир", "Тимур", "Евгений", "Федор", "Кирилл",
                                        "Денис", "Артем", "Станислав", "Григорий", "Михаил"];
            string[] NameFemaleList = ["Анастасия", "Екатерина", "Мария", "Ольга", "Татьяна",
                                        "Наталья", "Светлана", "Елена", "Дарья", "Ксения",
                                        "Анна", "Юлия", "Виктория",  "Людмила", "Ирина",
                                        "Алена", "Полина", "Вероника", "София", "Евгения"];
            string[] LastnameMaleList = ["Иванов", "Петров", "Сидоров", "Кузнецов", "Смирнов",
                                            "Попов", "Лебедев", "Ковалев", "Новиков", "Морозов",
                                            "Федоров", "Соловьев", "Григорьев", "Васильев", "Тихонов",
                                            "Белов", "Зайцев", "Михайлов", "Сергеев", "Алексеев"];
            string[] LastnameFemaleList = ["Иванова", "Петрова", "Сидорова", "Кузнецова", "Смирнова",
                                            "Попова", "Лебедева", "Ковалева", "Новикова", "Морозова",
                                            "Федорова", "Соловьева", "Григорьева", "Васильева", "Тихонова",
                                            "Белова", "Зайцева", "Михайлова", "Сергеева", "Алексеева"];
            Gender[] GenderList = [Gender.Male, Gender.Female];
            Random rnd = new();

            int GenderIndex = rnd.Next(GenderList.Length);

            string NamePerson = GenderIndex == 0
                ? NameMaleList[rnd.Next(NameMaleList.Length)]
                : NameFemaleList[rnd.Next(NameFemaleList.Length)];
            string SurnamePerson = GenderIndex == 0
                ? LastnameMaleList[rnd.Next(LastnameMaleList.Length)]
                : LastnameFemaleList[rnd.Next(LastnameFemaleList.Length)];

            int Age = rnd.Next(Person.MinAge, Person.MaxAge);
            Gender RandomPersonGender = GenderList[GenderIndex];
            Person RandomPerson = new Person(NamePerson, SurnamePerson, 
                Age, RandomPersonGender);
            Console.WriteLine(RandomPerson.GetInfo());
            return RandomPerson;
        }
    }
}