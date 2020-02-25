using System.Collections.Generic;
using System.Linq;
using SynetecMvcAssesmentRefactored.IRepository;
using SynetecMvcAssesmentRefactored.Model.Data;
using SynetecMvcAssessmentRefactored.IServices;

namespace SynetecMvcAssessmentRefactored.Services
{
    public class HrEmployeeService : IHrEmployeeService
    {
        private readonly IHrEmployeeRepository _hrEmployeeRepository;

        public HrEmployeeService(IHrEmployeeRepository hrEmployeeRepository)
        {
            _hrEmployeeRepository = hrEmployeeRepository;
        }
        public List<HrEmployee> GetHrEmployees()
        {
            return _hrEmployeeRepository.GetHrEmployees().ToList();
        }
    }
}