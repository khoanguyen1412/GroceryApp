using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.Models
{
    public class EmailVerifyItem
    {
        public string Code { get; set; }
        public DateTime SendTime { get; set; }

        public bool CheckTime()
        {
            TimeSpan duration = DateTime.Now - SendTime;
            if (duration.TotalSeconds > 120) return false;
            return true;
        }
    }
}
