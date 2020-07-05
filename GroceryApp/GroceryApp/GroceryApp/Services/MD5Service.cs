using ImTools;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.Services
{
    public class MD5Service
    {
        public static string EncodeToMD5(string inputStr)
        {
            string result = "";
            StringBuilder sb = new StringBuilder();
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(inputStr);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

            }
            result = sb.ToString();
            return result;
        }
    }
}
