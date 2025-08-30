using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobBoard.Web.Models.ViewModels.Candidate
{
    public class JobOfferIndex
    {
        public string Id { get; set; }
        public string Location { get; set; }
        public string Contract { get; set; }
        public bool OnlyActive { get; set; } = true;

        public IEnumerable<JobOfferListItem> Items { get; set; }
    }

    public class JobOfferListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public decimal Salary { get; set; }
        public string ContractType { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}