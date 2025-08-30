using JobBoard.Dtos;
using JobBoard.Services.Contracts;
using JobBoard.Web.Models.ViewModels.Candidate;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JobBoard.Web.Controllers
{
    public class HomeController : Controller
    {
        public IJobOfferService Service { get; set; }

        public ActionResult Index()
        {
            var items = Service.List();

            var vms = new List<JobOfferListItem>();
            foreach (var d in items)
                vms.Add(new JobOfferListItem
                {
                    Id = d.Id,
                    Title = d.Title,
                    Location = d.Location,
                    Salary = d.Salary,
                    ContractType = d.ContractType,
                    PublishedAt = d.PublishedAt
                });

            return View(vms);
        }
    }
}