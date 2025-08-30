using JobBoard.Common.Enums;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JobBoard.Web.Models.ViewModels.Recruiter
{
    public class JobOfferForm
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public decimal Salary { get; set; }
        public ContractType ContractType { get; set; }
        public bool IsActive { get; set; } = true;

        public IEnumerable<SelectListItem> ContractTypeOptions { get; set; }
    }
}