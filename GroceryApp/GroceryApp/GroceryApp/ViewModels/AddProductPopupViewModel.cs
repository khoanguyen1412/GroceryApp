using GroceryApp.Data;
using GroceryApp.Models;
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

        private Product SourceProduct;

        private bool _radioDefault;
        public bool RadioDefault
        {
            get
            {
                return _radioDefault;
            }
            set { _radioDefault = value; OnPropertyChanged(nameof(RadioDefault)); }
        }

        private bool _radioOther;
        public bool RadioOther
        {
            get
            {
                return _radioOther;
            }
            set { _radioOther = value; OnPropertyChanged(nameof(RadioOther)); }
        }

        private string _currentType;
        public string CurrentType
        {
            get
            {
                return _currentType;
            }
            set { _currentType = value; OnPropertyChanged(nameof(CurrentType)); }
        }
        public List<String> typeNames
        {
            get
            {
                return new List<string>
                {
                    "Vegetable","Fruit","Drink","Candy","Cake"
                };
            }
        }

        private string _otherUnit;
        public string OtherUnit
        {
            get
            {
                return _otherUnit;
            }
            set { _otherUnit = value; OnPropertyChanged(nameof(OtherUnit)); }
        }

        private string _currentUnit;
        public string CurrentUnit
        {
            get
            {
                return _currentUnit;
            }
            set { _currentUnit = value; OnPropertyChanged(nameof(CurrentUnit)); }
        }

        public List<String> UnitNames
        {
            get
            {
                return new List<string>
                {
                    "Kg","Gram","Cup","One"
                };
            }
        }

        private int _unitAmount;
        public int UnitAmount
        {
            get
            {
                return _unitAmount;
            }
            set { _unitAmount = value; OnPropertyChanged(nameof(UnitAmount)); }
        }

        #region Product proterties binding


        private string _imageURL;
        public string ImageURL
        {
            get { return _imageURL; }
            set { _imageURL = value; OnPropertyChanged(nameof(ImageURL)); }
        }

        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; OnPropertyChanged(nameof(ProductName)); }
        }


        private string _unitNameOther;
        public string UnitNameOther
        {
            get { return _unitNameOther; }
            set { _unitNameOther = value; OnPropertyChanged(nameof(UnitNameOther)); }
        }

        private int _quantityInventory;
        public int QuantityInventory
        {
            get { return _quantityInventory; }
            set { _quantityInventory = value; OnPropertyChanged(nameof(QuantityInventory)); }
        }

        private string _price;
        public string Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(nameof(Price)); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }
        #endregion

        #region Command and Command Function
        public ICommand ChooseDefaultCommand { get; set; }
        public ICommand ChooseOtherCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DecInventoryCommand { get; set; }
        public ICommand IncInventoryCommand { get; set; }
        public ICommand DecUnitAmountCommand { get; set; }
        public ICommand IncUnitAmountCommand { get; set; }

        public void ChooseDefault()
        {
            RadioDefault = true;
            RadioOther = false;
        }
        public void ChooseOther()
        {
            RadioDefault = false;
            RadioOther = true;
        }
        public void DecUnitAmount()
        {
            UnitAmount = UnitAmount - 1;
            if (UnitAmount < 0) UnitAmount = 0;
        }
        public void IncUnitAmount()
        {
            UnitAmount = UnitAmount + 1;
        }
        public void DecInventory()
        {
            QuantityInventory = QuantityInventory - 1;
            if (QuantityInventory < 0) QuantityInventory = 0;
        }
        public void IncInventory()
        {
            QuantityInventory = QuantityInventory + 1;
        }

        #endregion
        public AddProductPopupViewModel()
        {
            SourceProduct = new Product();
            ExitCommand = new Command(Exit);
            AddCommand = new Command(Add);
            DecInventoryCommand = new Command(DecInventory);
            IncInventoryCommand = new Command(IncInventory);
            DecUnitAmountCommand = new Command(DecUnitAmount);
            IncUnitAmountCommand = new Command(IncUnitAmount);
            ChooseDefaultCommand = new Command(ChooseDefault);
            ChooseOtherCommand = new Command(ChooseOther);

            SetValueProperties();
            //InitRadioButton();
        }

        public void SetValueProperties()
        {
            ImageURL = "cart";
            ProductName = "";
            CurrentType = "";
            RadioOther = true;
            RadioDefault = false;
            CurrentUnit = "";
            OtherUnit = "";
            UnitAmount = 0;
            QuantityInventory = 0;
            Price = "";
            Description = "";
        }

        public async void Exit()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public async void Add()
        {
            await PopupNavigation.Instance.PopAsync();
        }


        public Product GetNewProduct()
        {
            if (!CheckValidData()) return null;
            SourceProduct.IDProduct = "000000000";
            SourceProduct.StateInStore = ProductStateInStore.Selling;
            SourceProduct.IDStore = Infor.IDStore;
            SourceProduct.ImageURL = ImageURL;
            SourceProduct.ProductName = ProductName;
            SourceProduct.IDType = GetIDCurrentType();
            SourceProduct.Unit = GetUnit();
            SourceProduct.QuantityInventory = QuantityInventory;
            SourceProduct.Price = Double.Parse(Price);
            SourceProduct.ProductDescription = Description;

            return SourceProduct;
        }
        public bool CheckValidData()
        {
            bool result = true;
            if (ProductName == null || ProductName == "") return false;
            if (CurrentType == null || CurrentType == "") return false;
            if (RadioDefault)
            {
                if (CurrentUnit == null || CurrentUnit == "") return false;
            }
            else
            {
                if (OtherUnit == null || OtherUnit == "") return false;
            }

            if (Price == null || Price == "" || Double.Parse(Price) < 0) return false;

            return result;
        }
        public string GetUnit()
        {
            string result = "";
            if (UnitAmount != 0) result += UnitAmount.ToString();
            result += "#";

            if (RadioDefault) result += CurrentUnit.ToString();
            else result += OtherUnit;

            return result;
        }
        public string GetIDCurrentType()
        {
            int id = 1;
            for (int i = 0; i < typeNames.Count; i++)
                if (typeNames[i] == CurrentType)
                {
                    id = i + 1;
                    break;
                }

            return id.ToString();
        }
    }
}
