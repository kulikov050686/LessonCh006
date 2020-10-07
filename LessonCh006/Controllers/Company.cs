using Models;
using Services;

namespace Controllers
{
    /// <summary>
    /// Класс Компания
    /// </summary>
    public class Company : Ministry
    {
        #region Закрытые поля

        DataBase dataBase;

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор Компании
        /// </summary>
        /// <param name="nameCompany"> Название Компании </param>
        public Company(string nameCompany) : base(nameCompany)
        {
            dataBase = new DataBase(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\MyDocument\Visual C#\LessonCh006\LessonCh006\WorkerDataBase.mdf;Integrated Security=True");
        }

        #endregion

        #region Открытые методы

        /// <summary>
        /// Добавить генерального директора
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        public new void AddGeneralDirector(string name, string surname, long age)
        {
            base.AddGeneralDirector(name, surname, age);

            dataBase.AddWorkerToDataBase(new Worker(GeneralDirector, null));
        }

        /// <summary>
        /// Добавить главного бухгалтера
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        public new void AddChiefAccountant(string name, string surname, long age)
        {
            base.AddChiefAccountant(name, surname, age);

            dataBase.AddWorkerToDataBase(new Worker(ChiefAccountant, null));
        }

        /// <summary>
        /// Добавить заместителя директора
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        public new void AddDeputyDirector(string name, string surname, long age)
        {
            base.AddDeputyDirector(name, surname, age);

            dataBase.AddWorkerToDataBase(new Worker(DeputyDirector, null));
        }

        /// <summary>
        /// Добавить руководителя депортамента
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        /// <param name="jobTitle"> Название занимаемой должности </param>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        public new bool AddSupervisorDepartment(string name, string surname, long age, string jobTitle, string pathToDepartment)
        {
            if(base.AddSupervisorDepartment(name, surname, age, jobTitle, pathToDepartment))
            {
                dataBase.AddWorkerToDataBase(new Worker(GetSupervisorOfDepartment(pathToDepartment), pathToDepartment));
                return true;
            }

            return false;
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
        public new bool AddIntern(string name, string surname, long age, double salary, string jobTitle, string pathToDepartment)
        {
            if(base.AddIntern(name, surname, age, salary, jobTitle, pathToDepartment))
            {
                dataBase.AddWorkerToDataBase(new Worker(name, surname, age, salary, jobTitle, EmployeePosition.Intern, pathToDepartment));
                return true;
            }

            return false;
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
        public new bool AddEmployee(string name, string surname, long age, double salary, string jobTitle, string pathToDepartment)
        {
            if(base.AddEmployee(name, surname, age, salary, jobTitle, pathToDepartment))
            {
                dataBase.AddWorkerToDataBase(new Worker(name, surname, age, salary, jobTitle, EmployeePosition.Employee, pathToDepartment));
                return true;
            }

            return false;
        }

        #endregion
    }
}
