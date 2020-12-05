using System;

namespace Models
{
    /// <summary>
    /// Базовый класс работник
    /// </summary>
    public abstract class BaseWorker
    {        
        string _name;
        string _surname;
        long _age;
        double _salary;
        string _jobTitle;

        /// <summary>
        /// Идентификатор
        /// </summary>
        public ulong Id { get; set; }       

        /// <summary>
        /// Имя
        /// </summary>
        public string Name
        {
            get => _name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Имя не может быть пустым!!!");
                }

                _name = value;
            }
        }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname
        {
            get => _surname;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Фамилия не может быть пустой!!!");
                }

                _surname = value;
            }
        }

        /// <summary>
        /// Возраст
        /// </summary>
        public long Age
        {
            get => _age;

            set
            {
                if (value < 18 && value > 99)
                {
                    throw new ArgumentException("Невозможный возраст!!!");
                }

                _age = value;
            }
        }

        /// <summary>
        /// Зарплата
        /// </summary>
        public double Salary
        {
            get => _salary;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Невозможная зарплата!!!");
                }

                _salary = value;
            }
        }

        /// <summary>
        /// Название должности
        /// </summary>
        public string JobTitle
        {
            get => _jobTitle;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Название должности не может быть пустым!!!");
                }

                _jobTitle = value;
            }
        }

        /// <summary>
        /// Статус
        /// </summary>
        public EmployeePosition EmployeePosition { get; set; }

        /// <summary>
        /// Конструктор 
        /// </summary>        
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        /// <param name="salary"> Зарплата </param>
        /// <param name="jobTitle"> Название должности </param>
        public BaseWorker(string name, string surname, long age, double salary, string jobTitle)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Salary = salary;
            JobTitle = jobTitle;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id"> Идентификатор </param>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        /// <param name="salary"> Зарплата </param>
        /// <param name="jobTitle"> Название должности </param>
        public BaseWorker(ulong id, string name, string surname, long age, double salary, string jobTitle)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
            Salary = salary;
            JobTitle = jobTitle;
        }

        /// <summary>
        /// Определяет равны ли объекты друг другу
        /// </summary>        
        public bool Equals(BaseWorker worker)
        {
            if(worker != null)
            {
                return (worker.Id == Id) &&
                       (worker.Name == Name) &&
                       (worker.Surname == Surname) &&
                       (worker.Age == Age) &&
                       (worker.Salary == Salary) &&
                       (worker.JobTitle == JobTitle) &&
                       (worker.EmployeePosition == EmployeePosition);
            }

            return false;
        }
    }
}
