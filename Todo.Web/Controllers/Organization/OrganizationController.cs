using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Core;
using Todo.Core.DTO;
using Todo.Core.Entity;
using Todo.Web.Models.Board;
using Todo.Web.Models.Organization;

namespace Todo.Web.Controllers.Organization
{
    [AllowAnonymous]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService _service;

        public OrganizationController(IOrganizationService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var list = _service.AllOrganizations();
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddOrganizationViewModel model)
        {
            OrganizationDTO dto = new() { Name = model.Name };
            var addService = _service.AddOrganization(dto);
            if (!addService.IsSuccess)
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var organization = _service.GetOrganization(new() { Id = id });
            if (!organization.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            var organizationData = Convert.ChangeType(organization.Data, typeof(Todo.Core.Entity.Organization)) as Todo.Core.Entity.Organization;

            EditOrganizationViewModel viewModel = new()
            {  Id = organizationData.Id, Name= organizationData.Name };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditOrganizationViewModel model)
        {
            UpdateOrganizationDTO dto = new() { Id = model.Id, Name = model.Name };
            var addService = _service.UpdateOrganization(dto);
            if (!addService.IsSuccess)
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            DeleteOrganizationDTO dto = new() { Id = id};
            var addService = _service.DeleteOrganization(dto);
            if (!addService.IsSuccess)
            {
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
