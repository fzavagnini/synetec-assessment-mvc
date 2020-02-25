using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SynetecMvcAssesmentRefactored.Model.Core;
using SynetecMvcAssesmentRefactored.Model.Data;
using SynetecMvcAssesmentRefactored.Models;
using SynetecMvcAssessmentRefactored.IServices;

namespace SynetecMvcAssesmentRefactored.Controllers
{
    public class BonusController : Controller
    {
        private readonly IHrEmployeeService _hrEmployeeService;
        private readonly IBonusService _bonusService;
        private readonly ILogger<BonusController> _logger;

        public BonusController(IHrEmployeeService hrEmployeeService, IBonusService bonusService, ILogger<BonusController> logger)
        {
            _hrEmployeeService = hrEmployeeService;
            _bonusService = bonusService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            BonusPoolCalculatorModel model = new BonusPoolCalculatorModel();
            model.AllEmployees = _hrEmployeeService.GetHrEmployees();
            return View(model);
        }

        public IActionResult Calculate(BonusPoolCalculatorModel bonusPoolCalculatorModel)
        {
            var model = _bonusService.CalculateEmployeeBonus(bonusPoolCalculatorModel);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
