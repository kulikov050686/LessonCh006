using System;
using System.ComponentModel;
using Models;
using Services;

namespace Controllers
{
    /// <summary>
    /// Министерство
    /// </summary>
    public class Ministry
    {
        #region Закрытые поля

        string nameMinistry;
        Department MainDepartment;
        
        #endregion

        #region Открытые поля

        /// <summary>
        /// Название министерства
        /// </summary>
        public string NameMinistry 
        {
            get => nameMinistry;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Название министерства не может быть пустым!!!");
                }

                nameMinistry = value;
            }
        }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор министерства
        /// </summary>
        /// <param name="nameMinistry"> Название министерства </param>
        public Ministry(string nameMinistry)
        {
            NameMinistry = nameMinistry;            
            MainDepartment = new Department(NameMinistry);           
        }

        #endregion

        #region Открытые методы

        /// <summary>
        /// Добавить департамент
        /// </summary>
        /// <param name="pathToParentDepartment"> Путь до родительского департамента </param>
        /// <param name="nameDepartment"> Название добавляемого поддепартамента </param>        
        public bool AddDepartment(string pathToParentDepartment, string nameDepartment)
        {
            if(!string.IsNullOrWhiteSpace(pathToParentDepartment) && !string.IsNullOrWhiteSpace(nameDepartment))
            {
                if(Checks.CorrectnessOfName(nameDepartment))
                {
                    if(DepartmentSearchAndAdding(pathToParentDepartment, nameDepartment, MainDepartment))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Удалить департамент
        /// </summary>
        /// <param name="pathToParentDepartment"> Путь до родительского департамента </param>
        /// <param name="nameDepartment"> Название удаляемого поддепартамента </param>        
        public bool DeleteDepartment(string pathToParentDepartment, string nameDepartment)
        {
            if(!string.IsNullOrWhiteSpace(pathToParentDepartment) && !string.IsNullOrWhiteSpace(nameDepartment))
            {
                if(Checks.CorrectnessOfName(nameDepartment))
                {
                    if(DepartmentSearchAndDelete(pathToParentDepartment, nameDepartment, MainDepartment))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Добавить руководителя в депортамент
        /// </summary>
        /// <param name="pathToDepartment"> Путь до депортамента </param>
        /// <param name="nameSupervisor"> Имя </param>
        /// <param name="surnameSupervisor"> Фамилия </param>
        /// <param name="ageSupervisor"> Возраст </param>
        /// <param name="salarySupervisor"> Зарплата </param>
        /// <param name="jobTitleSupervisor"> Название должности </param>        
        public bool AddSupervisor(string pathToDepartment, string nameSupervisor, string surnameSupervisor, long ageSupervisor, double salarySupervisor, string jobTitleSupervisor)
        {
            if (!string.IsNullOrWhiteSpace(pathToDepartment) && 
                !string.IsNullOrWhiteSpace(nameSupervisor) && 
                !string.IsNullOrWhiteSpace(surnameSupervisor) && 
                ( 18 < ageSupervisor && ageSupervisor < 99) && 
                !string.IsNullOrWhiteSpace(jobTitleSupervisor))
            {
                Supervisor supervisor = new Supervisor(nameSupervisor, surnameSupervisor, ageSupervisor, salarySupervisor, jobTitleSupervisor);

                if(DepartmentSearchAndAddingSupervisor(pathToDepartment, supervisor, MainDepartment))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Удалить руководителя департамента
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>        
        public bool DeleteSupervisor(string pathToDepartment)
        {
            if(!string.IsNullOrWhiteSpace(pathToDepartment))
            {
                if (DepartmentSearchAndDeleteSupervisor(pathToDepartment, MainDepartment))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Добавить интерна в депортамент
        /// </summary>
        /// <param name="pathToDepartment"> Путь до депортамента </param>
        /// <param name="nameIntern"> Имя </param>
        /// <param name="surnameIntern"> Фамилия </param>
        /// <param name="ageIntern"> Возраст </param>
        /// <param name="salaryIntern"> Зарплата </param>
        /// <param name="jobTitleIntern"> Название должности </param>        
        public bool AddIntern(string pathToDepartment, string nameIntern, string surnameIntern, long ageIntern, double salaryIntern, string jobTitleIntern)
        {
            if (!string.IsNullOrWhiteSpace(pathToDepartment) &&
                !string.IsNullOrWhiteSpace(nameIntern) &&
                !string.IsNullOrWhiteSpace(surnameIntern) &&
                (18 < ageIntern && ageIntern < 99) &&
                !string.IsNullOrWhiteSpace(jobTitleIntern))
            {
                Intern intern = new Intern(nameIntern, surnameIntern, ageIntern, salaryIntern, jobTitleIntern);
                
                if (DepartmentSearchAndAddingIntern(pathToDepartment, intern, MainDepartment))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Удалить интерна из департамента
        /// </summary>
        /// <param name="pathToDepartment"> Путь до депортамента </param>
        /// <param name="nameIntern"> Имя </param>
        /// <param name="surnameIntern"> Фамилия </param>
        /// <param name="ageIntern"> Возраст </param>
        /// <param name="salaryIntern"> Зарплата </param>
        /// <param name="jobTitleIntern"> Название должности </param>               
        public bool DeleteIntern(string pathToDepartment, string nameIntern, string surnameIntern, long ageIntern, double salaryIntern, string jobTitleIntern)
        {
            if (!string.IsNullOrWhiteSpace(pathToDepartment) &&
                !string.IsNullOrWhiteSpace(nameIntern) &&
                !string.IsNullOrWhiteSpace(surnameIntern) &&
                (18 < ageIntern && ageIntern < 99) &&
                !string.IsNullOrWhiteSpace(jobTitleIntern))
            {
                Intern intern = new Intern(nameIntern, surnameIntern, ageIntern, salaryIntern, jobTitleIntern);

                if (DepartmentSearchAndDeleteIntern(pathToDepartment, intern, MainDepartment))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Добавить работника в депортамент
        /// </summary>
        /// <param name="pathToDepartment"> Путь до депортамента </param>
        /// <param name="nameEmployee"> Имя </param>
        /// <param name="surnameEmployee"> Фамилия </param>
        /// <param name="ageEmployee"> Возраст </param>
        /// <param name="salaryEmployee"> Зарплата </param>
        /// <param name="jobTitleEmployee"> Название должности </param>        
        public bool AddEmployee(string pathToDepartment, string nameEmployee, string surnameEmployee, long ageEmployee, double salaryEmployee, string jobTitleEmployee)
        {
            if (!string.IsNullOrWhiteSpace(pathToDepartment) &&
                !string.IsNullOrWhiteSpace(nameEmployee) &&
                !string.IsNullOrWhiteSpace(surnameEmployee) &&
                (18 < ageEmployee && ageEmployee < 99) &&
                !string.IsNullOrWhiteSpace(jobTitleEmployee))
            {
                Employee employee = new Employee(nameEmployee, surnameEmployee, ageEmployee, salaryEmployee, jobTitleEmployee);
                
                if (DepartmentSearchAndAddingEmployee(pathToDepartment, employee, MainDepartment))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Удалить работника из депортамента
        /// </summary>
        /// <param name="pathToDepartment"> Путь до депортамента </param>
        /// <param name="nameEmployee"> Имя </param>
        /// <param name="surnameEmployee"> Фамилия </param>
        /// <param name="ageEmployee"> Возраст </param>
        /// <param name="salaryEmployee"> Зарплата </param>
        /// <param name="jobTitleEmployee"> Название должности </param>  
        public bool DeleteEmployee(string pathToDepartment, string nameEmployee, string surnameEmployee, long ageEmployee, double salaryEmployee, string jobTitleEmployee)
        {
            if (!string.IsNullOrWhiteSpace(pathToDepartment) &&
                !string.IsNullOrWhiteSpace(nameEmployee) &&
                !string.IsNullOrWhiteSpace(surnameEmployee) &&
                (18 < ageEmployee && ageEmployee < 99) &&
                !string.IsNullOrWhiteSpace(jobTitleEmployee))
            {
                Employee employee = new Employee(nameEmployee, surnameEmployee, ageEmployee, salaryEmployee, jobTitleEmployee);

                if (DepartmentSearchAndDeleteEmployee(pathToDepartment, employee, MainDepartment))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Закрытые методы

        /// <summary>
        /// Поиск родительского департамента и добавление поддепартамента
        /// </summary>
        /// <param name="pathToParentDepartment"> Путь до родительского департамента </param>
        /// <param name="nameDepartment"> Название добавляемого поддепартамента </param>
        /// <param name="department"> Родительский департамент </param>        
        private bool DepartmentSearchAndAdding(string pathToParentDepartment, string nameDepartment, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToParentDepartment);
            pathToParentDepartment = ShortenPath(pathToParentDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToParentDepartment.Length == 0) 
            {
                if(department.NextDepartments == null)
                {
                    department.NextDepartments = new BindingList<Department>();
                }

                Department NewDepartment = new Department(nameDepartment);
                department.NextDepartments.Add(NewDepartment);
                return true;
            }

            NameParentDepartment = NameOfCurrentDepartment(pathToParentDepartment);
            bool key = false;
            int numberDepartments = 0;

            for (int i = 0; i < department.NextDepartments.Count; i++)
            {
                string temp = department.NextDepartments[i].NameDepartment;

                if(temp == NameParentDepartment)
                {
                    key = true;
                    numberDepartments = i;
                    break;
                }
            }

            if(key)
            {
                return DepartmentSearchAndAdding(NameParentDepartment, nameDepartment, department.NextDepartments[numberDepartments]);
            }

            return false;
        }

        /// <summary>
        /// Поиск родительского департамента и удаление поддепартамента
        /// </summary>
        /// <param name="pathToParentDepartment"> Путь до родительского департамента </param>
        /// <param name="nameDepartment"> Название удаляемого поддепартамента </param>
        /// <param name="department"> Родительский департамент </param>
        private bool DepartmentSearchAndDelete(string pathToParentDepartment, string nameDepartment, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToParentDepartment);
            pathToParentDepartment = ShortenPath(pathToParentDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToParentDepartment.Length == 0)
            {               
                if(department.NextDepartments != null)
                {
                    int k = 0;

                    for(int i = 0; i < department.NextDepartments.Count; i++)
                    {
                        if(department.NextDepartments[i].NameDepartment == nameDepartment)
                        {
                            k = i;
                            break;
                        }
                    }

                    department.NextDepartments.RemoveAt(k);
                    return true;
                }

                return false;
            }

            NameParentDepartment = NameOfCurrentDepartment(pathToParentDepartment);
            bool key = false;
            int numberDepartments = 0;

            for (int i = 0; i < department.NextDepartments.Count; i++)
            {
                string temp = department.NextDepartments[i].NameDepartment;

                if (temp == NameParentDepartment)
                {
                    key = true;
                    numberDepartments = i;
                    break;
                }
            }

            if (key)
            {
                return DepartmentSearchAndDelete(NameParentDepartment, nameDepartment, department.NextDepartments[numberDepartments]);
            }

            return false;
        }

        /// <summary>
        /// Поиск департамента и добавление руководителя
        /// </summary>
        /// <param name="pathTotDepartment"> Путь до департамента </param>        
        /// <param name="supervisor"> Руководитель </param>        
        /// <param name="department"> Текущий департамент </param>        
        private bool DepartmentSearchAndAddingSupervisor(string pathToDepartment, Supervisor supervisor, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToDepartment.Length == 0)
            {
                department.Supervisor = supervisor;
                return true;
            }

            NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            bool key = false;
            int numberDepartments = 0;

            for (int i = 0; i < department.NextDepartments.Count; i++)
            {
                string temp = department.NextDepartments[i].NameDepartment;

                if (temp == NameParentDepartment)
                {
                    key = true;
                    numberDepartments = i;
                    break;
                }
            }

            if (key)
            {
                return DepartmentSearchAndAddingSupervisor(NameParentDepartment, supervisor, department.NextDepartments[numberDepartments]);
            }

            return false;
        }

        /// <summary>
        /// Поиск департамента и удаление руководителя
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        /// <param name="department"> Текущий департамент </param>        
        private bool DepartmentSearchAndDeleteSupervisor(string pathToDepartment, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToDepartment.Length == 0)
            {
                department.Supervisor = null;
                return true;
            }

            NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            bool key = false;
            int numberDepartments = 0;

            for (int i = 0; i < department.NextDepartments.Count; i++)
            {
                string temp = department.NextDepartments[i].NameDepartment;

                if (temp == NameParentDepartment)
                {
                    key = true;
                    numberDepartments = i;
                    break;
                }
            }

            if (key)
            {
                return DepartmentSearchAndDeleteSupervisor(NameParentDepartment, department.NextDepartments[numberDepartments]);
            }

            return false;
        }

        /// <summary>
        /// Поиск департамента и добавление интерна
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        /// <param name="intern"> Интерн </param>
        /// <param name="department"> Текущий департамент </param>        
        private bool DepartmentSearchAndAddingIntern(string pathToDepartment, Intern intern, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToDepartment.Length == 0)
            {
                if(department.Interns == null)
                {
                    department.Interns = new BindingList<Intern>();
                }

                department.Interns.Add(intern);
                return true;
            }

            NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            bool key = false;
            int numberDepartments = 0;

            for (int i = 0; i < department.NextDepartments.Count; i++)
            {
                string temp = department.NextDepartments[i].NameDepartment;

                if (temp == NameParentDepartment)
                {
                    key = true;
                    numberDepartments = i;
                    break;
                }
            }

            if (key)
            {
                return DepartmentSearchAndAddingIntern(NameParentDepartment, intern, department.NextDepartments[numberDepartments]);
            }

            return false;
        }

        /// <summary>
        /// Поиск департамента и удаление интерна
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        /// <param name="intern"> Интерн </param>
        /// <param name="department"> Текущий департамент </param>
        private bool DepartmentSearchAndDeleteIntern(string pathToDepartment, Intern intern, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToDepartment.Length == 0)
            {
                if (department.Interns != null)
                {
                    int k = 0;

                    for(int i = 0; i < department.Interns.Count; i++)
                    {
                        if(department.Interns[i] == intern)
                        {
                            k = i;
                            break;
                        }
                    }

                    department.Interns.RemoveAt(k);
                    return true;
                }

                return false;
            }

            NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            bool key = false;
            int numberDepartments = 0;

            for (int i = 0; i < department.NextDepartments.Count; i++)
            {
                string temp = department.NextDepartments[i].NameDepartment;

                if (temp == NameParentDepartment)
                {
                    key = true;
                    numberDepartments = i;
                    break;
                }
            }

            if (key)
            {
                return DepartmentSearchAndDeleteIntern(NameParentDepartment, intern, department.NextDepartments[numberDepartments]);
            }

            return false;
        }

        /// <summary>
        /// Поиск департамента и добавление сотрудника
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        /// <param name="employee"> Сотрудник </param>
        /// <param name="department"> Текущий департамент </param>        
        private bool DepartmentSearchAndAddingEmployee(string pathToDepartment, Employee employee, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToDepartment.Length == 0)
            {
                if (department.Employees == null)
                {
                    department.Employees = new BindingList<Employee>();
                }

                department.Employees.Add(employee);
                return true;
            }

            NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            bool key = false;
            int numberDepartments = 0;

            for (int i = 0; i < department.NextDepartments.Count; i++)
            {
                string temp = department.NextDepartments[i].NameDepartment;

                if (temp == NameParentDepartment)
                {
                    key = true;
                    numberDepartments = i;
                    break;
                }
            }

            if (key)
            {
                return DepartmentSearchAndAddingEmployee(NameParentDepartment, employee, department.NextDepartments[numberDepartments]);
            }

            return false;
        }

        /// <summary>
        /// Поиск департамента и удаление сотрудника
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        /// <param name="employee"> Сотрудник </param>
        /// <param name="department"> Текущий департамент </param>        
        private bool DepartmentSearchAndDeleteEmployee(string pathToDepartment, Employee employee, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToDepartment.Length == 0)
            {
                if (department.Employees == null)
                {
                    int k = 0;

                    for(int i = 0; i < department.Employees.Count; i++)
                    {
                        if(department.Employees[i] == employee)
                        {
                            k = i;
                            break;
                        }
                    }

                    department.Employees.RemoveAt(k);
                    return true;
                }

                return false;
            }

            NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            bool key = false;
            int numberDepartments = 0;

            for (int i = 0; i < department.NextDepartments.Count; i++)
            {
                string temp = department.NextDepartments[i].NameDepartment;

                if (temp == NameParentDepartment)
                {
                    key = true;
                    numberDepartments = i;
                    break;
                }
            }

            if (key)
            {
                return DepartmentSearchAndDeleteEmployee(NameParentDepartment, employee, department.NextDepartments[numberDepartments]);
            }

            return false;
        }

        /// <summary>
        /// Сократить путь
        /// </summary>
        /// <param name="pathToParent"> Путь к родителю </param>        
        private string ShortenPath(string pathToParent)
        {
            int temp = 0;

            for(int i = 0; i < pathToParent.Length; i++)
            {
                if(pathToParent[i] != '/')
                {                    
                    temp++;
                }
                else
                {
                    break;
                }
            }

            if(temp == pathToParent.Length)
            {
                return "";
            }

            return pathToParent.Substring(++temp);
        }

        /// <summary>
        /// Название текущего департамента
        /// </summary>
        /// <param name="pathDepartment"> Путь до департамента </param>        
        private string NameOfCurrentDepartment(string pathToParentDepartment)
        {
            string temp = "";

            for (int i = 0; i < pathToParentDepartment.Length; i++)
            {
                if (pathToParentDepartment[i] != '/')
                {
                    temp += pathToParentDepartment[i];
                }
                else
                {
                    break;
                }
            }

            return temp;
        }

        #endregion
    }
}
