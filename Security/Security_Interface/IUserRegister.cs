using System;
using UserRegistrationTask.Security.Security_Model;

namespace UserRegistrationTask.Security.Security_Interface
{
    public interface IUserRegister
    {
        public Utilities.JsonResponse UserRegister(UserRegister userRegister);
    }
}

