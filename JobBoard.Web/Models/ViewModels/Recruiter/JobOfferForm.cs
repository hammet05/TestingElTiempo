using JobBoard.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace JobBoard.Web.Models.ViewModels.Recruiter
{
    public class JobOfferForm
    {
        [Required, StringLength(120)]
        public string Title { get; set; }

        [Required, StringLength(4000)]
        public string Description { get; set; }

        [Required, StringLength(120)]
        public string Location { get; set; }

        [Range(0, 100000000)]
        public decimal Salary { get; set; }

        [Required]
        public ContractType ContractType { get; set; }
        public bool IsActive { get; set; } = true;

        public IEnumerable<SelectListItem> ContractTypeOptions { get; set; }
    }
}