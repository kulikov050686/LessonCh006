﻿using System;

namespace Models
{
    /// <summary>
    /// Руководитель
    /// </summary>
    public class Supervisor : IWorker
    {        
        string name;
        string surname;
        long age;
        double salary;
        string jobTitle;
                
        public string Name
        {
            get => name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Имя руководителя не может быть пустым!!!");
                }

                name = value;
            }
        }
        public string Surname
        {
            get => surname;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Фамилия руководителя не может быть пустой!!!");
                }

                surname = value;
            }
        }
        public long Age
        {
            get => age;

            set
            {
                if (value < 18 && value > 99)
                {
                    throw new ArgumentException("Невозможный возраст руководителя!!!");
                }

                age = value;
            }
        }
        public double Salary
        {
            get => salary;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Невозможная зарплата руководителя!!!");
                }

                salary = value;
            }
        }
        public string JobTitle
        {
            get => jobTitle;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Название должности руководителя не может быть пустым!!!");
                }

                jobTitle = value;
            }
        }
        public EmployeePosition EmployeePosition { get; set; }

        /// <summary>
        /// Конструктор руководителя
        /// </summary>        
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        /// <param name="salary"> Зарплата </param>
        /// <param name="jobTitle"> Название должности </param>
        public Supervisor(string name, string surname, long age, double salary, string jobTitle)
        {            
            Name = name;
            Surname = surname;
            Age = age;
            Salary = salary;
            JobTitle = jobTitle;
            EmployeePosition = EmployeePosition.Supervisor;
        }
    }
}
