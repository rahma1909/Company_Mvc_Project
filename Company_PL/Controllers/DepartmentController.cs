using Company_BLL.Interfaces;
using Company_BLL.Repositories;
using Company_DAL.Models;
using Company_PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company_PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _depRepo;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _depRepo = departmentRepository;
        }


        public IActionResult Index()
        {

            var departments = _depRepo.GetAll();
            return View(departments);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public IActionResult Create(CreateDepartmentDTO model)
        {
            if (ModelState.IsValid) //server side validation
            {
                var department = new Department()
                {
                    Code=model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };
                //manual mapping
              var count=  _depRepo.Add(department);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
    }
}
