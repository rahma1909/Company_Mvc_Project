using AutoMapper;
using Company_BLL.Interfaces;
using Company_DAL.Data.Models;
using Company_DAL.Models;
using Company_PL.Dtos;
using Company_PL.helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Company_PL.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _EmpRepo;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepository EmployeeRepository,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork UnitOfWork,
            IMapper mapper)
        {
            _unitOfWork = UnitOfWork;
            //_EmpRepo = EmployeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }


        public async Task< IActionResult> Index(string? SearchInput)
        {

            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchInput))
            {
               employees =await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                 employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }
            //transfer extra info from controller (action) to view
            ////viewdata
            ////ViewData["Massege"] = "hello from employee";

            ////viewbag
            //ViewBag.Massege = "hello";
            ////more flexable ف حاله انك مش عارف التايب
            ////compiler will ignore type safety


            //tempdata==>send data from req to req
          
            return View(employees);
        }



        [HttpGet]
        public  async Task<IActionResult> Create()
        {
        var dep= await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["dep"] = dep;
            return View();
        }




        [HttpPost]
        
        public async Task< IActionResult> Create(CreateEmployeeDTO model)
        {
            
         
            if (ModelState.IsValid) //server side validation
            {
                if (model.Image is not null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
                }
                //manual mapping
                //var employee = new Employee()
                //{
                //   Name=model.Name,
                //   Address=model.Address,
                //   Age=model.Age,
                //   CreateAt=model.CreateAt,
                //   HiringDate=model.HiringDate,
                //   IsActive=model.IsActive,
                //   IsDeleted=model.IsDeleted,
                //   Email=model.Email,
                //   Salary=model.Salary,
                //   Phone = model.Phone,
                //   DepartmentId=model.DepartmentId
                //};
                var employee=     _mapper.Map<Employee>(model);
                //manual mapping
             await  _unitOfWork.EmployeeRepository.AddAsync(employee);
           var count=  await   _unitOfWork.completeAsync();
                if (count > 0)
                {
                    TempData["massege"] = "employee created successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task< IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null)
            {
                return BadRequest("invalid id");
            }


            var emp = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

            if (emp is null)
                return NotFound($"emp with id {id} is not found");


            return View(ViewName, emp);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id) //covert from emp to empdto manual mapper
        {

            var dep =  await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["dep"] = dep;

            if (id == null)
            {
                return BadRequest("invalid id");
            }
            var emp =  await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (emp is null)
                return NotFound($"emp with id {id} is not found");

            //var empdto = new CreateEmployeeDTO()
            //{
            //    Id=emp.Id,
            //    Name = emp.Name,
            //    Address = emp.Address,
            //    Age = emp.Age,
            //    CreateAt = emp.CreateAt,
            //    HiringDate = emp.HiringDate,
            //    IsActive = emp.IsActive,
            //    IsDeleted = emp.IsDeleted,
            //    Email = emp.Email,
            //    Salary = emp.Salary,
            //    Phone = emp.Phone,
            //    DepartmentId=emp.DepartmentId
            //};
             
            var empdto = _mapper.Map<CreateEmployeeDTO>(emp);
           

            return View(empdto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDTO model)
        {
            var dep = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["dep"] = dep;

            if (ModelState.IsValid) //server side validation
            {
                if(model.ImageName is not null && model.Image is not null)
                {
                    DocumentSettings.DeleteFile(model.ImageName, "images");

                }

        if(model.Image is not null)
                {
             model.ImageName=       DocumentSettings.UploadFile(model.Image, "images");
                }

                //var employee = new Employee()
                //{
                //    Id=model.Id,
                //    Name = model.Name,
                //    Address = model.Address,
                //    Age = model.Age,
                //    CreateAt = model.CreateAt,
                //    HiringDate = model.HiringDate,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    Email = model.Email,
                //    Salary = model.Salary,
                //    Phone = model.Phone,
                //    DepartmentId = model.DepartmentId
                //};

                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;
         _unitOfWork.EmployeeRepository.Update(employee);
                var count = _unitOfWork.completeAsync();

                if ( await count > 0) return RedirectToAction("Index");


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
        public async Task<IActionResult> Delete(int? id)
        {

            //if (id == null)
            //{
            //    return BadRequest("invalid id");
            //}


            //var dep = _depRepo.Get(id.Value);

            //if (dep is null)
            //    return NotFound($"dep with id {id} is not found");


            return  await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, CreateEmployeeDTO dto)
        {

            if (ModelState.IsValid)
            {
                
                
                    var employee = _mapper.Map<Employee>(dto);
                    employee.Id = id;
                   _unitOfWork.EmployeeRepository.Delete(employee);

                var count = await _unitOfWork.completeAsync();
                if ( count > 0)
                {
                    if(dto.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(dto.ImageName, "images");
                    }
                   
                    return RedirectToAction("Index");
                }
              

            }
            return View(dto);


        }


    }
}

