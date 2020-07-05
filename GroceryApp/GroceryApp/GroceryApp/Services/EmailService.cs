using Acr.UserDialogs;
using GroceryApp.Data;
using GroceryApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace GroceryApp.Services
{
    public class EmailService
    {
        public static bool CheckValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool CheckExistedEmail(string email)
        {
            foreach (User user in Database.Users)
                if (user.Email == email) return true;
            return false;
        }

        public static EmailVerifyItem SendCodeToEmail(string email)
        {
            string code = "";
            using (UserDialogs.Instance.Loading("Sending code.."))
            {

                code = CreateCode();

                string content = "This is a GroceryApp's automatic email for verification, enter this code to reset your password: " + code + ". The code is valid for 2 minutes.";
                MailMessage mail = new MailMessage("kdsoftverify@gmail.com", email, "Reset mật khẩu Grocery app", content);
                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Host = "smtp.gmail.com";
                client.UseDefaultCredentials = false;
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential("kdsoftverify@gmail.com", "@quenmkroi");
                client.EnableSsl = true;
                client.Send(mail);


            }
            EmailVerifyItem emailItem = new EmailVerifyItem
            {
                Code = code,
                SendTime = DateTime.Now
            };
            return emailItem ;
        }

        public static string CreateCode()
        {
            char[] chars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            Random rd = new Random();
            string resetcode = "";
            for (int i = 0; i < 6; i++)
            {
                int a = rd.Next(0, 36);
                resetcode += chars[a];
            }

            return resetcode;
        }
    }
}
