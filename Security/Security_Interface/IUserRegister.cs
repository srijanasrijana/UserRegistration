using System;
using UserRegistrationTask.Security.Security_Model;
using UserRegistrationTask.Utilities;

namespace UserRegistrationTask.Security.Security_Interface
{
    public interface IUserRegister
    {
        public Utilities.JsonResponse UserRegister(UserRegister userRegister);

        public JsonResponse checkExistingUser(string username);
    }
}

