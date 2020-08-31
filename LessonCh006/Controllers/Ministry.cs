using Models;
using System.ComponentModel;

namespace Controllers
{
    class Ministry : MinistryBase
    {
        #region Закрытые поля

        BindingList<Worker> workers;
        double salaryGeneralDirector = 0;
        double salaryChiefAccountant = 0;
        double salaryDeputyDirector = 0;
        double salarySupervisorDepartment = 0;
        double totalSalary = 0;

        #endregion

        #region Открытые методы

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
            GeneralDirector = new Supervisor(name, surname, age, salaryGeneralDirector, "Генеральный директор");
            AddWorkerToList(GeneralDirector);
        }

        /// <summary>
        /// Добавить главного бугалтера
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        public void AddChiefAccountant(string name, string surname, long age)
        {
            ChiefAccountant = new Supervisor(name, surname, age, salaryChiefAccountant, "Главный бугалтер");
            AddWorkerToList(ChiefAccountant);
        }

        /// <summary>
        /// Добавить заместителя директора
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        public void AddDeputyDirector(string name, string surname, long age)
        {
            DeputyDirector = new Supervisor(name, surname, age, salaryDeputyDirector, "Заместитель генерального директора");
            AddWorkerToList(DeputyDirector);
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
            var supervisor = new Supervisor(name, surname, age, salarySupervisorDepartment, jobTitle);
            AddWorkerToList(supervisor, pathToDepartment);
            return AddDeleteSupervisor(supervisor, pathToDepartment);
        }
        
        /// <summary>
        /// Удалить руководителя департамента
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        public bool DeleteSupervisorDepartment(string pathToDepartment)
        {
            var supervisor = GetSupervisorOfDepartment(pathToDepartment);

            if(supervisor != null)
            {
                DeleteWorkerFromList(supervisor, pathToDepartment);
            }
            
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
            var intern = new Intern(name, surname, age, salary, jobTitle);
            AddWorkerToList(intern, pathToDepartment);
            return AddWorker(intern, pathToDepartment);
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
            var intern = new Intern(name, surname, age, salary, jobTitle);
            DeleteWorkerFromList(intern, pathToDepartment);
            return DeleteWorker(intern, pathToDepartment);
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
            var employee = new Employee(name, surname, age, salary, jobTitle);
            AddWorkerToList(employee, pathToDepartment);
            return AddWorker(employee, pathToDepartment);
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
            var employee = new Employee(name, surname, age, salary, jobTitle);
            DeleteWorkerFromList(employee, pathToDepartment);
            return DeleteWorker(employee, pathToDepartment);
        }

        #endregion

        #region Закрытые методы

        /// <summary>
        /// Общая заработная плата всех сотрудников департамента
        /// </summary>
        /// <param name="department"> Департамент </param>
        /// <param name="key"> Ключ учитывать ли зарплату руководителя </param>        
        private double TotalSalaryOfAllEmployeesDepartment(Department department, bool key)
        {
            double sum = 0;

            if(department != null)
            {
                for (int j = 0; j < department.CountWorkers; j++)
                {
                    sum += department.Workers[j].Salary;
                }

                if(key && department.Supervisor != null)
                {
                    sum += department.Supervisor.Salary;
                }

                for (int i = 0; i < department.CountDepartments; i++)
                {
                    sum += TotalSalaryOfAllEmployeesDepartment(department.NextDepartments[i], true);
                }
            }

            return sum;
        }

        /// <summary>
        /// Добавить работника в лист
        /// </summary>
        /// <param name="worker"> Работник </param>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        private void AddWorkerToList(IWorker worker, string pathToDepartment = null)
        {
            if(workers == null)
            {
                workers = new BindingList<Worker>();
            }

            workers.Add(new Worker(worker, pathToDepartment));
        }

        /// <summary>
        /// Удалить работника из листа
        /// </summary>
        /// <param name="worker"> Работник </param>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        private void DeleteWorkerFromList(IWorker worker, string pathToDepartment = null)
        {
            if(workers != null)
            {
                var tempWorker = new Worker(worker, pathToDepartment);
                int k = -1;

                for(int i = 0; i < workers.Count; i++)
                {
                    if(workers[i] == tempWorker)
                    {
                        k = i;
                    }
                }

                if(k > -1)
                {
                    workers.RemoveAt(k);
                }
            }
        }

        /// <summary>
        /// Расчитать зарплату генерального директора
        /// </summary>
        /// <param name="minSalary"> Минимальная зарплата генерального директора </param>
        /// <param name="coefficient"> Коэффициент </param>
        private double СalculateSalaryGeneralDirector(double minSalary, double coefficient)
        {
            if(totalSalary == 0)
            {
                СalculateTotalSalary();
            }

            double sum = totalSalary;

            if(ChiefAccountant != null)
            {
                sum += ChiefAccountant.Salary;
            }

            if(DeputyDirector != null)
            {
                sum += DeputyDirector.Salary;
            }

            sum *= coefficient;

            if (sum > minSalary)
            {
                return sum;
            }

            return minSalary;
        }

        /// <summary>
        /// Расчитать зарплату главного бугалтера
        /// </summary>
        /// <param name="minSalary"> Минимальная зарплата главного бугалтера </param>
        /// <param name="coefficient"> Коэффициент </param>
        private double СalculateSalaryChiefAccountant(double minSalary, double coefficient)
        {
            if (totalSalary == 0)
            {
                СalculateTotalSalary();
            }

            double sum = totalSalary;

            sum *= coefficient;

            if (sum > minSalary)
            {
                return sum;
            }

            return minSalary;
        }

        /// <summary>
        /// Расчитать зарплату заместителя директора
        /// </summary>
        /// <param name="minSalary"> Минимальная зарплата заместителя директора </param>
        /// <param name="coefficient"> Коэффициент </param>
        private double СalculateSalaryDeputyDirector(double minSalary, double coefficient)
        {
            if (totalSalary == 0)
            {
                СalculateTotalSalary();
            }

            double sum = 0;

            if (Departments != null)
            {
                for (int i = 0; i < Departments.Count; i++)
                {
                    sum += TotalSalaryOfAllEmployeesDepartment(Departments[i], true);
                }
            }

            sum *= coefficient;

            if (sum > minSalary)
            {
                return sum;
            }

            return minSalary;
        }

        /// <summary>
        /// Расчитать зарплату руководителя департамента
        /// </summary>
        /// <param name="pathToDepartment"></param>
        /// <param name="minSalary"> Минимальная зарплата заместителя директора </param>        
        /// <param name="coefficient"> Коэффициент </param>
        private double СalculateSalarySupervisor(string pathToDepartment, double minSalary, double coefficient)
        {
            double sum = 0;

            if (!string.IsNullOrWhiteSpace(pathToDepartment))
            {
                Department department = GetDepartment(pathToDepartment);

                if (department != null)
                {
                    sum += TotalSalaryOfAllEmployeesDepartment(department, false);
                }
            }

            sum *= coefficient;

            if (sum > minSalary)
            {
                return sum;
            }

            return minSalary;
        }

        /// <summary>
        /// Расчитать суммарную зарплату всех сотрудников министерства
        /// </summary>
        private void СalculateTotalSalary()
        {
            if (Departments != null)
            {
                for (int i = 0; i < Departments.Count; i++)
                {
                    totalSalary += TotalSalaryOfAllEmployeesDepartment(Departments[i], true);
                }
            }            
        }

        private void СalculateSalary(string pathToDepartment)
        {
            
        }

        #endregion
    }
}
