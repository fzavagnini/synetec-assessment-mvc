using System;

namespace SynetecMvcAssesmentRefactored.Model.Data
{
    public class HrDepartment
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> BonusPoolAllocationPerc { get; set; }
    }
}