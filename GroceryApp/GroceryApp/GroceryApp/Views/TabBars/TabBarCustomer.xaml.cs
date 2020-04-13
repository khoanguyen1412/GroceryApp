using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace GroceryApp.Views.TabBars
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabBarCustomer : Xamarin.Forms.TabbedPage
    {
        public TabBarCustomer()
        {
            InitializeComponent();
            this.SelectedTabColor=Color.White;
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}