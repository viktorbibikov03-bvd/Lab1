using Model;
using System;
using System.Xml.Linq;

namespace BVD
{
    /// <summary>
    /// Класс, в котором выполняется основная часть программы
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Точка входа в программу
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="Exception">Исключения</exception>
        static void Main(string[] args)
        {
            var (firstPersonList, secondPersonList) = InitializeObjects();

            var testDictionary = new Dictionary<string, Action>
            {
                ["Вывод содержимого списков"] = () =>
                    PrintList(firstPersonList, secondPersonList),
                ["Добавление нового человека"] = () =>
                    AddPerson(firstPersonList),
                ["Копирование человека"] = () =>
                    CopyPerson(firstPersonList, secondPersonList),
                ["Удаление человека"] = () =>
                    RemovePerson(firstPersonList, secondPersonList),
                ["Очиска списка"] = () =>
                    ClearList(secondPersonList),
                ["Генерация случайного человека"] = () =>
                    GeneratePerson(),
                ["Ввод нового человека"] = () =>
                    InputPerson()
            };

            StartTesting(testDictionary);
        }

        /// <summary>
        /// Метод для инициализации экземпляров
        /// </summary>
        /// <returns>Два объекта класса PersonList</returns>
        public static (PersonList firstList, PersonList secondList)
        InitializeObjects()
        {
            Console.WriteLine("Создание списков...");

            Person person1 = new Person("Виктор", "Бибиков", 23, Gender.Male);
            Person person2 = new Person("Дастиен", "Швайнштайгер", 30, Gender.Male);
            Person person3 = new Person("Любовь", "Подопригора", 24, Gender.Female);
            Person person4 = new Person("Александр", "Данилов", 45, Gender.Male);
            Person person5 = new Person("Матвей", "Бибиков", 18, Gender.Female);
            Person person6 = new Person("Альбина", "Бибикова", 34, Gender.Male);

            PersonList firstPersonList = new PersonList(new Person[]
            {
                person1,
                person2,
                person3
            });

            PersonList secondPersonList = new Model.PersonList(new Person[]
            {
                person4,
                person5,
                person6
            });

            Console.WriteLine("Списки созданы!");
            WaitForKey();

            return (firstPersonList, secondPersonList);
        }

        /// <summary>
        /// Метод, запускающий тест функционала
        /// </summary>
        /// <param name="testDictionary">Словарь, хранящий лямбда-выражения</param>
        public static void StartTesting(Dictionary<string, Action> testDictionary)
        {
            foreach (var test in testDictionary)
            {
                Console.WriteLine($"==={test.Key}===\n");
                test.Value();
                WaitForKey();
            }
        }

        /// <summary>
        /// Метод для вывода списков
        /// </summary>
        /// <param name="firstPersonList">Первый список</param>
        /// <param name="secondPersonList">Второй список</param>
        public static void PrintList(PersonList firstPersonList,
            PersonList secondPersonList)
        {
            PrintList(firstPersonList, "Первый список");
            PrintList(secondPersonList, "Второй список");
        }

        /// <summary>
        /// Метод теста добавления человека в первый список
        /// </summary>
        /// <param name="firstPersonList">Первый список</param>
        public static void AddPerson(PersonList firstPersonList)
        {
            //TODO: rename
            Person person7 = new Person("Аркадий", "Сидоров", 40, Gender.Male);
            firstPersonList.AddPerson(person7);
            Console.WriteLine($"В первый список добавлен {person7.Name}" +
                $" {person7.Surname}");
            PrintList(firstPersonList, "Первый список");
        }

        /// <summary>
        /// Метод теста копирования человека из первого списка во второй
        /// </summary>
        /// <param name="firstPersonList">Первый список</param>
        /// <param name="secondPersonList">Второй список</param>
        public static void CopyPerson(PersonList firstPersonList,
            PersonList secondPersonList)
        {
            Person copiedPerson = firstPersonList.GetPersonInIndex(1);
            secondPersonList.AddPerson(copiedPerson);
            Console.WriteLine($"Во второй список скопирован" +
                $" {copiedPerson.Name} {copiedPerson.Surname}");
            PrintList(firstPersonList, "Первый список");
            PrintList(secondPersonList, "Обновленный второй список");
        }

        /// <summary>
        /// Метод для удаления человека из первого списка
        /// </summary>
        /// <param name="firstPersonList">Первый список</param>
        /// <param name="secondPersonList">Второй список</param>
        public static void RemovePerson(PersonList firstPersonList,
            PersonList secondPersonList)
        {
            firstPersonList.RemovePersonInIndex(2);
            Console.WriteLine("В первом списке удален третий человек");
            PrintList(firstPersonList, "Первый список после удаления");
            PrintList(secondPersonList, "Второй список");
        }

        /// <summary>
        /// Очистка списка
        /// </summary>
        /// <param name="secondPersonList">Первый список</param>
        /// //TODO: rename
        public static void ClearList(PersonList secondPersonList)
        {
            secondPersonList.Clear();
            PrintList(secondPersonList, "Очищенный список");
        }

        /// <summary>
        /// Генерация человека
        /// </summary>
        public static void GeneratePerson()
        {
            Person person = GetRandomPerson();
            PrintPerson(person);
        }

