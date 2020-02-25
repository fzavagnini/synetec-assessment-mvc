using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Models;


namespace InterviewTestTemplatev2.Controllers
{
    public class BonusPoolController : Controller
    {
        private MvcInterviewV3Entities1 db = new MvcInterviewV3Entities1();

        // GET: BonusPool
        public ActionResult Index()
        {
            BonusPoolCalculatorModel model = new BonusPoolCalculatorModel();
            model.AllEmployees = db.HrEmployees.ToList<HrEmployee>();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Calculate(BonusPoolCalculatorModel model)
        {
            int selectedEmployeeId = model.SelectedEmployeeId;
            int totalBonusPool = model.BonusPoolAmount;

            //load the details of the selected employee using the ID
            HrEmployee hrEmployee = (HrEmployee)db.HrEmployees.FirstOrDefault(item => item.ID == selectedEmployeeId);
            
            int employeeSalary = hrEmployee.Salary;

            //get the total salary budget for the company
            int totalSalary = (int)db.HrEmployees.Sum(item => item.Salary);

            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)employeeSalary / (decimal)totalSalary;
            int bonusAllocation = (int)(bonusPercentage * totalBonusPool);

            BonusPoolCalculatorResultModel result = new BonusPoolCalculatorResultModel();
            result.hrEmployee = hrEmployee;
            result.bonusPoolAllocation = bonusAllocation;
            
            return View(result);
        }
    }
}