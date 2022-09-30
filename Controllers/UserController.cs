using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using UserRegistrationTask.Security.Security_Interface;
using UserRegistrationTask.Security.Security_Model;
using UserRegistrationTask.Utilities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserRegistrationTask.Controllers
{
   // [Controller]
    [Route("User")]

    public class UserController : Controller
    {
        private readonly IUserRegister _userRegister;
        public UserController(IUserRegister userRegister)
        {
            this._userRegister = userRegister;

        }
        // GET: /<controller>/
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("SaveRegister")]
        public IActionResult SaveRegister(UserRegister userRegister)
        {
            userRegister.Password = Crypto.OneWayEncryter(userRegister.Password);
            return Ok(_userRegister.UserRegister(userRegister));
        }
    }
}

