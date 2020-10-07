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

            company.AddDepartment("Департамент_0");
            company.AddDepartment("Департамент_1");
            company.AddDepartment("Департамент_2");
            company.AddDepartment("Департамент_3");

            company.AddDepartment("Департамент_3/Департамент_3_0");
            company.AddDepartment("Департамент_3/Департамент_3_1");

            company.AddDepartment("Департамент_3/Департамент_3_0/Департамент_3_0_0");
            company.AddDepartment("Департамент_3/Департамент_3_0/Департамент_3_0_1");

            #endregion

            #region Добавление Руководителей Высшего Звена

            company.AddGeneralDirector("Иван", "Иванов", 45);
            //company.AddChiefAccountant("Татьяна", "Сергеева", 47);
            //company.AddDeputyDirector("Валентин", "Петров", 43);

            #endregion

            //Console.ReadKey();
        }
    }
}
