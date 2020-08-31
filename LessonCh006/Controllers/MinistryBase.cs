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

        private string nameMinistry;        
        
        #endregion

        #region Открытые поля

        /// <summary>
        /// Название министерства
        /// </summary>
        public string NameMinistry
        {
            get => nameMinistry;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Название министерства не может быть пустым!!!");
                }

                nameMinistry = value;
            }
        }

        /// <summary>
        /// Список департаментов первого уровня
        /// </summary>
        protected BindingList<Department> Departments { get; private set; }

        /// <summary>
        /// Генеральный директор
        /// </summary>
        protected Supervisor GeneralDirector { get; set; }

        /// <summary>
        /// Главный бугалтер
        /// </summary>
        protected Supervisor ChiefAccountant { get; set; }

        /// <summary>
        /// Заместитель генерального директора
        /// </summary>
        protected Supervisor DeputyDirector { get; set; }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор министерства
        /// </summary>
        /// <param name="nameMinistry"> Название министерства </param>
        public MinistryBase(string nameMinistry)
        {
            NameMinistry = nameMinistry;           
        }

        #endregion

        #region Открытые методы
        
        /// <summary>
        /// Добавить департамент
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>        
        public void AddDepartment(string pathToDepartment)
        {
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть путсым!!!");
            }

            string ParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (Departments == null)
            {
                if(pathToDepartment.Length == 0)
                {
                    Departments = new BindingList<Department>();                    
                    Departments.Add(new Department(ParentDepartment));
                    return;                    
                }
                else
                {
                    Departments = new BindingList<Department>();
                    Departments.Add(new Department(ParentDepartment));
                }                
            }
            else
            {
                if(pathToDepartment.Length == 0)
                {
                    if (SearchNumber(ParentDepartment) == -1)
                    {
                        Departments.Add(new Department(ParentDepartment));
                        return;                        
                    }

                    return;
                }
                else
                {
                    if (SearchNumber(ParentDepartment) == -1)
                    {
                        Departments.Add(new Department(ParentDepartment));                        
                    }
                }                 
            }

            DepartmentSearchAndAdding(pathToDepartment, Departments[SearchNumber(ParentDepartment)]);
        }

        /// <summary>
        /// Удалить департамент
        /// </summary>
        /// <param name="nameDepartment"> Название удаляемого поддепартамента </param>        
        /// <param name="pathToParentDepartment"> Путь до родительского департамента </param>
        public bool DeleteDepartment(string pathToDepartment)
        {
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть путсым!!!");
            }

            if (Departments != null)
            {
                string ParentDepartment = NameOfCurrentDepartment(pathToDepartment);
                pathToDepartment = ShortenPath(pathToDepartment);
                int numberDepartments = SearchNumber(ParentDepartment);

                if (pathToDepartment.Length == 0)
                {
                    if (numberDepartments > -1)
                    {
                        Departments.RemoveAt(numberDepartments);
                        return true;
                    }
                }
                else 
                {
                    if(numberDepartments > -1)
                    {                        
                        return DepartmentSearchAndDelete(pathToDepartment, Departments[numberDepartments]);
                    }
                }
            }            

            return false;
        }

        /// <summary>
        /// Добавить или удалить руководителя в департамент
        /// </summary>
        /// <param name="supervisor"> Руководитель департамента </param>
        /// <param name="pathToDepartment"> Путь до депортамента </param>
        protected bool AddDeleteSupervisor(Supervisor supervisor, string pathToDepartment)
        {            
            if(!string.IsNullOrWhiteSpace(pathToDepartment))
            {
                string NameDepartment = NameOfCurrentDepartment(pathToDepartment);
                int numberDepartments = SearchNumber(NameDepartment);

                if(numberDepartments > -1)
                {
                    if (ShortenPath(pathToDepartment).Length == 0)
                    {
                        Departments[numberDepartments].Supervisor = supervisor;
                        return true;                        
                    }
                    else
                    {
                        if (DepartmentSearchAndAddingDeleteSupervisor(pathToDepartment, supervisor, Departments[numberDepartments]))
                        {
                            return true;
                        }                        
                    }
                }                              
            }

            return false;
        }       

        /// <summary>
        /// Добавить работника в департамент
        /// </summary>
        /// <param name="pathToDepartment"> Путь до депортамента </param>
        /// <param name="worker"> Работник </param>      
        protected bool AddWorker(IWorker worker, string pathToDepartment)
        {            
            if (worker != null && !string.IsNullOrWhiteSpace(pathToDepartment))
            {
                string NameDepartment = NameOfCurrentDepartment(pathToDepartment);
                int numberDepartments = SearchNumber(NameDepartment);

                if (numberDepartments > -1) 
                {
                    if (ShortenPath(pathToDepartment).Length == 0)
                    {
                        if (Departments[numberDepartments].Workers == null)
                        {
                            Departments[numberDepartments].Workers = new BindingList<IWorker>();
                        }

                        Departments[numberDepartments].Workers.Add(worker);
                        return true;
                    }
                    else
                    {
                        if (DepartmentSearchAndAddingWorker(pathToDepartment, worker, Departments[numberDepartments]))
                        {
                            return true;
                        }
                    }
                }                                
            }

            return false;
        }

        /// <summary>
        /// Удалить работника из департамента
        /// </summary>
        /// <param name="pathToDepartment"> Путь до депортамента </param>
        /// <param name="worker"> Работник </param>        
        protected bool DeleteWorker(IWorker worker, string pathToDepartment)
        {
            if (worker != null && !string.IsNullOrWhiteSpace(pathToDepartment))
            {
                string NameDepartment = NameOfCurrentDepartment(pathToDepartment);
                int numberDepartments = SearchNumber(NameDepartment);

                if(numberDepartments > -1)
                {
                    if (ShortenPath(pathToDepartment).Length == 0)
                    {
                        if (Departments[numberDepartments].Workers != null)
                        {
                            int numberIntern = -1;

                            for (int i = 0; i < Departments[numberDepartments].Workers.Count; i++)
                            {
                                if(Departments[numberDepartments].Workers[i] == worker)
                                {
                                    numberIntern = i;
                                }
                            }

                            if(numberIntern > -1)
                            {
                                Departments[numberDepartments].Workers.RemoveAt(numberIntern);
                                return true;
                            }                            
                        }
                    }
                    else
                    {
                        if (DepartmentSearchAndDeleteWorker(pathToDepartment, worker, Departments[numberDepartments]))
                        {
                            return true;
                        }
                    }
                }               
            }

            return false;
        }

        /// <summary>
        /// Получить данные дапартамента
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>        
        protected Department GetDepartment(string pathToDepartment)
        {
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть пустым!!!");
            }

            string NameDepartment = NameOfCurrentDepartment(pathToDepartment);
            int numberDepartments = SearchNumber(NameDepartment);

            if (numberDepartments > -1)
            {
                if (ShortenPath(pathToDepartment).Length == 0)
                {
                    return Departments[numberDepartments];
                }
                else
                {
                    return DepartmentSearchAndGet(pathToDepartment, Departments[numberDepartments]);
                }
            }

            return null;
        }

        /// <summary>
        /// Получить данные руководителя департамента
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>        
        protected Supervisor GetSupervisorOfDepartment(string pathToDepartment)
        {
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть пустым!!!");
            }

            string NameDepartment = NameOfCurrentDepartment(pathToDepartment);
            int numberDepartments = SearchNumber(NameDepartment);

            if (numberDepartments > -1)
            {
                if (ShortenPath(pathToDepartment).Length == 0)
                {
                    return Departments[numberDepartments].Supervisor;
                }
                else
                {
                    return DepartmentSearchAndGetSupervisor(pathToDepartment, Departments[numberDepartments]);
                }
            }

            return null;
        }

        /// <summary>
        /// Получить данные работников департамента
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>        
        protected BindingList<IWorker> GetWorkersOfDepartment(string pathToDepartment)
        {
            if (string.IsNullOrWhiteSpace(pathToDepartment))
            {
                throw new ArgumentNullException("Путь до департамента не может быть пустым!!!");
            }

            string NameDepartment = NameOfCurrentDepartment(pathToDepartment);
            int numberDepartments = SearchNumber(NameDepartment);

            if (numberDepartments > -1)
            {
                if (ShortenPath(pathToDepartment).Length == 0)
                {
                    return Departments[numberDepartments].Workers;
                }
                else
                {
                    return DepartmentSearchAndGetWorkers(pathToDepartment, Departments[numberDepartments]);
                }
            }

            return null;            
        }

        #endregion

        #region Закрытые методы
               
        /// <summary>
        /// Поиск родительского департамента и добавление поддепартамента
        /// </summary>
        /// <param name="pathToParentDepartment"> Путь до родительского департамента </param>
        /// <param name="nameDepartment"> Название добавляемого поддепартамента </param>
        /// <param name="department"> Родительский департамент </param>        
        private void DepartmentSearchAndAdding(string pathToDepartment, Department department)
        {
            string ParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (department.NextDepartments == null)
            {
                if (pathToDepartment.Length == 0)
                {
                    department.NextDepartments = new BindingList<Department>();
                    department.NextDepartments.Add(new Department(ParentDepartment));
                }
                else
                {
                    department.NextDepartments = new BindingList<Department>();
                    department.NextDepartments.Add(new Department(ParentDepartment));
                    DepartmentSearchAndAdding(pathToDepartment, department.NextDepartments[0]);
                }
            }
            else
            {
                int numberDepartments = SearchNumber(ParentDepartment, department);

                if (pathToDepartment.Length == 0)
                {
                    if (numberDepartments == -1)
                    {
                        department.NextDepartments.Add(new Department(ParentDepartment));
                    }
                }
                else
                {
                    if (numberDepartments == -1)
                    {
                        department.NextDepartments.Add(new Department(ParentDepartment));
                        numberDepartments = SearchNumber(ParentDepartment, department);
                        DepartmentSearchAndAdding(pathToDepartment, department.NextDepartments[numberDepartments]);
                    }
                    else
                    {
                        DepartmentSearchAndAdding(pathToDepartment, department.NextDepartments[numberDepartments]);
                    }
                }
            }            
        }

        /// <summary>
        /// Поиск родительского департамента и удаление поддепартамента
        /// </summary>
        /// <param name="pathToParentDepartment"> Путь до родительского департамента </param>
        /// <param name="nameDepartment"> Название удаляемого поддепартамента </param>
        /// <param name="department"> Родительский департамент </param>
        private bool DepartmentSearchAndDelete(string pathToDepartment, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            int numberDepartments = SearchNumber(NameParentDepartment, department);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (numberDepartments > -1)
            {
                if(pathToDepartment.Length != 0)
                {
                    return DepartmentSearchAndDelete(pathToDepartment, department.NextDepartments[numberDepartments]);
                }
                else
                {
                    if (department.NextDepartments != null)
                    {
                        if (numberDepartments > -1)
                        {
                            department.NextDepartments.RemoveAt(numberDepartments);
                            return true;
                        }
                    }
                }                
            }

            return false;
        }

        /// <summary>
        /// Поиск департамента и добавление руководителя
        /// </summary>
        /// <param name="pathTotDepartment"> Путь до департамента </param>        
        /// <param name="supervisor"> Руководитель </param>        
        /// <param name="department"> Текущий департамент </param>        
        private bool DepartmentSearchAndAddingDeleteSupervisor(string pathToDepartment, Supervisor supervisor, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToDepartment.Length == 0)
            {
                department.Supervisor = supervisor;
                return true;
            }

            NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);            
            int numberDepartments = SearchNumber(NameParentDepartment, department);
            
            if (numberDepartments > -1)
            {
                return DepartmentSearchAndAddingDeleteSupervisor(pathToDepartment, supervisor, department.NextDepartments[numberDepartments]);
            }

            return false;
        }
                
        /// <summary>
        /// Поиск департамента и добавление интерна
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        /// <param name="intern"> Интерн </param>
        /// <param name="department"> Текущий департамент </param>        
        private bool DepartmentSearchAndAddingWorker(string pathToDepartment, IWorker worker, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToDepartment.Length == 0)
            {
                if(department.Workers == null)
                {
                    department.Workers = new BindingList<IWorker>();
                }

                department.Workers.Add(worker);
                return true;
            }

            NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);            
            int numberDepartments = SearchNumber(NameParentDepartment, department);

            if (numberDepartments > -1)
            {
                return DepartmentSearchAndAddingWorker(pathToDepartment, worker, department.NextDepartments[numberDepartments]);
            }

            return false;
        }

        /// <summary>
        /// Поиск департамента и удаление работника
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        /// <param name="worker"> Работник </param>
        /// <param name="department"> Текущий департамент </param>
        private bool DepartmentSearchAndDeleteWorker(string pathToDepartment, IWorker worker, Department department)
        {            
            string NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToDepartment.Length == 0)
            {
                if (department.Workers != null)
                {
                    int k = -1;                   

                    for(int i = 0; i < department.Workers.Count; i++)
                    {
                        if(department.Workers[i] == worker)
                        {
                            k = i;                           
                            break;
                        }
                    }

                    if(k > -1)
                    {
                        department.Workers.RemoveAt(k);
                        return true;
                    }                    
                }

                return false;
            }

            NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            int numberDepartments = SearchNumber(NameParentDepartment, department);

            if (numberDepartments > -1)
            {
                return DepartmentSearchAndDeleteWorker(pathToDepartment, worker, department.NextDepartments[numberDepartments]);
            }

            return false;
        }        

        /// <summary>
        /// Поиск департамента и получение данных о нём
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        /// <param name="department"> Текущий департамент </param>
        private Department DepartmentSearchAndGet(string pathToDepartment, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToDepartment.Length == 0)
            {
                return department;
            }

            NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);            
            int numberDepartments = SearchNumber(NameParentDepartment, department);

            if (numberDepartments > -1)
            {
                return DepartmentSearchAndGet(pathToDepartment, department.NextDepartments[numberDepartments]);
            }

            return null;
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
            int numberDepartments = SearchNumber(NameParentDepartment, department);

            if (numberDepartments > -1)
            {
                return DepartmentSearchAndGetSupervisor(pathToDepartment, department.NextDepartments[numberDepartments]);
            }
            
            return null;
        }

        /// <summary>
        /// Поиск департамента и получение данных о работниках
        /// </summary>
        /// <param name="pathToDepartment"> Путь до департамента </param>
        /// <param name="department"> Текущий департамент </param>        
        private BindingList<IWorker> DepartmentSearchAndGetWorkers(string pathToDepartment, Department department)
        {
            string NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);
            pathToDepartment = ShortenPath(pathToDepartment);

            if (department.NameDepartment == NameParentDepartment && pathToDepartment.Length == 0)
            {
                return department.Workers;
            }

            NameParentDepartment = NameOfCurrentDepartment(pathToDepartment);            
            int numberDepartments = SearchNumber(NameParentDepartment, department);
            
            if (numberDepartments > -1)
            {
                return DepartmentSearchAndGetWorkers(pathToDepartment, department.NextDepartments[numberDepartments]);
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
        /// Поиск номера вхождения департамента в основной список
        /// </summary>
        /// <param name="nameDepartment"> Название департамента </param>        
        private int SearchNumber(string nameDepartment)
        {
            for (int i = 0; i < Departments.Count; i++)
            {
                if (Departments[i].NameDepartment == nameDepartment)
                {
                    return i;                   
                }
            }

            return -1;
        }

        /// <summary>
        /// Поиск номера вхождения поддепартамента в департамент
        /// </summary>
        /// <param name="nameDepartment"> Название поддепартамента </param>
        /// <param name="department"> Департамент </param>        
        private int SearchNumber(string nameDepartment, Department department)
        {
            for (int i = 0; i < department.NextDepartments.Count; i++)
            {
                if (department.NextDepartments[i].NameDepartment == nameDepartment)
                {
                    return i;
                }
            }

            return -1;        
        }

        #endregion
    }
}
