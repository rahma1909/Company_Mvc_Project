
using System.Security.Policy;
using Company_DAL.Data.Models;
using Company_PL.Dtos;
using Company_PL.helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Company_PL.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<AppUser> _appuser;
        private readonly SignInManager<AppUser> _signinmanager;

        public AccountController(UserManager<AppUser> appuser,SignInManager<AppUser> Signinmanager)
        {
            _appuser = appuser;
            _signinmanager = Signinmanager;
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
        [HttpGet]

        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]

        public async Task< IActionResult> SignIn(SignInDto model)
        {

            if (ModelState.IsValid)
            {
           var user=   await  _appuser.FindByEmailAsync(model.Email);

                if(user is not null)
                {
                    var flag = await _appuser.CheckPasswordAsync(user, model.Password);

                    if (flag)
                    {
                        //sign in



               var res=    await     _signinmanager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (res.Succeeded)
                        {
                            //redierct to home
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                      
                    }
                }
                ModelState.AddModelError("", "invalide login!!");
            }



            return View(model);
        }



        #endregion



        #region singout

        [HttpGet]

        public new async Task<IActionResult>  SignOut()
        {
           await _signinmanager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }


        #endregion


      #region Forget Password
        [HttpGet]

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]

        public  async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordDTO model)
        {
            if (ModelState.IsValid)
            {

                var user = await _appuser.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    var token =  await _appuser.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token }, Request.Scheme);
                    //good
                    var email = new Email()
                    {
To=model.Email,
Subject="Resert Password",
Body=url
                    };

                  var flag=  EmailSettings.SendEmail(email);

                    if (flag)
                    {
                        //good

                        //check inbox
                        return RedirectToAction("CheckYourInbox");
                    }
                }
                 

            }
            ModelState.AddModelError("", "Invalid ResetPassword");
            return View("ForgetPassword",model );
        }

        [HttpGet]
        public  IActionResult CheckYourInbox()
        {
            return View();
        }


        #endregion


        #region resetPassword

        [HttpGet]


        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();   
        
        }


        [HttpPost ]


        public async Task<IActionResult>  ResetPassword(ResetPasswordDto model)
        {
            var email = TempData["email"] as string;
            var token = TempData["token"] as string;
            if (ModelState.IsValid)
            {
                var user = await _appuser.FindByEmailAsync(email);
                if(user is not null)
                {
                    var res = await _appuser.ResetPasswordAsync(user, token, model.Password);
                    if (res.Succeeded){

                        return RedirectToAction("SignIn");


                    }
                    {
                        
                    }
                }
                ModelState.AddModelError("", "invalid operation of reset password");
            }
            return View();

        }



        #endregion
    }
}
