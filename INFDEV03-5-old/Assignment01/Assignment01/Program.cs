using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Assignment01
{
    class EmployeeLogic
    {
        // Add emoloyee
        // Delete employee
        // Modify employee

    }

    class InputHandler
    {

        // Make list with options
        // What do you want to do?
        // 1. Add Employee + address + education + job position
        // 2. Modify Employee + address + education + job position
        // 3. Delete Employee + address + education + job position
        // 4. Add Project + location
        // 5. Modify Project
        // 6. Delete Project
        // 7. Assign an employee to a project

        public InputHandler()
        {
            Console.WriteLine("InputHandler has started");
        }

        public void InsertEmployee(int bsn, string name, string surname) // also pass works in building
        {
            SqlConnection con;
            SqlDataReader reader;
            con = new SqlConnection(Properties.Settings.Default.connectionString);
            con.Open();

            string query = "INSERT INTO Employee (bsn, name, surname) VALUES (" +  "'" +bsn + "','" + name + "','" + surname + "')";

            reader = new SqlCommand(query, con).ExecuteReader();

        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            InputHandler handler;
            SqlConnection con;
            SqlDataReader reader;
            string query;

            con = new SqlConnection(Properties.Settings.Default.connectionString);

            con.Open();
            Console.WriteLine("Connection is open");

            handler = new InputHandler();

            Console.WriteLine("Fill in the BSN");
            int inputBsn = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Fill in the first name");
            string inputFstName = Console.ReadLine();
            Console.WriteLine("Fill in the surname");
            string inputSrName = Console.ReadLine();

            handler.InsertEmployee(inputBsn, inputFstName, inputSrName);
          

            // query test
            Console.WriteLine("Select key");
            string result = Console.ReadLine();

            query = "SELECT * FROM Employee WHERE bsn =" + result;


            // query result
            reader = new SqlCommand(query, con).ExecuteReader();
            
            while (reader.Read())
            {
                Console.WriteLine(reader.GetInt32(0));
            }

            Console.ReadKey();

        }
    }
}
