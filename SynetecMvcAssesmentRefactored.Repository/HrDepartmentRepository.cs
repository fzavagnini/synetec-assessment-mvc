using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SynetecMvcAssesmentRefactored.IRepository;
using SynetecMvcAssesmentRefactored.Model.Data;

namespace SynetecMvcAssesmentRefactored.Repository
{
    public class HrDepartmentRepository : IHrDepartmentRepository
    {
        public IEnumerable<HrDepartment> GetHrDepartments()
        {
            List<HrDepartment> hrDepartments = new List<HrDepartment>();

            var connectionString = @"Server=localhost\SQLEXPRESS;Database=SynetecMvcDb;Trusted_Connection=True;";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM [dbo].[HrDepartment]";

            DataTable dt = new DataTable();

            dt.Load(cmd.ExecuteReader());

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            
            foreach (DataRow row in dt.Rows)
            {
                var hrDepartment = new HrDepartment();
                hrDepartment.ID = Convert.ToInt32(row[0]);
                hrDepartment.Title = Convert.ToString(row[1]);
                hrDepartment.Description = Convert.ToString(row[2]);
                hrDepartment.BonusPoolAllocationPerc = Convert.ToInt32(row[3]);
                hrDepartments.Add(hrDepartment);
            }

            conn.Close();

            return hrDepartments;
        }
    }
}
