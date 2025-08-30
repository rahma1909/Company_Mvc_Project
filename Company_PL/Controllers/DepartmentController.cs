using Company_BLL.Interfaces;
using Company_BLL.Repositories;
using Company_DAL.Models;
using Company_PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company_PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _depRepo;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            //_depRepo = departmentRepository;
           _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index()
        {

            var departments =  await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        public  async Task<IActionResult> Create(CreateDepartmentDTO model)
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
            await _unitOfWork.DepartmentRepository.AddAsync(department);
                var count = await _unitOfWork.completeAsync();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id,string ViewName="Details")
        {
            if (id == null)
            {
                return BadRequest("invalid id");
            }
   
            
            var dep= await _unitOfWork.DepartmentRepository.GetAsync(id.Value);

            if(dep is null)
             return NotFound($"dep with id {id} is not found");


            return View(ViewName,dep);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null) return BadRequest("invalid id");
            


            var dep = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (dep is null)
                return NotFound($"dep with id {id} is not found");


            var depdto =new CreateDepartmentDTO(){
                Name=dep.Name,
                CreateAt=dep.CreateAt,
                Code=dep.Code,
            };
            


            return View(depdto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateDepartmentDTO dto)
        {
            if (ModelState.IsValid)
            {
              
                var department = new Department
                {
                    Id = id,
                    Code = dto.Code,
                    Name = dto.Name,
                    CreateAt = dto.CreateAt
                };

                _unitOfWork.DepartmentRepository.Update(department);

                var count = await _unitOfWork.completeAsync();
                if (count > 0)
                    return RedirectToAction("Index");
            }

          
            return View(dto);
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
        public async Task<IActionResult> Delete(int? id)
        {

            //if (id == null)
            //{
            //    return BadRequest("invalid id");
            //}


            //var dep = _depRepo.Get(id.Value);

            //if (dep is null)
            //    return NotFound($"dep with id {id} is not found");


            return await Details(id,"Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);

            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                     _unitOfWork.DepartmentRepository.Delete(department);

                    var count = await _unitOfWork.completeAsync();
                    if (count > 0) return RedirectToAction("Index");

                }

            }
            return View(department);


        }


    }
}
