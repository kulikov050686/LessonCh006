using Models;
using System;
using System.ComponentModel;

namespace Controllers
{
    public class Ministry : MinistryBase
    {
        #region Закрытые поля

        BindingList<Worker> workers;
        const double minSalary = 1300;
        double salaryGeneralDirector;
        double salaryChiefAccountant;
        double salaryDeputyDirector;        
        double totalSalary;

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор министерства
        /// </summary>
        /// <param name="nameMinistry"> Название министерства </param>
        public Ministry(string nameMinistry) : base(nameMinistry) 
        {
            salaryGeneralDirector = 0;
            salaryChiefAccountant = 0;
            salaryDeputyDirector = 0;
        }

        #endregion

        #region Открытые методы

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
            СalculateSalary();
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
            СalculateSalary();
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
            СalculateSalary();
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
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть путсым!!!");
            }

            var supervisor = new Supervisor(name, surname, age, minSalary, jobTitle);

            if(AddDeleteSupervisor(supervisor, pathToDepartment))
            {
                AddWorkerToList(supervisor, pathToDepartment);
                СalculateSalary(pathToDepartment);
                return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// Удалить руководителя департамента
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        public bool DeleteSupervisorDepartment(string pathToDepartment)
        {
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть путсым!!!");
            }

            var supervisor = GetSupervisorOfDepartment(pathToDepartment);

            if(supervisor != null)
            {
                DeleteWorkerFromList(supervisor, pathToDepartment);
            }

            if(AddDeleteSupervisor(null, pathToDepartment))
            {
                СalculateSalary(pathToDepartment);
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
        public bool AddIntern(string name, string surname, long age, double salary, string jobTitle, string pathToDepartment)
        {
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть путсым!!!");
            }

            var intern = new Intern(name, surname, age, salary, jobTitle);

            if(AddWorker(intern, pathToDepartment))
            {
                AddWorkerToList(intern, pathToDepartment);
                СalculateSalary(pathToDepartment);
                return true;
            }

            return false; 
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
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть путсым!!!");
            }

            var intern = new Intern(name, surname, age, salary, jobTitle);

            if(DeleteWorker(intern, pathToDepartment))
            {
                DeleteWorkerFromList(intern, pathToDepartment);
                СalculateSalary(pathToDepartment);
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
        public bool AddEmployee(string name, string surname, long age, double salary, string jobTitle, string pathToDepartment)
        {
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть путсым!!!");
            }

            var employee = new Employee(name, surname, age, salary, jobTitle);

            if(AddWorker(employee, pathToDepartment))
            {
                AddWorkerToList(employee, pathToDepartment);
                СalculateSalary(pathToDepartment);
                return true;
            }
            
            return false;
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
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть путсым!!!");
            }

            var employee = new Employee(name, surname, age, salary, jobTitle);

            if(DeleteWorker(employee, pathToDepartment))
            {
                DeleteWorkerFromList(employee, pathToDepartment);
                СalculateSalary(pathToDepartment);
                return true;
            }

            return false;
        }

        #endregion

        #region Закрытые методы

        /// <summary>
        /// Общая заработная плата всех сотрудников департамента и поддепартаментов
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
        /// Расчитать зарплату руководителя департамента
        /// </summary>
        /// <param name="pathToDepartment"></param>
        /// <param name="minSalary"> Минимальная зарплата заместителя директора </param>        
        /// <param name="coefficient"> Коэффициент </param>
        private void СalculateSalarySupervisors(string pathToDepartment)
        {
            Department department = GetDepartment(pathToDepartment);

            if(department != null)
            {
                double salarySupervisorDepartment = 0.15 * TotalSalaryOfAllEmployeesDepartment(department, false);
                Supervisor supervisor = GetSupervisorOfDepartment(pathToDepartment);

                if (supervisor != null)
                {                    
                    if (salarySupervisorDepartment > minSalary)
                    {
                        supervisor.Salary = salarySupervisorDepartment;
                    }
                }

                pathToDepartment = ShortenPath(pathToDepartment);

                if (pathToDepartment.Length != 0)
                {
                    СalculateSalarySupervisors(pathToDepartment);
                }
            }            
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

        /// <summary>
        /// Расчёт зарплат руководителей
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        private void СalculateSalary(string pathToDepartment = null)
        {
            if(!string.IsNullOrWhiteSpace(pathToDepartment))
            {
                СalculateSalarySupervisors(pathToDepartment);
            }

            СalculateTotalSalary();

            if(DeputyDirector != null)
            {
                salaryDeputyDirector = 0.15 * totalSalary;

                if(salaryDeputyDirector <= minSalary)
                {
                    DeputyDirector.Salary = minSalary;
                }
                else
                {
                    DeputyDirector.Salary = salaryDeputyDirector;
                }
            }
            else
            {
                salaryDeputyDirector = 0;
            }

            if(ChiefAccountant != null)
            {
                salaryChiefAccountant = 0.15 * totalSalary;

                if(salaryChiefAccountant <= minSalary)
                {
                    ChiefAccountant.Salary = minSalary;
                }
                else
                {
                    ChiefAccountant.Salary = salaryChiefAccountant;
                }
            }
            else
            {
                salaryChiefAccountant = 0;
            }
            
            if(GeneralDirector != null)
            {
                salaryGeneralDirector = 0.15 * (salaryChiefAccountant + salaryDeputyDirector + totalSalary);

                if(salaryGeneralDirector <= minSalary)
                {
                    GeneralDirector.Salary = minSalary;
                }
                else
                {
                    GeneralDirector.Salary = salaryGeneralDirector;
                }
            }
        }

        /// <summary>
        /// Сократить путь с конца
        /// </summary>
        /// <param name="path"> Путь к родителю </param>
        private string ShortenPath(string path)
        {
            if(path[path.Length - 1] == '/')
            {
                path = path.Substring(0, path.Length - 1);
            }

            int temp = 0;

            for(int i = path.Length - 1; i >= 0; i--)
            {
                if(path[i] != '/')
                {
                    temp++;
                }
                else
                {
                    break;
                }
            }

            if (temp == path.Length)
            {
                return "";
            }

            return path.Substring(0, path.Length - (++temp));
        }

        #endregion
    }
}
