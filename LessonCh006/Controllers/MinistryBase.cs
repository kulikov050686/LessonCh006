using System;
using System.ComponentModel;
using Models;

namespace Controllers
{
    /// <summary>
    /// Базовый класс Министерство
    /// </summary>
    public abstract class MinistryBase
    {
        #region Закрытые поля

        string nameMinistry;
        Department MainDepartment { get; set; }
        
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

                if(!CorrectnessOfName(value))
                {
                    throw new ArgumentNullException("Название министерства некорректно!!!");
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
        public MinistryBase(string nameMinistry)
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
                if(CorrectnessOfName(nameDepartment))
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
                if(CorrectnessOfName(nameDepartment))
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
        /// Добавить руководителя в департамент
        /// </summary>
        /// <param name="pathToDepartment"> Путь до депортамента </param>
        /// <param name="supervisor"> Руководитель департамента </param>        
        public bool AddSupervisor(string pathToDepartment, Supervisor supervisor)
        {            
            if (supervisor != null && !string.IsNullOrWhiteSpace(pathToDepartment))
            {
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
            if (!string.IsNullOrWhiteSpace(pathToDepartment))
            {
                if (DepartmentSearchAndDeleteSupervisor(pathToDepartment, MainDepartment))
                {
                    return true;
                }
            }            
            
            return false;
        }

        /// <summary>
        /// Добавить интерна в департамент
        /// </summary>
        /// <param name="pathToDepartment"> Путь до депортамента </param>
        /// <param name="intern"> Интерн </param>      
        public bool AddIntern(string pathToDepartment, Intern intern)
        {            
            if (intern != null && !string.IsNullOrWhiteSpace(pathToDepartment))
            {
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
        /// <param name="intern"> Интерн </param>        
        public bool DeleteIntern(string pathToDepartment, Intern intern)
        {
            if (intern != null && !string.IsNullOrWhiteSpace(pathToDepartment))
            {
                if (DepartmentSearchAndDeleteIntern(pathToDepartment, intern, MainDepartment))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Добавить сотрудника в департамент
        /// </summary>
        /// <param name="pathToDepartment"> Путь до депортамента </param>
        /// <param name="employee"> Сотрудник </param>        
        public bool AddEmployee(string pathToDepartment, Employee employee)
        {
            if (employee != null && !string.IsNullOrWhiteSpace(pathToDepartment))
            {
                if (DepartmentSearchAndAddingEmployee(pathToDepartment, employee, MainDepartment))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Удалить работника из департамента
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        /// <param name="employee"> Сотрудник </param>
        public bool DeleteEmployee(string pathToDepartment, Employee employee)
        {
            if (employee != null && !string.IsNullOrWhiteSpace(pathToDepartment))
            {
                if (DepartmentSearchAndDeleteEmployee(pathToDepartment, employee, MainDepartment))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Получить данные начальника департамента
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>        
        public Supervisor GetHeadOfDepartment(string pathToDepartment)
        {
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть путсым!!!");
            }

            return DepartmentSearchAndGetSupervisor(pathToDepartment, MainDepartment);
        }

        /// <summary>
        /// Получить данные интернов департамента
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>        
        public BindingList<Intern> GetInternsOfDepartment(string pathToDepartment)
        {
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть путсым!!!");
            }

            return DepartmentSearchAndGetInterns(pathToDepartment, MainDepartment);
        }

        /// <summary>
        /// Получить данные сотрудников департамента
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        public BindingList<Employee> GetEmployeesOfDepartment(string pathToDepartment)
        {
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть путсым!!!");
            }

            return DepartmentSearchAndGetEmployees(pathToDepartment, MainDepartment);
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
                return DepartmentSearchAndAdding(pathToParentDepartment, nameDepartment, department.NextDepartments[numberDepartments]);
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
                    bool p = false;

                    for(int i = 0; i < department.NextDepartments.Count; i++)
                    {
                        if(department.NextDepartments[i].NameDepartment == nameDepartment)
                        {
                            k = i;
                            p = true;
                            break;
                        }
                    }

                    if(p)
                    {
                        department.NextDepartments.RemoveAt(k);
                        return true;
                    }                    
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
                return DepartmentSearchAndDelete(pathToParentDepartment, nameDepartment, department.NextDepartments[numberDepartments]);
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
                return DepartmentSearchAndAddingSupervisor(pathToDepartment, supervisor, department.NextDepartments[numberDepartments]);
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
                return DepartmentSearchAndDeleteSupervisor(pathToDepartment, department.NextDepartments[numberDepartments]);
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
                return DepartmentSearchAndAddingIntern(pathToDepartment, intern, department.NextDepartments[numberDepartments]);
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
                    bool p = false;

                    for(int i = 0; i < department.Interns.Count; i++)
                    {
                        if(department.Interns[i] == intern)
                        {
                            k = i;
                            p = true;
                            break;
                        }
                    }

                    if(p)
                    {
                        department.Interns.RemoveAt(k);
                        return true;
                    }                    
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
                return DepartmentSearchAndDeleteIntern(pathToDepartment, intern, department.NextDepartments[numberDepartments]);
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
                return DepartmentSearchAndAddingEmployee(pathToDepartment, employee, department.NextDepartments[numberDepartments]);
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
                if (department.Employees != null)
                {
                    int k = 0;
                    bool p = false;

                    for(int i = 0; i < department.Employees.Count; i++)
                    {
                        if(department.Employees[i] == employee)
                        {
                            k = i;
                            p = true;
                            break;
                        }
                    }

                    if(p)
                    {
                        department.Employees.RemoveAt(k);
                        return true;
                    }                    
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
                return DepartmentSearchAndDeleteEmployee(pathToDepartment, employee, department.NextDepartments[numberDepartments]);
            }

            return false;
        }

        /// <summary>
        /// Поиск департамента и получение данных о руководителе
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        /// <param name="department"> Текущий департамент </param>        
        private Supervisor DepartmentSearchAndGetSupervisor(string pathToDepartment, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToDepartment.Length == 0)
            {                
                return department.Supervisor;
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
                return DepartmentSearchAndGetSupervisor(pathToDepartment, department.NextDepartments[numberDepartments]);
            }
            
            return null;
        }

        /// <summary>
        /// Поиск департамента и получение данных о интернах
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        /// <param name="department"> Текущий департамент </param>        
        private BindingList<Intern> DepartmentSearchAndGetInterns(string pathToDepartment, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToDepartment.Length == 0)
            {
                return department.Interns;
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
                return DepartmentSearchAndGetInterns(pathToDepartment, department.NextDepartments[numberDepartments]);
            }

            return null;
        }

        /// <summary>
        /// Поиск департамента и получение данных о сотрудниках
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        /// <param name="department"> Текущий департамент </param> 
        private BindingList<Employee> DepartmentSearchAndGetEmployees(string pathToDepartment, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToDepartment.Length == 0)
            {
                return department.Employees;
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
                return DepartmentSearchAndGetEmployees(pathToDepartment, department.NextDepartments[numberDepartments]);
            }

            return null;
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

        /// <summary>
        /// Корректность ввода названия
        /// </summary>
        /// <param name="name"> Название </param>        
        private bool CorrectnessOfName(string name)
        {
            for (int i = 0; i < name.Length; i++)
            {
                if (ForbiddenSymbol(name[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Запрещённый символ
        /// </summary>
        /// <param name="Symbol"> Символ </param>        
        private bool ForbiddenSymbol(char Symbol)
        {
            return Symbol == '/';                   
        }

        #endregion
    }
}
