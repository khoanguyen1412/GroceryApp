using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IEditProductPopupViewModel
    {

    }
    public class EditProductPopupViewModel : BaseViewModel, IEditProductPopupViewModel
    {
        public ICommand ExitCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public EditProductPopupViewModel()
        {
            ExitCommand = new Command(Exit);
            SaveCommand = new Command(Save);
        }

        public async void Exit()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public async void Save()
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
