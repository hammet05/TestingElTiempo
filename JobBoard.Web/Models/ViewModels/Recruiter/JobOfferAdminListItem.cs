using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobBoard.Web.Models.ViewModels.Recruiter
{
    public class JobOfferAdminListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public decimal Salary { get; set; }
        public string ContractType { get; set; }
        public DateTime PublishedAt { get; set; }
        public bool IsActive { get; set; }
    }
}