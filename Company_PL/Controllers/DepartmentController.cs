using Company_BLL.Interfaces;
using Company_BLL.Repositories;
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
    }
}
