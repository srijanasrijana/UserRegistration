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
                MD5 md5 = MD5.Create();
                byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(textToBeEncrypted);
                byte[] hash = md5.ComputeHash(bytes);

                StringBuilder sbPassword = new StringBuilder();
                for (int b = 0; b < hash.Length; b++)
                {
                    sbPassword.Append(hash[b].ToString("X2"));
                }

                return sbPassword.ToString();
            }
            else
            {
                return string.Empty;

            }
        }

        public static byte[] dataURL2byte(string dataurl)
        {
            string[] dataURL = dataurl.Split(new char[] { ',' }, 2);

            byte[] urlByte = Convert.FromBase64String(dataURL[1]);
            return urlByte;
        }

        public static string byte2dataURL(byte[] urlByte)
        {
            string dataURL = Convert.ToBase64String(urlByte);
            return dataURL;
        }
    }
}

