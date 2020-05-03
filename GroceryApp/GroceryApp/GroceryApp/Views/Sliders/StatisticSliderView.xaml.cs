using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroceryApp.Views.Sliders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticSliderView : ContentView
    {

        public StatisticSliderView()
        {
            InitializeComponent();
            animate();
        }

        public async void animate()
        {
            while (true)
            {
                await order.ScaleTo(1.2, 20, Easing.Linear);
                await product.ScaleTo(1.2, 20, Easing.Linear);
                await revenue.ScaleTo(1.2, 20, Easing.Linear);
                await Task.Delay(500);
                await order.ScaleTo(1, 20, Easing.Linear);
                await product.ScaleTo(1, 20, Easing.Linear);
                await revenue.ScaleTo(1, 20, Easing.Linear);
                await Task.Delay(500);
            }
            /*
            int time = 500;
            int target = 500;
            for(int i = 1; i <= target; i++)
            {
                number.Text = i.ToString();
                await Task.Delay(1);
            }
            await number.ScaleTo(1.1, 50, Easing.Linear);
            await Task.Delay(500);
            await number.ScaleTo(1, 50, Easing.Linear);
            await Task.Delay(500);
            await number.ScaleTo(1.1, 50, Easing.Linear);
            await Task.Delay(500);
            await number.ScaleTo(1, 50, Easing.Linear);
            */
        }
    }
}