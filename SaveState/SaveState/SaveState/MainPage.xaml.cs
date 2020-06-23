using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace SaveState
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        DateTime dt = new DateTime();
        public MainPage()
        {
            InitializeComponent();
            EntUserName.Text = Preferences.Get("username", string.Empty);
            EntPassword.Text = Preferences.Get("password", string.Empty);
        }

        private void button_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("xin chào", "hfghfff", "dffff");
            Preferences.Set("username", EntUserName.Text);
            Preferences.Set("password", EntPassword.Text);
        }

        private void buttonForgot_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EntEmail.Text))
            {
                dt = DateTime.Now;
                DisplayAlert("Note", dt.ToString("HH:mm:ss"), "OK");
                char[] chars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                Random rd = new Random();
                string resetcode = "";
                for (int i = 0; i < 6; i++)
                {
                    int a = rd.Next(0, 36);
                    resetcode += chars[a];
                }



                string content = "Đây là email được gửi tự động, vui lòng nhập mã sau để reset lại mật khẩu: " + resetcode;
                MailMessage mail = new MailMessage("kdsoftverify@gmail.com", EntEmail.Text, "Reset mật khẩu grocery app", content);
                mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Host = "smtp.gmail.com";
                client.UseDefaultCredentials = false;
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential("kdsoftverify@gmail.com", "@quenmkroi");
                client.EnableSsl = true;
                client.Send(mail);
            }
            else
                DisplayAlert("Note", "Email rỗng", "OK");
            
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            DateTime newdt = DateTime.Now;
            TimeSpan sub = newdt-dt;
            DisplayAlert("Total", sub.TotalMinutes + "/"+newdt.ToString("HH:mm:ss"), "OK");
        }
    }
}
