using Microsoft.EntityFrameworkCore;
using SynetecMvcAssesmentRefactored.Model.Data;
using System;
using System.Collections.Generic;

namespace SynetecMvcAssesmentRefactored.IRepository
{
    public interface IHrEmployeeRepository
    {
        IEnumerable<HrEmployee> GetHrEmployees();
    }
}
