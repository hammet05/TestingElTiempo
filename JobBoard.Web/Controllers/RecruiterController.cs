using JobBoard.Common.Enums;
using JobBoard.Dtos;
using JobBoard.Services.Contracts;
using JobBoard.Web.Models.ViewModels.Recruiter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Reflection;

namespace JobBoard.Web.Controllers
{
    public class RecruiterController : Controller
    {
        public IJobOfferService Service { get; set; }
        public ActionResult Index()
        {
            var items = Service.List(onlyActive: false);

            var vms = new List<JobOfferAdminListItem>();
            foreach (var d in items)
                vms.Add(new JobOfferAdminListItem
                {
                    Id = d.Id,
                    Title = d.Title,
                    Location = d.Location,
                    Salary = d.Salary,
                    ContractType = d.ContractType,
                    PublishedAt = d.PublishedAt,
                });

            return View(vms);
        }

        public ActionResult Create()
        {
            var vm = new JobOfferForm
            {
                ContractTypeOptions = GetContractTypeOptions()
            };
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(JobOfferForm vm)
        {
            if (!ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                    return Json(new { ok = false, message = "Datos inválidos" });
                vm.ContractTypeOptions = GetContractTypeOptions();
                return View(vm);
            }

            var dto = new JobOfferCreateDto
            {
                Title = vm.Title,
                Description = vm.Description,
                Location = vm.Location,
                Salary = vm.Salary,
                ContractType = vm.ContractType.ToString(),
                IsActive = vm.IsActive
            };
            Service.Create(dto);

            if (Request.IsAjaxRequest())
                return Json(new { ok = true, message = "Oferta creada" });

            TempData["Toast"] = "Oferta creada";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var response = Service.Get(id);
            if (response == null) return HttpNotFound();

            var vm = new JobOfferForm
            {
                Title = response.Title,
                Description = response.Description,
                Location = response.Location,
                Salary = response.Salary,
                ContractType = (ContractType)Enum.Parse(typeof(ContractType), response.ContractType, true),
                IsActive = response.IsActive,
                ContractTypeOptions = GetContractTypeOptions(
                     (ContractType)Enum.Parse(typeof(ContractType), response.ContractType, true)
                )
            };
            ViewBag.Id = id;
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, JobOfferForm vm)
        {
            if (!ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                    return Json(new { ok = false, message = "Datos inválidos" });
                vm.ContractTypeOptions = GetContractTypeOptions(vm.ContractType);
                ViewBag.Id = id;
                return View(vm);
            }

            //

            if (Request.IsAjaxRequest())
            {
                TempData["Toast"] = "Oferta actualizada";

                return Json(new { ok = true, message = "Oferta actualizada" });
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Toggle(int id)
        {
            var dto = Service.Get(id);
            if (dto == null) return Json(new { ok = false, message = "Oferta no encontrada" });

            var newActive = !dto.IsActive;

            return Json(new { ok = true, active = newActive });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var dto = Service.Get(id);
            if (dto == null)
                return Json(new { ok = false, message = "La oferta ya no existe." });

            Service.Delete(id);

            return Json(new { ok = true, id, message = "Oferta eliminada" });
        }

        private static string GetDisplayName(Enum value)
        {
            var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            var attr = member?.GetCustomAttribute<DisplayAttribute>();
            return attr?.Name ?? value.ToString();
        }

        private static IEnumerable<SelectListItem> GetContractTypeOptions(ContractType? selected = null, bool includeUndefined = false)
        {
            var all = Enum.GetValues(typeof(ContractType)).Cast<ContractType>();
            if (!includeUndefined) all = all.Where(x => x != ContractType.Undefined);

            return all.Select(x => new SelectListItem
            {
                Text = GetDisplayName(x),
                Value = ((int)x).ToString(),
                Selected = selected.HasValue && x == selected.Value
            });
        }
    }
}