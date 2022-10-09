using System;
using System.ComponentModel.DataAnnotations;

namespace UserRegistrationTask.Security.Security_Model
{
    public class UserRegister
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Range(0, int.MaxValue)]
        public int PhoneNo { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}

