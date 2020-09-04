﻿using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Controllers
{
    public class Ministry : MinistryBase
    {
        #region Закрытые поля
               
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
        /// Добавить департамент
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        public new void AddDepartment(string pathToDepartment)
        {
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть пустым!!!");
            }

            base.AddDepartment(pathToDepartment);
        }

        /// <summary>
        /// Удалить департамент
        /// </summary>             
        /// <param name="pathToDepartment"> Путь до родительского департамента </param>
        public new bool DeleteDepartment(string pathToDepartment)
        {
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть пустым!!!");
            }

            if(base.DeleteDepartment(pathToDepartment))
            {
                pathToDepartment = ShortenPath(pathToDepartment);
                СalculateSalary(pathToDepartment);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Добавить генерального директора
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        public void AddGeneralDirector(string name, string surname, long age)
        {
            GeneralDirector = new Supervisor(name, surname, age, minSalary, "Генеральный директор", EmployeePosition.GeneralDirector);            
            СalculateSalary();            
        }

        /// <summary>
        /// Удалить генерального директора
        /// </summary>
        public void DeleteGeneralDirector()
        {
            if(GeneralDirector != null)
            {                
                salaryGeneralDirector = 0;
                GeneralDirector = null;
            }
        }

        /// <summary>
        /// Добавить главного бухгалтера
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        public void AddChiefAccountant(string name, string surname, long age)
        {
            ChiefAccountant = new Supervisor(name, surname, age, minSalary, "Главный бухгалтер", EmployeePosition.ChiefAccountant);            
            СalculateSalary();            
        }

        /// <summary>
        /// Удалить главного бухгалтера
        /// </summary>
        public void DeleteChiefAccountant()
        {
            if(ChiefAccountant != null)
            {                
                ChiefAccountant = null;
                СalculateSalary();
            }
        }

        /// <summary>
        /// Добавить заместителя директора
        /// </summary>
        /// <param name="name"> Имя </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="age"> Возраст </param>
        public void AddDeputyDirector(string name, string surname, long age)
        {
            DeputyDirector = new Supervisor(name, surname, age, minSalary, "Заместитель генерального директора", EmployeePosition.DeputyDirector);            
            СalculateSalary();                      
        }

        /// <summary>
        /// Удалить заместителя генерального директора
        /// </summary>
        public void DeleteDeputyDirector()
        {
            if(DeputyDirector != null)
            {                
                DeputyDirector = null;
                СalculateSalary();
            }
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
                throw new ArgumentNullException("Путь до департамента не может быть пустым!!!");
            }

            var supervisor = new Supervisor(name, surname, age, minSalary, jobTitle);

            if(AddDeleteSupervisor(supervisor, pathToDepartment))
            {
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
                throw new ArgumentNullException("Путь до департамента не может быть пустым!!!");
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
                throw new ArgumentNullException("Путь до департамента не может быть пустым!!!");
            }

            var intern = new Intern(name, surname, age, salary, jobTitle);

            if(AddWorker(intern, pathToDepartment))
            {
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
                throw new ArgumentNullException("Путь до департамента не может быть пустым!!!");
            }

            var intern = new Intern(name, surname, age, salary, jobTitle);

            if(DeleteWorker(intern, pathToDepartment))
            {                
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
                throw new ArgumentNullException("Путь до департамента не может быть пустым!!!");
            }

            var employee = new Employee(name, surname, age, salary, jobTitle);

            if(AddWorker(employee, pathToDepartment))
            {
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
                throw new ArgumentNullException("Путь до департамента не может быть пустым!!!");
            }

            var employee = new Employee(name, surname, age, salary, jobTitle);

            if(DeleteWorker(employee, pathToDepartment))
            {                
                СalculateSalary(pathToDepartment);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Получить список всех работников
        /// </summary>        
        public BindingList<Worker> GetListOfAllWorkers()
        {
            List<Worker> workers = new List<Worker>();

            if(GeneralDirector != null)
            {
                workers.Add(new Worker(GeneralDirector, null));
            }

            if(ChiefAccountant != null)
            {
                workers.Add(new Worker(ChiefAccountant, null));
            }

            if(DeputyDirector != null)
            {
                workers.Add(new Worker(DeputyDirector, null));
            }

            if(Departments != null)
            {
                List<Worker> temp;

                for(int i = 0; i < Departments.Count; i++)
                {
                    temp = ListOfAllWorkers(Departments[i], Departments[i].NameDepartment);

                    if (temp.Count != 0)
                    {
                        workers.AddRange(temp);
                    }                                        
                }
            }

            if(workers.Count != 0)
            {
                BindingList<Worker> blworkers = new BindingList<Worker>();

                for(int i = 0; i < workers.Count; i++)
                {
                    blworkers.Add(workers[i]);
                }

                return blworkers;
            }

            return null;
        }

        /// <summary>
        /// Устанавить список всех работников 
        /// </summary>
        /// <param name="workers"> Список работников </param>
        public bool SetListOfAllWorkers(BindingList<Worker> workers)
        {
            if(workers != null)
            {
                bool key = true;

                for(int i = 0; i < workers.Count && key; i++)
                {
                    if(string.IsNullOrWhiteSpace(workers[i].PathToDepartment))
                    {
                        key = false;

                        if(workers[i].EmployeePosition == EmployeePosition.GeneralDirector)
                        {
                            GeneralDirector = new Supervisor(workers[i].Name, workers[i].Surname, workers[i].Age, workers[i].Salary, workers[i].JobTitle, workers[i].EmployeePosition);
                            key = true;
                        }

                        if(workers[i].EmployeePosition == EmployeePosition.ChiefAccountant)
                        {
                            ChiefAccountant = new Supervisor(workers[i].Name, workers[i].Surname, workers[i].Age, workers[i].Salary, workers[i].JobTitle, workers[i].EmployeePosition);
                            key = true;
                        }

                        if(workers[i].EmployeePosition == EmployeePosition.DeputyDirector)
                        {
                            DeputyDirector = new Supervisor(workers[i].Name, workers[i].Surname, workers[i].Age, workers[i].Salary, workers[i].JobTitle, workers[i].EmployeePosition);
                            key = true;
                        }
                    }
                    else
                    {
                        string pathToDepartment = workers[i].PathToDepartment;
                        AddDepartment(pathToDepartment);

                        if(workers[i].EmployeePosition == EmployeePosition.Intern)
                        {
                            var intern = new Intern(workers[i].Name, workers[i].Surname, workers[i].Age, workers[i].Salary, workers[i].JobTitle);
                            key = AddWorker(intern, pathToDepartment);
                        }

                        if(workers[i].EmployeePosition == EmployeePosition.Employee)
                        {
                            var employee = new Employee(workers[i].Name, workers[i].Surname, workers[i].Age, workers[i].Salary, workers[i].JobTitle);
                            key = AddWorker(employee, pathToDepartment);
                        }

                        if(workers[i].EmployeePosition == EmployeePosition.Supervisor)
                        {
                            var supervisor = new Supervisor(workers[i].Name, workers[i].Surname, workers[i].Age, workers[i].Salary, workers[i].JobTitle);
                            key = AddDeleteSupervisor(supervisor, pathToDepartment);
                        }
                    }
                }

                return key;
            }

            return false;
        }

        /// <summary>
        /// Получить список всех работников департамента
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>        
        public BindingList<Worker> GetListOfAllWorkersDepartment(string pathToDepartment)
        {
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть пустым!!!");
            }

            Department department = GetDepartment(pathToDepartment);

            if(department != null)
            {
                BindingList<Worker> temp;

                if(department.CountWorkers != 0)
                {
                    temp = new BindingList<Worker>();

                    for(int i = 0; i < department.CountWorkers; i++)
                    {
                        temp.Add(new Worker(department.Workers[i], pathToDepartment));
                    }

                    if (department.Supervisor != null)
                    {
                        temp.Add(new Worker(department.Supervisor, pathToDepartment));
                    }

                    return temp;
                }                
            }

            return null;
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

            if (department != null)
            {
                for (int j = 0; j < department.CountWorkers; j++)
                {
                    sum += department.Workers[j].Salary;
                }

                if (key && department.Supervisor != null)
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
                    else
                    {
                        supervisor.Salary = minSalary;
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
                totalSalary = 0;

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

                if(salaryDeputyDirector > minSalary)
                {
                    DeputyDirector.Salary = salaryDeputyDirector;
                }
                else
                {
                    DeputyDirector.Salary = minSalary;
                }
            }
            else
            {
                salaryDeputyDirector = 0;
            }

            if(ChiefAccountant != null)
            {
                salaryChiefAccountant = 0.15 * totalSalary;

                if(salaryChiefAccountant > minSalary)
                {
                    ChiefAccountant.Salary = salaryChiefAccountant;
                }
                else
                {
                    ChiefAccountant.Salary = minSalary;
                }
            }
            else
            {
                salaryChiefAccountant = 0;
            }
            
            if(GeneralDirector != null)
            {
                salaryGeneralDirector = 0.15 * (salaryChiefAccountant + salaryDeputyDirector + totalSalary);

                if(salaryGeneralDirector > minSalary)
                {
                    GeneralDirector.Salary = salaryGeneralDirector;
                }
                else
                {
                    GeneralDirector.Salary = minSalary;
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

        /// <summary>
        /// Лист всех работников
        /// </summary>
        /// <param name="department"> Департамент </param>
        /// <param name="nameDepartment"> Имя текущего департамента </param>        
        private List<Worker> ListOfAllWorkers(Department department, string nameDepartment)
        {
            List<Worker> workers = new List<Worker>();

            if (department.NextDepartments != null)
            {
                List<Worker> temp;

                for (int i = 0; i < department.CountDepartments; i++)
                {
                    temp = ListOfAllWorkers(department.NextDepartments[i], nameDepartment + "/" + department.NextDepartments[i].NameDepartment);

                    if (temp.Count != 0)
                    {
                        workers.AddRange(temp);
                    }                    
                }

                if (department.Supervisor != null)
                {
                    workers.Add(new Worker(department.Supervisor, nameDepartment));
                }

                if (department.CountWorkers != 0)
                {
                    for (int j = 0; j < department.CountWorkers; j++)
                    {
                        workers.Add(new Worker(department.Workers[j], nameDepartment));
                    }
                }
            }
            else
            {
                if (department.Supervisor != null)
                {
                    workers.Add(new Worker(department.Supervisor, nameDepartment));
                }

                if (department.CountWorkers != 0)
                {
                    for (int i = 0; i < department.CountWorkers; i++)
                    {
                        workers.Add(new Worker(department.Workers[i], nameDepartment));
                    }
                }
            }

            return workers;
        }

        #endregion
    }
}
