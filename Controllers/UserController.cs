using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using UserRegistrationTask.Security.Security_Interface;
using UserRegistrationTask.Security.Security_Model;
using UserRegistrationTask.Utilities;

namespace UserRegistrationTask.Controllers
{
   /**
   * Class that manages User 
   */

    [Controller]
    [Route("User")]

    public class UserController : Controller
    {
        /**
        *  User Register Repository Instance
        */
        private readonly IUserRegister _userRegister;
        public UserController(IUserRegister userRegister)
        {
            this._userRegister = userRegister;
        }
        
        public IActionResult Register()
        {
            return View();
        }

        /**
         * Method that Save Register Details 
         */
        [HttpPost("SaveRegister")]
        public IActionResult SaveRegister(UserRegister userRegister)
        {
            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                //make list of errors messages
                List<string> errorList = new List<string>();
                foreach(var error in errors)
                {
                    errorList.Add(error[0].ErrorMessage);
                }

                JsonResponse response = new JsonResponse();
                response.IsSuccess = false;
                response.Message = errors[0][0].ErrorMessage;
                response.ErrorList = errorList;
                 return Ok(response);

            }

            JsonResponse isExistingUser = _userRegister.checkExistingUser(userRegister.UserName);
            if (isExistingUser.IsSuccess == false)
            {
                return Ok(isExistingUser);
            }
            

            //To encrypt user password
            userRegister.Password = Crypto.OneWayEncryter(userRegister.Password);
            return Ok(_userRegister.UserRegister(userRegister));
            
        }

     //   [HttpPost("CheckUsername")]
      //  public IActionResult CheckUsername(string Username)
      //  {          
           // return Ok(_userRegister.CheckUsername(Username));
     //   }

    }
}

