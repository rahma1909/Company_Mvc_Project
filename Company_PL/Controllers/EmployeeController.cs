using Company_BLL.Interfaces;
using Company_DAL.Data.Models;
using Company_DAL.Models;
using Company_PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company_PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _EmpRepo;
        public EmployeeController(IEmployeeRepository EmployeeRepository)
        {
            _EmpRepo = EmployeeRepository;
        }


        public IActionResult Index()
        {
            ////viewdata
            ////ViewData["Massege"] = "hello from employee";

            ////viewbag
            //ViewBag.Massege = "hello";
            ////more flexable ف حاله انك مش عارف التايب
            ////compiler will ignore type safety


            //tempdata==>send data from req to req
            var employees = _EmpRepo.GetAll();
            return View(employees);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public IActionResult Create(CreateEmployeeDTO model)
        {
            if (ModelState.IsValid) //server side validation
            {
                var employee = new Employee()
                {
                   Name=model.Name,
                   Address=model.Address,
                   Age=model.Age,
                   CreateAt=model.CreateAt,
                   HiringDate=model.HiringDate,
                   IsActive=model.IsActive,
                   IsDeleted=model.IsDeleted,
                   Email=model.Email,
                   Salary=model.Salary,
                   Phone = model.Phone
                };
                //manual mapping
                var count = _EmpRepo.Add(employee);
                if (count > 0)
                {
                    TempData["massege"] = "employee created successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null)
            {
                return BadRequest("invalid id");
            }


            var emp = _EmpRepo.Get(id.Value);

            if (emp is null)
                return NotFound($"emp with id {id} is not found");


            return View(ViewName, emp);
        }

        [HttpGet]
        public IActionResult Edit(int? id) //covert from emp to empdto manual mapper
        {

            if (id == null)
            {
                return BadRequest("invalid id");
            }
            var emp = _EmpRepo.Get(id.Value);
            if (emp is null)
                return NotFound($"emp with id {id} is not found");

            var empdto = new CreateEmployeeDTO()
            {
                Name = emp.Name,
                Address = emp.Address,
                Age = emp.Age,
                CreateAt = emp.CreateAt,
                HiringDate = emp.HiringDate,
                IsActive = emp.IsActive,
                IsDeleted = emp.IsDeleted,
                Email = emp.Email,
                Salary = emp.Salary,
                Phone = emp.Phone
            };
    

           

            return View(empdto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDTO model)
        {

            if (ModelState.IsValid) //server side validation
            {
                var employee = new Employee()
                {
                    Name = model.Name,
                    Address = model.Address,
                    Age = model.Age,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Email = model.Email,
                    Salary = model.Salary,
                    Phone = model.Phone
                };
                var count = _EmpRepo.Update(employee);


                if (count > 0) return RedirectToAction("Index");


            }
            return View(model);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken] //with any post action
        //public IActionResult Edit([FromRoute] int id, DepartmentEditDTO DTODepEdit)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var department = new Department()
        //        {
        //            Id = id,
        //            Code=DTODepEdit.Code,
        //            Name=DTODepEdit.Name,
        //            CreateAt=DTODepEdit.CreateAt
        //        };

        //            var count = _depRepo.Update(department);


        //            if (count > 0) return RedirectToAction("Index");



        //    }
        //    return View(DTODepEdit);


        //}

        [HttpGet]
        public IActionResult Delete(int? id)
        {

            //if (id == null)
            //{
            //    return BadRequest("invalid id");
            //}


            //var dep = _depRepo.Get(id.Value);

            //if (dep is null)
            //    return NotFound($"dep with id {id} is not found");


            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {

            if (ModelState.IsValid)
            {
                if (id == employee.Id)
                {
                    var count = _EmpRepo.Delete(employee);


                    if (count > 0) return RedirectToAction("Index");

                }

            }
            return View(employee);


        }


    }
}

