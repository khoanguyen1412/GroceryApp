using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GroceryApp.Services
{
    public class InternetService
    {
        public static async System.Threading.Tasks.Task<bool> TestConnectionIsOK()
        {
            var httpClient = new HttpClient();
            try
            {
                var testInternet = await httpClient.GetStringAsync("https://newappgroc.azurewebsites.net/store/getstorebyid/test");

            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
