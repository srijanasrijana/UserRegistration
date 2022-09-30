using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserRegistrationTask.Security.Security_Interface;
using UserRegistrationTask.Utilities;

namespace UserRegistrationTask.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginUser _loginUser;
        private readonly ILoggedinUser _IloggedinUser;
        public LoginController(ILoginUser loginUser, ILoggedinUser IloggedinUser)
        {
            _loginUser = loginUser;
            this._IloggedinUser = IloggedinUser;
        }
        public IActionResult Index()
        {
                 
            return View();
        }

        #region Login
        [HttpPost]

        public ActionResult Index(string Username, string Password)
        {
            if (Username != null && Password != null)
            {
                try
                {
                    Password = Crypto.OneWayEncryter(Password);
                    var user = _loginUser.Login(Username, Password);
                    if (user != null)
                    {
                        double cookieExpireTime = Convert.ToDouble(15);
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName.ToUpper(), ClaimValueTypes.String));
                        var userIdentity = new ClaimsIdentity("isAdmin");

                        userIdentity.AddClaims(claims);
                        var userPrincipal = new ClaimsPrincipal(userIdentity);
                        HttpContext.SignInAsync("UserRegistrationCookies", userPrincipal,
                           new AuthenticationProperties
                           {
                               ExpiresUtc = DateTime.UtcNow.AddDays(cookieExpireTime),
                               IsPersistent = true,
                               AllowRefresh = true,
                           });
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Failed";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    ViewBag.Message = "Failed";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Failed";
                return View();
            }
        }
        #endregion

        [Authorize]
        #region Logout
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync("UserRegistrationCookies");
                TempData["loggedOut"] = "You have logged out.";
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }

}

