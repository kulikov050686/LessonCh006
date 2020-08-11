using Models;
using System;
using Controllers;

namespace LessonCh006
{
    class Program
    {
        static void Main(string[] args)
        {

            Ministry ministry = new Ministry("Министерство");

            if (ministry.AddDepartment("Министерство", "Депортамент_1"))
            {
                Console.WriteLine("Департамент успешно добавлен!!!");
            }

            if (ministry.AddDepartment("Министерство", "Депортамент_2"))
            {
                Console.WriteLine("Департамент успешно добавлен!!!");
            }            

            if (ministry.AddDepartment("Министерство/Депортамент_2", "Депортамент_2_1"))
            {
                Console.WriteLine("Департамент успешно добавлен!!!");
            }

            if (ministry.AddDepartment("Министерство/Депортамент_2", "Депортамент_2_2"))
            {
                Console.WriteLine("Департамент успешно добавлен!!!");
            }

            if (ministry.AddDepartment("Министерство/Депортамент_2", "Депортамент_2_3"))
            {
                Console.WriteLine("Департамент успешно добавлен!!!");
            }

            if (ministry.AddDepartment("Министерство/Депортамент_2", "Депортамент_2_4"))
            {
                Console.WriteLine("Департамент успешно добавлен!!!");
            }

            //-------------------------------------------------------------------------------------------

            //if(ministry.DeleteDepartment("Министерство/Депортамент_2", "Депортамент_2_3"))
            //{
            //    Console.WriteLine("Департамент успешно удалён!!!");
            //}

            string name = "Павел";
            string surname = "Куликов";
            long age = 35;
            double salary = 150000;
            string jobTitle = "Руководитель департамента";

            if(ministry.AddSupervisor("Министерство/Депортамент_2", name, surname, age, salary, jobTitle))
            {
                Console.WriteLine("Руководитель успешно добавлен!!!");
            }            
        }
    }
}
