using Models;
using Services;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Controllers
{
    /// <summary>
    /// Класс Компания
    /// </summary>
    public class Company : Ministry
    {
        #region Закрытые поля       

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор Компании
        /// </summary>
        /// <param name="nameCompany"> Название Компании </param>
        public Company(string nameCompany) : base(nameCompany)
        {            
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
            
        }

        /// <summary>
        /// Добавить главного бухгалтера
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        public new void AddChiefAccountant(string name, string surname, long age)
        {
            
        }

        /// <summary>
        /// Добавить заместителя директора
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        public new void AddDeputyDirector(string name, string surname, long age)
        {
            
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
            return false;
        }

        #endregion

        #region Закрытые методы

        /// <summary>
        /// Сохранить лист работников в файл
        /// </summary>
        /// <param name="paht"> Путь </param>
        private async void SaveListWorkersToFile(string paht)
        {
            await Task.Run(() => 
            {
                BindingList<Worker> WorkersList = GetListOfAllWorkers();
                FileIOService.SaveAsJSON(paht, WorkersList); 
            });           
        }

        /// <summary>
        /// Загрузить список работников из файла
        /// </summary>
        /// <param name="path"> Путь </param>
        private void LoadWorkerListFromFile(string path)
        {
            BindingList<Worker> WorkersList = FileIOService.OpenAsJSON(path);

            SetListOfAllWorkers(WorkersList);
        }

        #endregion
    }
}
