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
    }
}
