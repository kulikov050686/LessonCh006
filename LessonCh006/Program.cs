using System;
using Controllers;

namespace LessonCh006
{
    public struct Man
    {
        public string name;
        public string surname;
        public long age;
        public double salary;
        public string jobTitle;
    }

    class Program
    {
        static void Main(string[] args)
        {           
            Ministry ministry = new Ministry("Министерство");
            

            ////-----------------------------------------------------------------------------------------

            //if (ministry.AddDepartment("Министерство", "Депортамент_1"))
            //{
            //    Console.WriteLine("Департамент успешно добавлен!!!");
            //}

            //if (ministry.AddDepartment("Министерство", "Депортамент_2"))
            //{
            //    Console.WriteLine("Департамент успешно добавлен!!!");
            //}            

            //if (ministry.AddDepartment("Министерство/Депортамент_2", "Депортамент_2_1"))
            //{
            //    Console.WriteLine("Департамент успешно добавлен!!!");
            //}

            //if (ministry.AddDepartment("Министерство/Депортамент_2", "Депортамент_2_2"))
            //{
            //    Console.WriteLine("Департамент успешно добавлен!!!");
            //}

            //if (ministry.AddDepartment("Министерство/Депортамент_2/Депортамент_2_2", "Депортамент_2_2_1"))
            //{
            //    Console.WriteLine("Департамент успешно добавлен!!!");
            //}

            //if (ministry.AddDepartment("Министерство/Депортамент_2/Депортамент_2_2", "Депортамент_2_2_2"))
            //{
            //    Console.WriteLine("Департамент успешно добавлен!!!");
            //}

            //if (ministry.AddDepartment("Министерство/Депортамент_2", "Депортамент_2_3"))
            //{
            //    Console.WriteLine("Департамент успешно добавлен!!!");
            //}

            //if (ministry.AddDepartment("Министерство/Депортамент_2", "Депортамент_2_4"))
            //{
            //    Console.WriteLine("Департамент успешно добавлен!!!");
            //}

            ////--------------------------------------------------------------------------------------------------------------------------

            //if (ministry.DeleteDepartment("Министерство/Депортамент_2", "Депортамент_2_3"))
            //{
            //    Console.WriteLine("Департамент успешно удалён!!!");
            //}

            //---------------------------------------------------------------------------------------------------------------------------

            //Man man1 = new Man();

            //man1.name = "Павел";
            //man1.surname = "Куликов";
            //man1.age = 35;
            //man1.salary = 150000;
            //man1.jobTitle = "Руководитель департамента";

            //if(ministry.AddSupervisor("Министерство/Депортамент_2", man1.name, man1.surname, man1.age, man1.salary, man1.jobTitle))
            //{
            //    Console.WriteLine("Руководитель успешно добавлен!!!");
            //}

            //Man man2 = new Man();

            //man2.name = "Иван";
            //man2.surname = "Иванов";
            //man2.age = 25;
            //man2.salary = 35000;
            //man2.jobTitle = "Помощник программиста";

            //if(ministry.AddIntern("Министерство/Депортамент_2", man2.name, man2.surname, man2.age, man2.salary, man2.jobTitle))
            //{
            //    Console.WriteLine("Интерн успешно добавлен!!!");
            //}

            //Man man3 = new Man();

            //man3.name = "Пётр";
            //man3.surname = "Петров";
            //man3.age = 29;
            //man3.salary = 47000;
            //man3.jobTitle = "Программист";

            //if (ministry.AddEmployee("Министерство/Депортамент_2", man3.name, man3.surname, man3.age, man3.salary, man3.jobTitle))
            //{
            //    Console.WriteLine("Сотрудник успешно добавлен!!!");
            //}

            //Man man4 = new Man();

            //man4.name = "Кирилл";
            //man4.surname = "Сумский";
            //man4.age = 45;
            //man4.salary = 190000;
            //man4.jobTitle = "Начальник депортамента";

            //if (ministry.AddSupervisor("Министерство/Депортамент_2/Депортамент_2_2", man4.name, man4.surname, man4.age, man4.salary, man4.jobTitle))
            //{
            //    Console.WriteLine("Руководитель успешно добавлен!!!");
            //}

            //Man man5 = new Man();

            //man5.name = "Михуил";
            //man5.surname = "Жопадрищев";
            //man5.age = 31;
            //man5.salary = 35000;
            //man5.jobTitle = "Разъебай";

            //if (ministry.AddIntern("Министерство/Депортамент_2/Депортамент_2_2", man5.name, man5.surname, man5.age, man5.salary, man5.jobTitle))
            //{
            //    Console.WriteLine("Интерн успешно добавлен!!!");
            //}

            //Man man6 = new Man();

            //man6.name = "Кукуцапль";
            //man6.surname = "Черезабораногузадирищенко";
            //man6.age = 33;
            //man6.salary = 67000;
            //man6.jobTitle = "Балабол";

            //if (ministry.AddEmployee("Министерство/Депортамент_2/Депортамент_2_2", man6.name, man6.surname, man6.age, man6.salary, man6.jobTitle))
            //{
            //    Console.WriteLine("Сотрудник успешно добавлен!!!");
            //}

            //-----------------------------------------------------------------------------------------------------------------------------------------------

        }
    }
}
