using GroceryApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.Models
{
    public class User
    {
        public string IDUser { get; set; }
        public string Password { get; set; }
        public string IDStore { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ImageURL { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserName { get; set; }
        public string ExternalId { get; set; }
        public int IsLogined { get; set; }

        public bool CheckValidInfor()
        {
            //PHONE NUMBER
            if (string.IsNullOrEmpty(PhoneNumber)) return false;
            //ADDRESS
            string[] parts = Address.Split('#');
            bool empty = true;
            foreach (string part in parts)
                if (!string.IsNullOrEmpty(part))
                {
                    empty = false;
                    break;
                }
            if (empty) return false;

            //EMAIL
            if (string.IsNullOrEmpty(Email)) return false;
            if (!EmailService.CheckValidEmail(Email)) return false;

            //USERNAME
            if (string.IsNullOrEmpty(UserName)) return false;
            return true;
        }
    }
}
