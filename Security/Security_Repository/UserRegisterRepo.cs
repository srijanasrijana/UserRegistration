using System;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using System.Data;
using UserRegistrationTask.Utilities;
using UserRegistrationTask.Security.Security_Model;
using UserRegistrationTask.Security.Security_Interface;
using System.Data.SqlClient;

namespace UserRegistrationTask.Security.Security_Repository
{
    public class UserRegisterRepo:IUserRegister
    {  
    IOptions<ReadConfig> connectionString;
    public UserRegisterRepo(IOptions<ReadConfig> _connectionString)
    {
        connectionString = _connectionString;
    }

    #region Create Register
    public JsonResponse UserRegister(UserRegister userRegister)
    {
        JsonResponse response = new JsonResponse();
        using (SqlConnection connection = new SqlConnection(connectionString.Value.DefaultConnection))
        {
            connection.Open();
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("REGISTER_USER", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 200).Value = userRegister.UserName;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 200).Value = userRegister.Password;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = userRegister.Email;
                cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 200).Value = userRegister.Gender;
                cmd.Parameters.Add("@MobileNo", SqlDbType.Int, 200).Value = userRegister.PhoneNo;
                cmd.Parameters.Add("@Age", SqlDbType.Int, 200).Value = userRegister.Age;

                cmd.ExecuteNonQuery();

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
}
}

