using System;

namespace Models
{
    /// <summary>
    /// Работник
    /// </summary>
    public class Worker : IWorker
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long Age { get; set; }
        public double Salary { get; set; }
        public string JobTitle { get; set; }
        public EmployeePosition EmployeePosition { get; set; }

        /// <summary>
        /// Путь до департамента
        /// </summary>
        public string PathToDepartment { get; set; }
        
        /// <summary>
        /// Конструктор работник
        /// </summary>
        /// <param name="worker"> Работник </param>
        /// <param name="employeePosition"> Занимаемая должность </param>
        /// <param name="pathDepartment"> Путь до департамента работника </param>
        public Worker(IWorker worker, string pathToDepartment)
        {
            Name = worker.Name;
            Surname = worker.Surname;
            Age = worker.Age;
            Salary = worker.Salary;
            JobTitle = worker.JobTitle;
            EmployeePosition = worker.EmployeePosition;
            PathToDepartment = pathToDepartment;
        }
    }
}
