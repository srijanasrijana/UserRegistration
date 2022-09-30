using System;
namespace UserRegistrationTask.Utilities
{

    public interface ILoggedinUser
    {
        string GetUserName();
    }

    public class LoggedinUser : ILoggedinUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoggedinUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserName()
        {
            try
            {
               return _httpContextAccessor.HttpContext.User.Identity.Name;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

    }
}

