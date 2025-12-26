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
        /// Список людей, принимает объект класса Person
        /// </summary>
        private List<Person> _persons;

        /// <summary>
        /// Конструктор класса по умолчанию
        /// </summary>
        public PersonList()
        {
            _persons = new List<Person>();
        }

        /// <summary>
        /// Создает новый список людей с указанными элементами
        /// </summary>
        /// <param name="persons">
        /// Перечисление людей для инициализации списка</param>
        /// <exception cref="ArgumentNullException">
        /// Возникает, если параметр <paramref name="persons"/> равен null
        /// </exception>
        public PersonList(IEnumerable<Person> persons)
        {
            if (persons == null)
            {
                throw new ArgumentNullException(nameof(persons));
            }

            _persons = new List<Person>(persons);
        }

        /// <summary>
        /// Добавление людей в список
        /// </summary>
        /// <param name="person">Человек</param>
        public void AddPerson(Person person)
        {
            _persons.Add(person);
        }

        /// <summary>
        /// Проверка наличия человека в списке
        /// </summary>
        /// <param name="person">Человек</param>
        /// <returns>true при наличии, false при отстутствии</returns>
        public bool Contains(Person person)
        {
            foreach (Person inPerson in _persons)
            {
                if (inPerson == person)
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
            _persons.Remove(person);
        }

        /// <summary>
        /// Получение человека из списка по индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <returns>Человек с заданным индексом</returns>
        public Person GetPersonInIndex(int index)
        {
            return _persons[index];
        }

        /// <summary>
        /// Удаление человека по индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        public void RemovePersonInIndex(int index)
        {
            _persons.Remove(GetPersonInIndex(index));
        }

        /// <summary>
        /// Получение индекса человека из списка
        /// </summary>
        /// <param name="person">Человек</param>
        /// <returns>Индекс человека</returns>
        /// <exception cref="ArgumentException">Исключение 
        /// при введении человека, которого нет в списке</exception>
		public int GetIndex(Person person) => _persons.IndexOf(person);

        /// <summary>
        /// Очистка списка людей
        /// </summary>
        public void Clear()
        {
            _persons.Clear();
        }

        /// <summary>
        /// Размер листа с людьми
        /// </summary>
        public int Count => _persons.Count;
    }
}