using System;
using System.Security.Cryptography.X509Certificates;

namespace Model
{
    /// <summary>
    /// Класс списка с людьми
    /// </summary>
    public class PersonList
    {
        /// <summary>
        /// Создание листа для хранения информации о людях
        /// </summary>
        public List<Person> ListPerson { get; } = new List<Person>();

        /// <summary>
        /// Добавление людей в список
        /// </summary>
        /// <param name="person">Человек</param>
        public void AddPerson(Person person)
        { 
            ListPerson.Add(person);
        }

        /// <summary>
        /// Получение информации о людях в списке
        /// </summary>
        public void PersonInfoFromPersonList()
        {
            foreach (Person person in ListPerson)
            {
                Console.WriteLine(person.GetInfo());
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Проверка наличия человека в списке
        /// </summary>
        /// <param name="person">Человек</param>
        /// <returns>true при наличии, false при отстутствии</returns>
        public bool Contains(Person person)
        {
            foreach (Person InPerson in ListPerson)
            {
                if (InPerson == person)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Удаление человека по имени
        /// </summary>
        /// <param name="person">Человек</param>
        public void Remove(Person person)
        {
            ListPerson.Remove(person);
        }

        /// <summary>
        /// Получение человека из списка по индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <returns>Человек с заданным индексом</returns>
        public Person GetPersonInIndex(int index)
        {
            return ListPerson[index];
        }

        /// <summary>
        /// Удаление человека по индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        public void RemovePersonInIndex(int index)
        {
            ListPerson.Remove(GetPersonInIndex(index));
        }

        /// <summary>
        /// Получение индекса человека из списка
        /// </summary>
        /// <param name="person">Человек</param>
        /// <returns>Индекс человека</returns>
        /// <exception cref="ArgumentException">Исключение 
        /// при введении человека, которого нет в списке</exception>
		public int GetIndex(Person person) => ListPerson.IndexOf(person);

        /// <summary>
        /// Очистка списка людей
        /// </summary>
        public void Clear()
        {
            ListPerson.Clear();
        }
        /// <summary>
        /// Размер листа с людьми
        /// </summary>
        public int Count => ListPerson.Count;
    }
}