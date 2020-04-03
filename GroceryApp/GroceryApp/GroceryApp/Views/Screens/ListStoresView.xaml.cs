using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroceryApp.Views.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListStoresView : ContentPage
    {
        public ListStoresView()
        {
            InitializeComponent();
            
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            List.SelectedItem = -1;
            
        }

        private void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            List.SelectedItem = null;
        }
    }
}