using System;
using Dapper;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using System.Data;
using System.Data.SqlClient;
using UserRegistrationTask.Utilities;
using UserRegistrationTask.Security.Security_Interface;
using UserRegistrationTask.Security.Security_Model;

namespace UserRegistrationTask.Security.Security_Repository
{
    public class LoginRepo: ILoginUser
    {
        IOptions<ReadConfig> connectionString;
        public LoginRepo(IOptions<ReadConfig> _connectionString)
        {
            connectionString = _connectionString;
        }

       
        public UserRegister Login(string Username, string Password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Value.DefaultConnection))
            {

                JsonResponse response = new JsonResponse();
                try
                {
                    connection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@UserName", Username);
                    param.Add("@Password", Password);
                    UserRegister user = connection.Query<UserRegister>("VALIDATE_USER", param, commandType: CommandType.StoredProcedure)?.FirstOrDefault();
                    return user;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}

