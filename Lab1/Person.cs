using System;

namespace Lab1 
{
    /// <summary>
    /// Информация о человеке
    /// </summary>
    class Person
    {
        /// <summary>
        /// Имя
        /// </summary>
        private string _name;

        /// <summary>
        /// Фамилия
        /// </summary>
        private string _surname { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        private int _age { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        private Gender _gender { get; set; }



    }
}
