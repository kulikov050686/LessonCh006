using Controllers;

namespace LessonCh006
{
    class Program
    {
        static void Main(string[] args)
        {           
            Ministry ministry = new Ministry("ОАО Рога и Копыта");

            ministry.AddDepartment("Департамент_0");
            ministry.AddDepartment("Департамент_1");
            ministry.AddDepartment("Департамент_2");
            ministry.AddDepartment("Департамент_3");

            ministry.AddDepartment("Департамент_3/Департамент_3_0");
            ministry.AddDepartment("Департамент_3/Департамент_3_2");
            ministry.AddDepartment("Департамент_3/Департамент_3_3");

            ministry.AddDepartment("Департамент_3/Департамент_3_0/Департамент_3_0_0");
            ministry.AddDepartment("Департамент_3/Департамент_3_0/Департамент_3_0_1");

            ministry.AddDepartment("Департамент_4/Департамент_4_0/Департамент_4_0_0");
            ministry.AddDepartment("Департамент_4/Департамент_4_1/Департамент_4_1_1");

            ministry.AddGeneralDirector("Иван", "Иванов", 45);
        }
    }
}
