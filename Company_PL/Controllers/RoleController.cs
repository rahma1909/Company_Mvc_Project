using Company_DAL.Data.Models;
using Company_PL.Dtos;
using Company_PL.helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company_PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _Rolemanagmer;

        public RoleController(RoleManager<IdentityRole> Rolemanagmer)
        {
            _Rolemanagmer = Rolemanagmer;
        }


        public async Task<IActionResult> Index(string? SearchInput)
        {

            IEnumerable<RoleToReturnDto> users;

            if (string.IsNullOrEmpty(SearchInput))
            {
                users = _Rolemanagmer.Roles.Select(u => new RoleToReturnDto()
                {
                    Id = u.Id,
          Name=u.Name,
                    

                });
            }
            else
            {
                users = _Rolemanagmer.Roles.Select(u => new RoleToReturnDto()
                {
                    Id = u.Id,
                    Name = u.Name,


                }).Where(u => u.Name.ToLower().Contains(SearchInput.ToLower()));
            }


            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
    
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(RoleToReturnDto model)
        {


            if (ModelState.IsValid) //server side validation
            {


                var role=await _Rolemanagmer.FindByNameAsync(model.Name);
                if(role is null)
                {
                    role = new IdentityRole()
                    {
                        Name=model.Name
                    };



                    var res = await _Rolemanagmer.CreateAsync(role);
                    if (res.Succeeded)
                    {
                        return RedirectToAction("Index");

                    }
                }
             
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id == null)
            {
                return BadRequest("invalid id");
            }


            var role = await _Rolemanagmer.FindByIdAsync(id);

            if (role is null)
                return NotFound($"role with id {id} is not found");

            var dto = new RoleToReturnDto()
            {
                Id =role.Id,
                Name = role.Name,

            };

            return View(ViewName, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id) //covert from emp to empdto manual mapper
        {

            return await Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDto model)
        {


            if (ModelState.IsValid) //server side validation
            {
                if (id != model.Id)
                {
                    return BadRequest("invalid operation");
                }


                var role = await _Rolemanagmer.FindByIdAsync(id);
                if (role  is  null)
                {
                    return BadRequest("invalid operation");
                }
                var roleres = await _Rolemanagmer.FindByNameAsync(model.Name);
                if (roleres is  null)
                {
                    role.Name = model.Name;
                    var res = await _Rolemanagmer.UpdateAsync(role);
                    if (res.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError("", "invalid operatin");
            }
              
              
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {




            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleToReturnDto model)
        {


            if (ModelState.IsValid) //server side validation
            {
                if (id != model.Id)
                {
                    return BadRequest("invalid operation");
                }


                var role = await _Rolemanagmer.FindByIdAsync(id);
                if (role is not null)
                {
                    return BadRequest("invalid operation");
                }
              
                    role.Name = model.Name;
                    var res = await _Rolemanagmer.DeleteAsync(role);
                    if (res.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                
                ModelState.AddModelError("", "invalid operatin");
            }


            return View(model);







        }
    }
}
