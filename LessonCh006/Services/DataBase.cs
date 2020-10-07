using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using Models;
using System.Windows;

namespace Services
{
    public class DataBase
    {
        string connectionString;
        SqlConnection sqlConnection;
        SqlDataReader sqlDataReader;
        SqlCommand sqlCommand;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="connection"> Строка подключения </param>
        public DataBase(string connection)
        {
            connectionString = connection;
        }

        /// <summary>
        /// Получить список работников из базы данных
        /// </summary>       
        public BindingList<Worker> GetListWorkersFromDataBase()
        {
            BindingList<Worker> workers = new BindingList<Worker>();
            sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            sqlCommand = new SqlCommand("SELECT * FROM [WorkerTable]", sqlConnection);

            try
            {
                sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    string name = Convert.ToString(sqlDataReader["Name"]);
                    string surname = Convert.ToString(sqlDataReader["Surname"]);
                    long age = Convert.ToInt64(sqlDataReader["Age"]);
                    double salary = Convert.ToDouble(sqlDataReader["Salary"]);
                    string jobTitle = Convert.ToString(sqlDataReader["JobTitle"]);
                    EmployeePosition employeePosition = (EmployeePosition)Convert.ToUInt32(sqlDataReader["EmployeePosition"]);
                    string pathToDepartment = Convert.ToString(sqlDataReader["PathToDepartment"]);

                    workers.Add(new Worker(name, surname, age, salary, jobTitle, employeePosition, pathToDepartment));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }

            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }

            if (workers.Count == 0)
            {
                return null;
            }

            return workers;
        }        

        /// <summary>
        /// Добавить работника в базу данных
        /// </summary>
        /// <param name="worker"> Работник </param>        
        public async void AddWorkerToDataBase(Worker worker)
        {
            if(worker != null)
            {
                sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                sqlCommand = new SqlCommand("INSERT INTO [WorkerTable] (Name, Surname, Age)VALUES(@Name, @Surname, @Age)", sqlConnection);

                sqlCommand.Parameters.AddWithValue("Name", worker.Name);
                sqlCommand.Parameters.AddWithValue("Surname", worker.Surname);
                sqlCommand.Parameters.AddWithValue("Age", worker.Age);
                //sqlCommand.Parameters.AddWithValue("Salary", worker.Salary);
                //sqlCommand.Parameters.AddWithValue("JobTitle", worker.JobTitle);
                //sqlCommand.Parameters.AddWithValue("EmployeePosition", (int)worker.EmployeePosition);
                //sqlCommand.Parameters.AddWithValue("PathToDepartment", worker.PathToDepartment);

                await sqlCommand.ExecuteNonQueryAsync();

                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }
            }            
        }               

        /// <summary>
        /// Удалить работника из базы данных
        /// </summary>
        /// <param name="worker"> Работник </param>
        public async void DeleteWorkerFromDataBase(int id)
        {
            if (id > 0)
            {
                sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();

                sqlCommand = new SqlCommand("DELETE FROM [WorkerTable] WHERE [Id]=@Id", sqlConnection);

                sqlCommand.Parameters.AddWithValue("Id", id);

                await sqlCommand.ExecuteNonQueryAsync();

                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
