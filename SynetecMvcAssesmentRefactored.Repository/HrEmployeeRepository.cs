using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SynetecMvcAssesmentRefactored.IRepository;
using SynetecMvcAssesmentRefactored.Model.Data;

namespace SynetecMvcAssesmentRefactored.Repository
{
    public class HrEmployeeRepository : IHrEmployeeRepository
    {
        public IEnumerable<HrEmployee> GetHrEmployees()
        {
            List<HrEmployee> hrEmployees = new List<HrEmployee>();

            var connectionString = @"Server=localhost\SQLEXPRESS;Database=SynetecMvcDb;Trusted_Connection=True;";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM [dbo].[HrEmployee]";

            DataTable dt = new DataTable();

            dt.Load(cmd.ExecuteReader());

            foreach (DataRow row in dt.Rows)
            {
                var hrEmployee = new HrEmployee();
                hrEmployee.ID = Convert.ToInt32(row[0]);
                hrEmployee.FistName = Convert.ToString(row[1]);
                hrEmployee.SecondName = Convert.ToString(row[2]);
                hrEmployee.DateOfBirth = Convert.ToDateTime(row[3]);
                hrEmployee.HrDepartmentId = Convert.ToInt32(row[4]);
                hrEmployee.JobTitle = Convert.ToString(row[5]);
                hrEmployee.Salary = Convert.ToInt32(row[6]);
                hrEmployee.Full_Name = Convert.ToString(row[7]);

                hrEmployees.Add(hrEmployee);
            }

            conn.Close();

            return hrEmployees;
        }
    }
}