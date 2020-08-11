using System;

namespace Models
{
    /// <summary>
    /// Работник
    /// </summary>
    public class Worker : IWorker
    {        
        string name;
        string surname;
        long age;
        double salary;
        string jobTitle;
        string pathDepartment;        
        EmployeePosition employeePosition;
                
        public string Name
        {
            get => name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Имя работника не может быть пустым!!!");
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
                    throw new ArgumentNullException("Фамилия работника не может быть пустой!!!");
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
                    throw new ArgumentException("Невозможный возраст работника!!!");
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
                    throw new ArgumentException("Невозможная зарплата работника!!!");
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
                    throw new ArgumentNullException("Название должности работника не может быть пустым!!!");
                }

                jobTitle = value;
            }
        }

        /// <summary>
        /// Путь до департамента работника
        /// </summary>
        public string PathDepartment
        {
            get => pathDepartment;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Название должности работника не может быть пустым!!!");
                }

                pathDepartment = value;
            }
        }

        /// <summary>
        /// Занимаемая должность работника
        /// </summary>
        public EmployeePosition EmployeePosition
        {
            get => employeePosition;
            
            set
            {
                employeePosition = value;
            }
        }

        /// <summary>
        /// Конструктор работник
        /// </summary>
        /// <param name="worker"> Работник </param>
        /// <param name="employeePosition"> Занимаемая должность </param>
        /// <param name="pathDepartment"> Путь до департамента работника </param>
        public Worker(IWorker worker, EmployeePosition employeePosition, string pathDepartment)
        {
            Name = worker.Name;
            Surname = worker.Surname;
            Age = worker.Age;
            Salary = worker.Salary;
            JobTitle = worker.JobTitle;
            EmployeePosition = employeePosition;
            PathDepartment = pathDepartment;
        }
    }
}
