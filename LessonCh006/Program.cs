using Controllers;
using Models;
using System;
using System.ComponentModel;

namespace LessonCh006
{
    class Program
    {
        static void Print(Company company)
        {
            BindingList<Worker> workers = company.GetListOfAllWorkers();

            if(workers != null)
            {
                for (int i = 0; i < workers.Count; i++)
                {
                    Console.WriteLine($"{workers[i].Name,20} {workers[i].Surname,20} {workers[i].Age,5} {workers[i].Salary,15} {workers[i].JobTitle,45} {workers[i].PathToDepartment}");
                    Console.WriteLine();
                }

                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------------------");
            }            
        }
        
        static void Main(string[] args)
        {
            Company company = new Company("ОАО Рога и Копыта");

            #region Создание департаментов 

            //company.AddDepartment("Департамент_0");
            //company.AddDepartment("Департамент_1");
            //company.AddDepartment("Департамент_2");
            //company.AddDepartment("Департамент_3");
            //company.AddDepartment("Департамент_4"); 
            //company.AddDepartment("Департамент_5");

            //company.AddDepartment("Департамент_3/Департамент_3_0");
            //company.AddDepartment("Департамент_3/Департамент_3_1");

            //company.AddDepartment("Департамент_3/Департамент_3_0/Департамент_3_0_0");
            //company.AddDepartment("Департамент_3/Департамент_3_0/Департамент_3_0_1");



            #endregion

            #region Добавление Руководителей Высшего Звена

            //company.AddGeneralDirector("Иван", "Иванов", 45);
            //company.AddChiefAccountant("Татьяна", "Сергеева", 47);
            //company.AddDeputyDirector("Валентин", "Петров", 43);

            #endregion

            #region Добавление работников

            //company.AddSupervisorDepartment("Юрий", "Мелькин", 45, "Начальник департамента", "Департамент_3/Департамент_3_0");

            //company.AddIntern("Иван", "Петров", 25, 500, "Помощник программиста", "Департамент_3/Департамент_3_0");
            //company.AddIntern("Михаил", "Иванов", 26, 500, "Помощник программиста", "Департамент_3/Департамент_3_0");
            //company.AddIntern("Кирилл", "Сидоров", 27, 500, "Помощник программиста", "Департамент_3/Департамент_3_0");
            //company.AddIntern("Игорь", "Матросов", 28, 500, "Помощник программиста", "Департамент_3/Департамент_3_0");
            //company.AddIntern("Иван", "Кажедуб", 29, 500, "Помощник программиста", "Департамент_3/Департамент_3_0");

            //company.AddSupervisorDepartment("Игорь", "Максимов", 45, "Начальник департамента", "Департамент_3/Департамент_3_0/Департамент_3_0_0");

            //company.AddEmployee("Алексей", "Пивоваров", 35, 1000, "Программист", "Департамент_0");
            //company.AddEmployee("Исламбек", "Казаев", 39, 1000, "Программист", "Департамент_0");
            //company.AddEmployee("Павел", "Петренко", 36, 1000, "Программист", "Департамент_3/Департамент_3_0/Департамент_3_0_0");
            //company.AddEmployee("Семён", "Самохин", 37, 1000, "Программист", "Департамент_3/Департамент_3_0/Департамент_3_0_0");
            //company.AddEmployee("Аримат", "Кузбеков", 38, 1000, "Программист", "Департамент_3/Департамент_3_0/Департамент_3_0_0");

            #endregion

            #region Удаление работников

            //company.DeleteEmployee(0, "Исламбек", "Казаев", 39, 1000, "Программист", "Департамент_0");
            //company.DeleteEmployee(0, "Семён", "Самохин", 37, 1000, "Программист", "Департамент_3/Департамент_3_0/Департамент_3_0_0");

            #endregion

            Console.ReadKey();
        }
    }
}
