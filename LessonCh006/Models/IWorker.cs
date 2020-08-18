namespace Models
{
    /// <summary>
    /// Интерфейс работник
    /// </summary>
    public interface IWorker
    {        
        /// <summary>
        /// Имя работника
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Фамилия работника
        /// </summary>
        string Surname { get; set; }

        /// <summary>
        /// Возраст работника
        /// </summary>
        long Age { get; set; }

        /// <summary>
        /// Зарплата работника
        /// </summary>
        double Salary { get; set; }

        /// <summary>
        /// Название должности работника
        /// </summary>
        string JobTitle { get; set; }

        /// <summary>
        /// Статус работника
        /// </summary>
        EmployeePosition EmployeePosition { get; set; }
    }
}
