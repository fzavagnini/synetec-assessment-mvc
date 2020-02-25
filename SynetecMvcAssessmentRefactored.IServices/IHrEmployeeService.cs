using System.Collections.Generic;
using SynetecMvcAssesmentRefactored.Model.Data;

namespace SynetecMvcAssessmentRefactored.IServices
{
    public interface IHrEmployeeService
    {
        List<HrEmployee> GetHrEmployees();
    }
}