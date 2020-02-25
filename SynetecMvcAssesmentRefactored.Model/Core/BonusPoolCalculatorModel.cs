using System;
using System.Collections.Generic;
using SynetecMvcAssesmentRefactored.Model.Data;

namespace SynetecMvcAssesmentRefactored.Model.Core
{
    public class BonusPoolCalculatorModel
    {
        public int BonusPoolAmount { get; set; }
        public List<HrEmployee> AllEmployees { get; set; }
        public int SelectedEmployeeId { get; set; }

    }
}
