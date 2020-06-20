using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Views.Screens;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroceryApp.ViewModels
{
    public interface IConfirmInforOrderPopupViewModel
    {

    }
    public class ConfirmInforOrderPopupViewModel:BaseViewModel, IConfirmInforOrderPopupViewModel
    {
        OrderBillItem OrderItem = null;
        bool emptyAddress=false;

        public Color _nextColor;
        public Color NextColor {
            get { return _nextColor; }
            set
            {
                _nextColor = value;
                OnPropertyChanged(nameof(NextColor));
            }
        }
        private bool _canNext;
        public bool CanNext
        {
            get {
                return _canNext;
                
            }
            set { 
                _canNext = value;
                if (_canNext) NextColor = Color.FromHex("#00cc00");
                else NextColor = Color.LightGray;
                OnPropertyChanged(nameof(CanNext)); 
            }
        }
        public string UserAddress {
            get {
                if(emptyAddress) return "(Chưa có dữ liệu)";

                DataProvider dataProvider = DataProvider.GetInstance();
                return dataProvider.GetUserAddress();

            }
        }

        private string _newAddress;
        public string NewAddress
        {
            get { return _newAddress; }
            set { 
                _newAddress = value;
                if (OtherAddress)
                {
                    if (_newAddress == null || _newAddress == "") CanNext = false;
                    else CanNext = true;
                }
                OnPropertyChanged(nameof(NewAddress)); 
            }
        }

        private string _note;
        public string Note
        {
            get {
                if (_note == null) return "";
                return _note; 
            }
            set { _note = value; OnPropertyChanged(nameof(Note)); }
        }

        private bool _defaultAddress;
        public bool DefaultAddress
        {
            get { return _defaultAddress; }
            set { _defaultAddress = value; OnPropertyChanged(nameof(DefaultAddress)); }
        }

        private bool _otherAddress;
        public bool OtherAddress
        {
            get { return _otherAddress; }
            set { _otherAddress = value; OnPropertyChanged(nameof(OtherAddress)); }
        }

        public ICommand ShowBillCommand { get; set; }
        public ICommand DefaultCheckCommand { get; set; }
        public ICommand OtherCheckCommand { get; set; }
        public ConfirmInforOrderPopupViewModel(OrderBillItem orderItem)
        {
            DefaultAddress = true;
            OtherAddress = false;
            CanNext = true;
            NewAddress = "";
            Note = "";
            emptyAddress = checkEmptyAddress();
            if(emptyAddress)
            {
                CanNext = false;
                DefaultAddress = false;
                OtherAddress = true;
            }

            DefaultCheckCommand = new Command(DefaultCheck);
            OtherCheckCommand = new Command(OtherCheck);

            this.OrderItem = orderItem as OrderBillItem;
            ShowBillCommand = new Command(ShowBill);
        }

        public bool checkEmptyAddress()
        {
            DataProvider dataProvider = DataProvider.GetInstance();
            string address = dataProvider.GetUserAddress();
            string[] items = address.Split('#');
            bool isEmpty = true;
            foreach (string item in items)
                if (item != "")
                {
                    isEmpty = false;
                    break;
                }
            return isEmpty;
        }

        public void DefaultCheck()
        {
            if (emptyAddress)
            {
                return;
            }
            DefaultAddress = true;
            OtherAddress = false;
            CanNext = true;
        }

        public void OtherCheck()
        {
            DefaultAddress = false;
            OtherAddress = true;
            if (NewAddress == null || NewAddress == "") CanNext = false;
            else CanNext = true;
        }


        public async void ShowBill()
        {
            if (!CanNext) return;
            var BillView = new FinalBillView();
            //set address
            if (DefaultAddress)
            {
                this.OrderItem.Order.CustomerAddress = UserAddress;
            }
            else { this.OrderItem.Order.CustomerAddress = NewAddress; }
            //set note
            this.OrderItem.Order.Note = Note;
            if (this.OrderItem.Order.Note == null) this.OrderItem.Order.Note = "";


            this.OrderItem.Order.State = OrderState.Waiting;
            BillView.BindingContext = new FinalBillViewModel(this.OrderItem);
            await PopupNavigation.Instance.PopAsync();
            await App.Current.MainPage.Navigation.PushAsync(BillView, true);
        }



        public ConfirmInforOrderPopupViewModel()
        {

            //ShowBillCommand = new Command(ShowBill);
        }
    }
}
