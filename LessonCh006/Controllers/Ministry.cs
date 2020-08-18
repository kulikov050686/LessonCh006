using Models;

namespace Controllers
{
    class Ministry : MinistryBase
    {
        /// <summary>
        /// Конструктор министерства
        /// </summary>
        /// <param name="nameMinistry"> Название министерства </param>
        public Ministry(string nameMinistry) : base(nameMinistry) {}

        /// <summary>
        /// Добавить генерального директора
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>        
        public void AddGeneralDirector(string name, string surname, long age)
        {
            GeneralDirector = new Supervisor(name, surname, age, 0, "Генеральный директор");
        }

        /// <summary>
        /// Добавить главного бугалтера
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        public void AddChiefAccountant(string name, string surname, long age)
        {
            ChiefAccountant = new Supervisor(name, surname, age, 0, "Главный бугалтер");
        }

        /// <summary>
        /// Добавить заместителя директора
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        public void AddDeputyDirector(string name, string surname, long age)
        {
            DeputyDirector = new Supervisor(name, surname, age, 0, "Заместитель генерального директора");
        }

        /// <summary>
        /// Добавить руководителя депортамента
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        /// <param name="jobTitle"> Название занимаемой должности </param>
        /// <param name="pathToDepartment"> Путь до департамента </param>        
        public bool AddSupervisorDepartment(string name, string surname, long age, string jobTitle, string pathToDepartment)
        {
            return AddDeleteSupervisor(new Supervisor(name, surname, age, 0, jobTitle), pathToDepartment);
        }
        
        /// <summary>
        /// Удалить руководителя департамента
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>        
        public bool DeleteSupervisorDepartment(string pathToDepartment)
        {
            return AddDeleteSupervisor(null, pathToDepartment);
        }

        /// <summary>
        /// Добавить интерна в департамент
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        /// <param name="salary"> зарплата </param>
        /// <param name="jobTitle"> Название занимаемой должности </param>
        /// <param name="pathToDepartment"> Путь до департамента </param>       
        public bool AddIntern(string name, string surname, long age, double salary, string jobTitle, string pathToDepartment)
        {
            return AddWorker(new Intern(name, surname, age, salary, jobTitle), pathToDepartment);
        }

        /// <summary>
        /// Удалить интерна из департамента
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        /// <param name="salary"> зарплата </param>
        /// <param name="jobTitle"> Название занимаемой должности </param>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        public bool DeleteIntern(string name, string surname, long age, double salary, string jobTitle, string pathToDepartment)
        {
            return DeleteWorker(new Intern(name, surname, age, salary, jobTitle), pathToDepartment);
        }

        /// <summary>
        /// Добавить сотрудника в депортамент
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        /// <param name="salary"> Зарплата </param>
        /// <param name="jobTitle"> Название занимаемой должности</param>
        /// <param name="pathToDepartment"> Путь до департамента </param>        
        public bool AddEmployee(string name, string surname, long age, double salary, string jobTitle, string pathToDepartment)
        {
            return AddWorker(new Employee(name, surname, age, salary, jobTitle), pathToDepartment);
        }

        /// <summary>
        /// Удалить сотрудника из департамента
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        /// <param name="salary"> Зарплата </param>
        /// <param name="jobTitle"> Название занимаемой должности</param>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        public bool DeleteEmployee(string name, string surname, long age, double salary, string jobTitle, string pathToDepartment)
        {
            return DeleteWorker(new Employee(name, surname, age, salary, jobTitle), pathToDepartment);
        }

        #region Закрытые методы

        #endregion
    }
}
