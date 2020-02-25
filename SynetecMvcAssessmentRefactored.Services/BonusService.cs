using System;
using System.Collections.Generic;
using System.Linq;
using SynetecMvcAssesmentRefactored.IRepository;
using SynetecMvcAssesmentRefactored.Model.Core;
using SynetecMvcAssesmentRefactored.Model.Data;
using SynetecMvcAssessmentRefactored.IServices;

namespace SynetecMvcAssessmentRefactored.Services
{
    public class BonusService : IBonusService
    {
        private readonly IHrEmployeeRepository _hrEmployeeRepository;
        private readonly IHrDepartmentRepository _hrDepartmentRepository;

        public BonusService(IHrEmployeeRepository hrEmployeeRepository, IHrDepartmentRepository hrDepartmentRepository)
        {
            _hrEmployeeRepository = hrEmployeeRepository;
            _hrDepartmentRepository = hrDepartmentRepository;
        }
        public BonusPoolCalculatorResultModel CalculateEmployeeBonus(BonusPoolCalculatorModel bonusPoolCalculatorModel)
        {
            var hrDepartments = _hrDepartmentRepository.GetHrDepartments();
            var hrEmployees = _hrEmployeeRepository.GetHrEmployees().ToList();

            var hrEmployeeSelected = hrEmployees.FirstOrDefault(x => x.ID == bonusPoolCalculatorModel.SelectedEmployeeId);

            var bonusesByDepartment =
                ProcessGlobalHrDepartmentBonusDistribution(bonusPoolCalculatorModel, hrDepartments);

            var employeesOfHrDepartment = hrEmployees.Where(x => x.HrDepartmentId == hrEmployeeSelected?.HrDepartmentId).ToList();

            var bonusPercentageBasedOnSalary = ProcessGlobalHrDepartmentEmployeePercentage(employeesOfHrDepartment);

            var bonusOfDepartment = (decimal)bonusesByDepartment.FirstOrDefault(x => x.Key == hrEmployeeSelected?.HrDepartmentId).Value;

            var actualBonusesForEmployeesInHrDepartment = GetActualBonusForHrDepartmentEmployees(
                employeesOfHrDepartment,
                bonusPercentageBasedOnSalary,
                bonusOfDepartment);

            var actualBonus = actualBonusesForEmployeesInHrDepartment[hrEmployeeSelected.ID];

            return new BonusPoolCalculatorResultModel() {BonusPoolAllocation = Convert.ToInt32(actualBonus), HrEmployee = hrEmployeeSelected };
        }

        public Dictionary<int, decimal> GetActualBonusForHrDepartmentEmployees(
            List<HrEmployee> hrDepartmentEmployees,
            List<Tuple<int, string, decimal>> globalHrDepartmentPercentages,
            decimal totalBonusAvailableForHrDepartment)
        {
            var actualBonusesDict = new Dictionary<int, decimal>();

            foreach (var hrDepartmentEmployee in hrDepartmentEmployees)
            {
                var actualPercentage = globalHrDepartmentPercentages
                    ?.FirstOrDefault(x => x.Item1 == hrDepartmentEmployee.ID)
                    ?.Item3;
                actualBonusesDict.Add(hrDepartmentEmployee.ID, GetActualBonusForEmployee(actualPercentage, totalBonusAvailableForHrDepartment));
            }

            return actualBonusesDict;
        }

        public decimal GetActualBonusForEmployee(decimal? bonusPercentage, decimal totalAvailableHrDepartmentBonus)
        {
            return (decimal) (totalAvailableHrDepartmentBonus * bonusPercentage);
        }

        public Dictionary<int, decimal?> ProcessGlobalHrDepartmentBonusDistribution(
            BonusPoolCalculatorModel bonusPoolCalculatorModel, IEnumerable<HrDepartment> hrDepartments)
        {
            var hrDepartmentsGlobalBonuses = new Dictionary<int, decimal?>();

            foreach (var hrDepartment in hrDepartments)
            {
                hrDepartmentsGlobalBonuses.Add(hrDepartment.ID, CalculateBonusForHrDepartment(hrDepartment.BonusPoolAllocationPerc, bonusPoolCalculatorModel.BonusPoolAmount));
            }

            return hrDepartmentsGlobalBonuses;
        }

        public decimal? CalculateBonusForHrDepartment(int? bonus, int totalBonus)
        {
            var bonusPercentage = Convert.ToDecimal(bonus) / 100m;
            return (decimal)totalBonus * bonusPercentage;
        }

        public List<Tuple<int, string, decimal>> ProcessGlobalHrDepartmentEmployeePercentage(List<HrEmployee> hrEmployeesByDepartment)
        {
            var globalHrDepartmentEmployeePercentage = new List<Tuple<int, string, decimal>>();

            var sum = hrEmployeesByDepartment.Sum(x => x.Salary);

            foreach (var hrEmployeeByDepartment in hrEmployeesByDepartment)
            {
                globalHrDepartmentEmployeePercentage.Add(new Tuple<int, string, decimal>(hrEmployeeByDepartment.ID, hrEmployeeByDepartment.Full_Name, CalculatePercentageForHrDepartmentBasedOnSalary(sum, hrEmployeeByDepartment.Salary)));
            }

            return globalHrDepartmentEmployeePercentage;
        }

        public decimal CalculatePercentageForHrDepartmentBasedOnSalary(int sum, int salary)
        {
            return Math.Round(Convert.ToDecimal(salary), 2) / Math.Round(Convert.ToDecimal(sum), 2);
        }

    }
}
