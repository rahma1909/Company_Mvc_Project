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


        [HttpGet]
        public IActionResult Details(int? id,string ViewName="Details")
        {
            if (id == null)
            {
                return BadRequest("invalid id");
            }
   
            
            var dep=  _depRepo.Get(id.Value);

            if(dep is null)
             return NotFound($"dep with id {id} is not found");


            return View(ViewName,dep);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            //if (id == null)
            //{
            //    return BadRequest("invalid id");
            //}


            //var dep = _depRepo.Get(id.Value);

            //if (dep is null)
            //    return NotFound($"dep with id {id} is not found");


            return Details(id,"Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {

            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                    var count = _depRepo.Update(department);


                    if (count > 0) return RedirectToAction("Index");

                }

            }
            return View(department);


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


            return Details(id,"Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {

            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                    var count = _depRepo.Delete(department);


                    if (count > 0) return RedirectToAction("Index");

                }

            }
            return View(department);


        }


    }
}
