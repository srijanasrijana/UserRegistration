using System;
using System.Security.Cryptography;
using System.Text;

namespace UserRegistrationTask.Utilities
{
    public class Crypto
    {
        public static string OneWayEncryter(string textToBeEncrypted)
        {
            if (!String.IsNullOrEmpty(textToBeEncrypted))
            {
                //To create object of MD5
                MD5 md5 = MD5.Create();
                //get bytes for original password and compute hash(encoded password)
                byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(textToBeEncrypted);
                byte[] hash = md5.ComputeHash(bytes);

                //To convert byte to string
                StringBuilder sbPassword = new StringBuilder();
                for (int b = 0; b < hash.Length; b++)
                {
                    //change it into 2 hexadecimal digits  
                    //for each byte 
                    sbPassword.Append(hash[b].ToString("X2"));
                }

                return sbPassword.ToString();
            }
            else
            {
                return string.Empty;

            }
        }

        //public static byte[] dataURL2byte(string dataurl)
       // {
           // string[] dataURL = dataurl.Split(new char[] { ',' }, 2);

           // byte[] urlByte = Convert.FromBase64String(dataURL[1]);
            //return urlByte;
       // }

        //public static string byte2dataURL(byte[] urlByte)
       // {
          //  string dataURL = Convert.ToBase64String(urlByte);
          //  return dataURL;
        //}
    }
}

