using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserRegistrationTask.Security.Security_Interface;
using UserRegistrationTask.Utilities;

namespace UserRegistrationTask.Controllers
{
   /**
   * Class that manages Login
   */
    public class LoginController : Controller
    {
        /**
        *  Login User Repository Instance
        */
        private readonly ILoginUser _loginUser;
        private readonly ILoggedinUser _IloggedinUser;
        public LoginController(ILoginUser loginUser, ILoggedinUser IloggedinUser)
        {
            this._loginUser = loginUser;
            this._IloggedinUser = IloggedinUser;
        }
        public IActionResult Index()
        {                 
            return View();
        }

        /**
         * Method that fetch login user Details 
         */
        #region Login
        [HttpPost]
        public ActionResult Index(string Username, string Password)
        {
            if (Username != null && Password != null)
            {
                try
                {
                    //To encrypt login user password
                    Password = Crypto.OneWayEncryter(Password);
                    var user = _loginUser.Login(Username, Password);
                    if (user != null)
                    {
                        //set coolie time expire
                        double cookieExpireTime = Convert.ToDouble(15);
                        //claim use : to add additional information so that information will be use lator
                        var claims = new List<Claim>();

                        claims.Add(new Claim(ClaimTypes.Name, user.UserName.ToUpper(), ClaimValueTypes.String));

                        var claimsIdentity = new ClaimsIdentity(
                          claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        //SignInAsync creates an encrypted cookie and adds it to the current response.
                        HttpContext.SignInAsync("UserRegistrationCookies", new ClaimsPrincipal(claimsIdentity),
                           new AuthenticationProperties
                           {
                               //sets the time at which the authentication ticket expires.
                               ExpiresUtc = DateTime.UtcNow.AddDays(cookieExpireTime),
                               //sets whether the authentication session is persisted across multiple requests.
                               IsPersistent = true,
                               //sets if refreshing the authentication session should be allowed.
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
                //  Clear the existing external cookie
                await HttpContext.SignOutAsync("UserRegistrationCookies");
                TempData["loggedOut"] = "You have logged out.";
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                throw new Exception("Error" + ex.Message);
            }
        }
        #endregion
    }

}

