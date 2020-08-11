using System;
using System.ComponentModel;

namespace Models
{
    /// <summary>
    /// Департамент
    /// </summary>
    public class Department
    {        
        string nameDepartment;        

        /// <summary>
        /// Название департамента
        /// </summary>
        public string NameDepartment
        { 
            get => nameDepartment;
            
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Название департамента не может быть пустым!!!");
                }

                nameDepartment = value;
            }
        }

        /// <summary>
        /// Начальник депертамента
        /// </summary>
        public Supervisor Supervisor { get; set; }

        /// <summary>
        /// Лист стажёров департамента
        /// </summary>
        public BindingList<Intern> Interns { get; set; }

        /// <summary>
        /// Лист сотрудников департамента
        /// </summary>
        public BindingList<Employee> Employees { get; set; }

        /// <summary>
        /// Лист поддепартаментов
        /// </summary>
        public BindingList<Department> NextDepartments { get; set; } 
        
        /// <summary>
        /// Количество поддепартаментов в данном департаменте
        /// </summary>
        public int CountDepartments
        {
            get
            {
                if(NextDepartments != null)
                {
                    return NextDepartments.Count;
                }

                return 0;
            }
        }

        /// <summary>
        /// Количество стажёров в данном департаменте
        /// </summary>
        public int CountInterns
        {
            get
            {
                if(Interns != null)
                {
                    return Interns.Count;
                }

                return 0;
            }
        }

        /// <summary>
        /// Количество работников в данном департаменте
        /// </summary>
        public int CountEmployees
        {
            get
            {
                if(Employees != null)
                {
                    return Employees.Count;
                }

                return 0;
            }
        }

        /// <summary>
        /// Конструктор департамента
        /// </summary>
        /// <param name="nameDepartment"> Название департамента </param>
        /// <param name="id"> Идентификатор департамента </param>
        public Department(string nameDepartment)
        {
            NameDepartment = nameDepartment;                     
        }
    }
}
