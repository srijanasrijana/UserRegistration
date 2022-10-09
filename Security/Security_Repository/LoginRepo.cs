using System;
using Dapper;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using UserRegistrationTask.Utilities;
using UserRegistrationTask.Security.Security_Interface;
using UserRegistrationTask.Security.Security_Model;

namespace UserRegistrationTask.Security.Security_Repository
{
 /**
 * Repository class that manages all the Database Level operations for Login User
 */
    public class LoginRepo: ILoginUser
    {
        //Readconfig defaultConnection Instance
        IOptions<ReadConfig> connectionString;
        public LoginRepo(IOptions<ReadConfig> _connectionString)
        {
            connectionString = _connectionString;
        }

        #region Login User
        /**
        * Method that fetch register user List with validate for Login
        */
        public UserRegister Login(string Username, string Password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Value.DefaultConnection))
            {
               JsonResponse response = new JsonResponse();
                try
                {
                    connection.Open();
                    //to pass parameter dynamically to the dapper query and execute methods.
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@UserName", Username);
                    param.Add("@Password", Password);
                    //The Query method executes a query and maps it to a list of dynamic objects
                    UserRegister user = connection.Query<UserRegister>("VALIDATE_USER", param, commandType: CommandType.StoredProcedure)?.FirstOrDefault();
                    return user;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error" + ex.Message);
                }
            }
        }
        #endregion
    }
}

