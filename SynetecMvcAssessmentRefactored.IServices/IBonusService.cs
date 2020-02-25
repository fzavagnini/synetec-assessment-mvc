using System;
using SynetecMvcAssesmentRefactored.Model.Core;

namespace SynetecMvcAssessmentRefactored.IServices
{
    public interface IBonusService
    {
        BonusPoolCalculatorResultModel CalculateEmployeeBonus(BonusPoolCalculatorModel bonusPoolCalculatorModel);
    }
}
