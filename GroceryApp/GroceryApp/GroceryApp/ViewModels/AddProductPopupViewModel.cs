using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IAddProductPopupViewModel
    {

    }
    public class AddProductPopupViewModel : BaseViewModel, IAddProductPopupViewModel
    {
        public ICommand ExitCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public AddProductPopupViewModel()
        {
            ExitCommand = new Command(Exit);
            AddCommand = new Command(Add);
        }

        public async void Exit()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public async void Add()
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
