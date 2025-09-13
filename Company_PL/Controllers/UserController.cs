using Company_DAL.Data.Models;
using Company_PL.Dtos;
using Company_PL.helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company_PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> UserManager)
        {
            _userManager = UserManager;
        }



        public async Task<IActionResult> Index(string? SearchInput)
        {

            IEnumerable<UserToReturnDto> users;

            if (string.IsNullOrEmpty(SearchInput))
            {
                users = _userManager.Users.Select(u => new UserToReturnDto()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Email = u.Email,
                    Roles =  _userManager.GetRolesAsync(u).Result

                });
            }
            else
            {
               users= _userManager.Users.Select(u => new UserToReturnDto()
               {
                   Id = u.Id,
                   FirstName = u.FirstName,
                   LastName = u.LastName,
                   UserName = u.UserName,
                   Email = u.Email,
                   Roles = _userManager.GetRolesAsync(u).Result

               }).Where(u => u.FirstName.ToLower().Contains(SearchInput.ToLower()));
            }
         

            return View(users);
        }



        [HttpGet]
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id == null)
            {
                return BadRequest("invalid id");
            }


            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound($"emp with id {id} is not found");

            var dto = new UserToReturnDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName=user.FirstName,
                Email=user.Email,
                LastName=user.LastName,
                Roles=_userManager.GetRolesAsync(user).Result
            };

            return View(ViewName, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id) //covert from emp to empdto manual mapper
        {

       return await Details(id,"Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserToReturnDto model)
        {
          

            if (ModelState.IsValid) //server side validation
            {
                if (id != model.Id)
                {
                    return BadRequest("invalid operation");
                }


              var user=await  _userManager.FindByIdAsync(id);
                if (user is null )
                {
                    return BadRequest("invalid operation");
                }
                //update
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Email = model.Email;
           var res=await     _userManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index");
                }
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
        public async Task<IActionResult> Delete([FromRoute] string id, UserToReturnDto model)
        {

            if (ModelState.IsValid)
            {
                if (id != model.Id)
                {
                    return BadRequest("invalid operation");
                }


                var user = await _userManager.FindByIdAsync(id);
                if (user is null)
                {
                    return BadRequest("invalid operation");
                }
                //delete
               
                var res = await _userManager.DeleteAsync(user);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);




        
         


        }


    }
}
