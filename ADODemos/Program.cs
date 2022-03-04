using ADODemos.Models;
using System;
// Step 1
using System.Data.SqlClient;

namespace ADODemos
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 2
            //GetConnection();
            //string connectionString = @"server=adminvm\SQLEXPRESS;initial catalog=practicedb;integrated security=true";

            Console.WriteLine("Enter ID");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("ENter Name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Address");
            string address = Console.ReadLine();
            Console.WriteLine("Enter Salary");
            int salary = Convert.ToInt32(Console.ReadLine());
            Employee employee = new Employee()
            {
                Id = id,
                Name = name,
                Address = address,
                Salary = salary
            };
            InsertEmployee(employee);
            GetEmployees();
        }

        private static string GetConnection()
        {
            string connectionString = @"server=adminvm\SQLEXPRESS;initial catalog=practicedb;user id=sa;password=pass@123";
            return connectionString;
        }

        private static void GetEmployees()
        {
            using (SqlConnection connection = new SqlConnection(GetConnection()))
            {
                //SqlCommand command = new SqlCommand();
                //command.CommandText = "Select * from Employee ";
                //command.Connection = connection;


                using (SqlCommand command = new SqlCommand("Select * from Employee ", connection))
                {
                    //command.CommandText = "Select * from Employee ";
                    //command.Connection = connection;

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            //Console.WriteLine(reader[0] + " " + reader[1]);
                            Console.WriteLine(reader["id"].ToString() + "  " + reader["name"]);

                        }
                    }
                    else
                        Console.WriteLine("No Records");
                    connection.Close();
                    //command.Dispose();
                    //connection.Dispose();
                }

            }
        }


        static void InsertEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "Insert into Employee (id, name , address,salary) values (@id, @name, @address, @salary)";
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@id", employee.Id);
                    command.Parameters.AddWithValue("@name", employee.Name);
                    command.Parameters.AddWithValue("@address", employee.Address);
                    command.Parameters.AddWithValue("@salary", employee.Salary);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();


                }
            }
        }


        static void EditEmployee(int id, Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "Update Employee set address=@address, salary = @salary where id=@id";
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@id", id);
                   
                    command.Parameters.AddWithValue("@address", employee.Address);
                    command.Parameters.AddWithValue("@salary", employee.Salary);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();


                }
            }
        }


        static void DeletetEmployee(int id)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "Delete Employee where id=@id";
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();


                }
            }
        }
    }
}