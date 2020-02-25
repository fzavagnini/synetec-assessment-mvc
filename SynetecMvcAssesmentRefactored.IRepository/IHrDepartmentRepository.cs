using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SynetecMvcAssesmentRefactored.Model.Data;

namespace SynetecMvcAssesmentRefactored.IRepository
{
    public interface IHrDepartmentRepository
    {
        IEnumerable<HrDepartment> GetHrDepartments();
    }
}