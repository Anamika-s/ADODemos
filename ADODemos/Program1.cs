using ADODemos.Models;
using System;
// Step 1
using System.Data.SqlClient;

namespace ADODemos
{
    class Program1
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
            //InsertEmployee(employee);
            //GetEmployees();
            GetEmployeeeDetailsById(112);
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


                using (SqlCommand command = new SqlCommand("GetEmployees", connection))
                {
                    //command.CommandText = "Select * from Employee ";
                    //command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
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
                    command.CommandText = "InsertEmpoyee";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@id", employee.Id);
                    command.Parameters.AddWithValue("@name", employee.Name);
                    command.Parameters.AddWithValue("@address", employee.Address);
                    command.Parameters.AddWithValue("@salary", employee.Salary);

                    SqlParameter p1 = new SqlParameter("@flag", System.Data.SqlDbType.Int);
                    p1.Direction = System.Data.ParameterDirection.ReturnValue;
                    command.Parameters.Add(p1);






                    connection.Open();
                    command.ExecuteNonQuery();
                    int flag = (int)command.Parameters["@flag"].Value;
                    if (flag == 1)
                        Console.WriteLine("Rec   inserted");
                    else
                        Console.WriteLine("This ID already exists");

                    connection.Close();


                }
            }

        }
        static void GetEmployeeeDetailsById(int id)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "GetDetailsById";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    SqlParameter p1 = new SqlParameter("@name", System.Data.SqlDbType.VarChar, 20);
                    SqlParameter p2 = new SqlParameter("@address", System.Data.SqlDbType.VarChar, 100);
                    SqlParameter p3 = new SqlParameter("@salary", System.Data.SqlDbType.Int);
                    SqlParameter p4 = new SqlParameter("@flag", System.Data.SqlDbType.Int);
                    command.Parameters.Add(p1);
                    command.Parameters.Add(p2);
                    command.Parameters.Add(p3);
                    command.Parameters.Add(p4);

                    p1.Direction = System.Data.ParameterDirection.Output;
                    p2.Direction = System.Data.ParameterDirection.Output;
                    p3.Direction = System.Data.ParameterDirection.Output;
                    p4.Direction = System.Data.ParameterDirection.ReturnValue;
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                    int flag = (int)command.Parameters["@flag"].Value;
                    if (flag == 1)
                    {
                        Console.WriteLine(command.Parameters["@name"].Value.ToString());
                        Console.WriteLine(command.Parameters["@address"].Value.ToString());
                        Console.WriteLine(command.Parameters["@salary"].Value.ToString());
                        connection.Close();
                    }
                    else
                        Console.WriteLine("No rec    exists with this ID");
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