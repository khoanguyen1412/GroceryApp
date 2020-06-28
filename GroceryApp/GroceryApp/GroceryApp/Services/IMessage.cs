using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GroceryApp.Services
{
    public interface IMessage
    {
        void Longtime(string message);
        void Shorttime(string message);
    }

    public class MessageService
    {
        public static void Show(string message,int type)
        {
            if(type==0)
                DependencyService.Get<IMessage>().Shorttime(message);
            else DependencyService.Get<IMessage>().Longtime(message);
        } 
    }
}
