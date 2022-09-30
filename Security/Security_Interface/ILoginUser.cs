using System;
using Microsoft.Win32;
using UserRegistrationTask.Security.Security_Model;

namespace UserRegistrationTask.Security.Security_Interface
{
    public interface ILoginUser
    {
        public UserRegister Login(string Username, string Password);
    }
}

