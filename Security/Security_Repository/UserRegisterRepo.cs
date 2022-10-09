using System;
using Microsoft.Extensions.Options;
using System.Data;
using UserRegistrationTask.Utilities;
using UserRegistrationTask.Security.Security_Model;
using UserRegistrationTask.Security.Security_Interface;
using System.Data.SqlClient;
using Dapper;

namespace UserRegistrationTask.Security.Security_Repository
{
  /**
  * Repository class that manages all the Database Level operations for User Register
  */
    public class UserRegisterRepo:IUserRegister
    {
        //Readconfig defaultConnection Instance
        IOptions<ReadConfig> connectionString;
        public UserRegisterRepo(IOptions<ReadConfig> _connectionString)
        {
            connectionString = _connectionString;
        }

          #region Create Register
          /**
          * Method that Save User Register
          */
        public JsonResponse UserRegister(UserRegister userRegister)
        {

            JsonResponse response = new JsonResponse();
            SqlCommand cmd;
            // Create a new instance of the SqlConnection class when given a string that contains the connection string.
            using (SqlConnection connection = new SqlConnection(connectionString.Value.DefaultConnection))
            {
                connection.Open(); 
                try
                {                   
                        cmd = new SqlCommand("REGISTER_USER", connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 200).Value = userRegister.UserName;
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar, 200).Value = userRegister.Password;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = userRegister.Email;
                        cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 200).Value = userRegister.Gender;
                        cmd.Parameters.Add("@MobileNo", SqlDbType.Int, 200).Value = userRegister.PhoneNo;
                        cmd.Parameters.Add("@Age", SqlDbType.Int, 200).Value = userRegister.Age;
                        cmd.ExecuteNonQuery(); //To execute query

                        response.IsSuccess = true;
                        response.Message = "Successfully Register.";

                }
                catch (Exception ex)
                {
                    throw new Exception("Error" + ex.Message);
                }
                return response;
            }
        }
        #endregion

        public JsonResponse checkExistingUser(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.Value.DefaultConnection))
            {
                JsonResponse response = new JsonResponse();
                try
                {
                    connection.Open();
                    //to pass parameter dynamically to the dapper query and execute methods.
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@UserName", username);
                    //The Query method executes a query and maps it to a list of dynamic objects
                    UserRegister user = connection.Query<UserRegister>("CHECK_USERNAME", param, commandType: CommandType.StoredProcedure)?.FirstOrDefault();
                    if(user != null)
                    {
                        response.IsSuccess = false;
                        //response.Message="User  "+ username + " Already Exist";
                        //response.Message = $"User {username} Already Exist";  //Interpolation
                        response.Message = String.Format("User {0} Already Exist", username);
                    }
                    else
                    {
                        response.IsSuccess = true;
                        response.Message = "New User";
                    }
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error" + ex.Message);
                }
            }

        }
   }
}

