using Models;
using System.Collections.Generic;

namespace Controllers
{
    public class Ministry : MinistryBase
    {
        #region Закрытые поля

        List<Worker> ministryStaff;

        #endregion

        #region Открытые поля

        #endregion

        #region Конструктор

        public Ministry(string nameMinistry) : base(nameMinistry) {}

        #endregion

        #region Открытые методы

        /// <summary>
        /// Добавить руководителя в департамент
        /// </summary>
        /// <param name="pathToDepartment"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="age"></param>
        /// <param name="jobTitle"></param>        
        public bool AddSupervisor(string pathToDepartment, string name, string surname, long age, string jobTitle)
        {
            Supervisor supervisor = new Supervisor(name, surname, age, 0, jobTitle);

            if(base.AddSupervisor(pathToDepartment, supervisor))
            {                
                return true;
            }

            return false;
        }        

        #endregion

        #region Закрытые методы
               

        #endregion
    }
}
