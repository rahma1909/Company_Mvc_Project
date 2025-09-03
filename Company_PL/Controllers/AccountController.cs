using Company_DAL.Data.Models;
using Company_PL.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Company_PL.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<AppUser> _appuser;

        public AccountController(UserManager<AppUser> appuser)
        {
            _appuser = appuser;
        }


        #region singup

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }



        [HttpPost]
        //P@ssw0rd
        public async Task<IActionResult> SignUp(SignUpDTO model)
        {
       var user= await   _appuser.FindByNameAsync(model.UserName);

            if (user is null)
            {
                user = await _appuser.FindByEmailAsync(model.Email);

                if (user is null)
                {
                    if (ModelState.IsValid)
                    {
                         user = new AppUser()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree,


                        };
                        var res = await _appuser.CreateAsync(user, model.Password);
                        if (res.Succeeded)
                        {

                            //send email to confirm email
                            return RedirectToAction("SingIn");
                        }
                        foreach (var error in res.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                    }
                }
            }



            ModelState.AddModelError("", "Invalid SignUp !!");


            return View(model);
        }

        #endregion



        #region singin

        #endregion



        #region singout

        #endregion
    }
}