        /// <summary>
        /// Ввод человека с клавиатуры
        /// </summary>
        public static void InputPerson()
        {
            Person person = InputFromConsole();
            PrintPerson(person);
        }

        /// <summary>
        /// Метод ввода пользователя с клавиатуры
        /// </summary>
        /// <returns>Объект класса Person</returns>
        /// <exception cref="Exception">Возникает, если пол введен
        /// в некорректном формате </exception>
        public static Person InputFromConsole()
        {
            Person person = new Person();

            var inputDictionary = new Dictionary<string, Action>()
            {
                {
                    "Имя",
                    new Action(() =>
                        {
                            string input = Console.ReadLine();
                            if (!Person.CheckNameAndSurname(input))
                            {
                                throw new Exception("Имя должно " +
                                    "содержать только буквы!");
                            }
    
                            person.Name = input;
                        })
                },
                {
                    "Фамилию",
                    new Action(() =>
                        {
                            string input = Console.ReadLine();
                            if (!Person.CheckNameAndSurname(input))
                            {
                                throw new Exception("Фамилия должна " +
                                    "содержать только буквы!");
                            }
                  
                            person.Surname = input;
                        })
                },
                {
                    "Возраст",
                    new Action(() =>
                        {
                            if (int.TryParse(Console.ReadLine(), out int age))
                            {
                                person.Age = age;
                            }
                            else
                            {
                                throw new Exception(
                                    "В поле 'Возраст' необходимо вводить " +
                                    "число!");
                            }
                        })
                },
                {
                    "Пол",
                    new Action(() =>
                        {
                            Console.Write("(1 - мужской, 2 - женский): ");

                            string inputNumber = Console.ReadLine();

                            switch (inputNumber)
                            {
                                case "1":
                                {
                                    person.Gender = Gender.Male;
                                    break;
                                }
                                case "2":
                                {
                                    person.Gender = Gender.Female;
                                    break;
                                }
                                default:
                                {
                                    throw new Exception("Введите число 1, " +
                                        "если вы мужчина и число 2, " +
                                        "если вы женщина");
                                }
                            }
                        })
                }
            };

            foreach (var actionHandler in inputDictionary)
            {
                ActionHandler(actionHandler.Value, actionHandler.Key);
            }

            return person;
        }

        /// <summary>
        /// Метод, выполняющий действия в оболочке try и while
        /// </summary>
        /// <param name="action">Действие</param>
        /// <param name="enteredValue">Введенная строка</param>
        public static void ActionHandler(Action action, string enteredValue)
        {
            while (true)
            {
                try
                {
                    Console.Write($"Пожалуйста, " +
                        $"введите {enteredValue} человека: ");
                    action.Invoke();
                    return;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        /// <summary>
        /// Вывод информации о человеке
        /// </summary>
        public static void PrintPerson(Person person)
        {
            Console.WriteLine($"Человек {person.Name} {person.Surname}" +
                $" в возрасте {person.Age} имеет {GetGender(person)} пол");
        }

        /// <summary>
        /// Метод получения пола
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Метод получения пола
        /// </summary>
        /// <returns></returns>
        private static string GetGender(Person person)
        {
            return person.Gender == Gender.Male ? "Мужской" : "Женский";
        }

        /// <summary>
        /// Выводит список людей на экран
        /// </summary>
        /// <param name="list">Список для вывода</param>
        /// <param name="listName">Название списка</param>
        private static void PrintList(PersonList list, string listName)
        {
            Console.WriteLine(listName);

            for (int i = 0; i < list.Count; i++)
            {
                Person person = list.GetPersonInIndex(i);
                Console.Write($"    {i + 1}) ");
                PrintPerson(person);
            }
        }

        /// <summary>
        /// Метод для паузы между пунктами программы
        /// </summary>
        private static void WaitForKey()
        {
            Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить");
            Console.ReadKey();
            Console.WriteLine();
        }

        /// <summary>
        /// Создает экземпляр класса Person со случайными значениями атрибутов
        /// </summary>
        /// <returns>Персона</returns>
        public static Person GetRandomPerson()
        {
            string[] nameMaleList = { "Александр", "Дмитрий" };
            string[] nameFemaleList = { "Анастасия", "Екатерина" };
            string[] surnameMaleList = { "Иванов", "Петров" };
            string[] surnameFemaleList = { "Иванова", "Петрова" };
            Gender[] genderList = { Gender.Male, Gender.Female };

            Random random = new();

            //TODO: RSDN
            int GenderIndex = random.Next(genderList.Length);
            //TODO: RSDN
            string NamePerson = GenderIndex == 0
                ? nameMaleList[random.Next(nameMaleList.Length)]
                : nameFemaleList[random.Next(nameFemaleList.Length)];
            //TODO: RSDN
            string SurnamePerson = GenderIndex == 0
                ? surnameMaleList[random.Next(surnameMaleList.Length)]
                : surnameFemaleList[random.Next(surnameFemaleList.Length)];
            //TODO: RSDN
            int Age = random.Next(Person.MinAge, Person.MaxAge);
            //TODO: RSDN
            Gender RandomPersonGender = genderList[GenderIndex];
            //TODO: RSDN
            Person RandomPerson = new Person(NamePerson, SurnamePerson,
                Age, RandomPersonGender);
            return RandomPerson;
        }
    }
}