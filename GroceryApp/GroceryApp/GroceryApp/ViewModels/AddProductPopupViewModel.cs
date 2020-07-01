using Acr.UserDialogs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.Services;
using GroceryApp.Views.Drawer;
using GroceryApp.Views.TabBars;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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


        string ImagePath;
        bool isNewImage = false;

        private ImageSource _productImage;

        public ImageSource ProductImage
        {
            get { return _productImage; }
            set { _productImage = value; OnPropertyChanged(nameof(ProductImage)); }
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
        public ICommand ChangeImageCommand { get; set; }

        public async void ChangeImage()
        {

            try
            {
                FileData fileData = await CrossFilePicker.Current.PickFile();
                if (fileData == null)
                    return; // user canceled file picking
                string path = fileData.FilePath;

                ProductImage = (ImageSource)path;
                ImagePath = path;
                isNewImage = true;
            }
            catch (Exception ex)
            {
                var page = TabbarStoreManager.GetInstance();
                await page.DisplayAlert("Error", "Error picking file from divice, try again!", "Ok");
            }
        }
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
            ChangeImageCommand = new Command(ChangeImage);

            SetValueProperties();
            //InitRadioButton();
        }

        public void SetValueProperties()
        {
            ProductImage = "";
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
            using (UserDialogs.Instance.Loading("Adding.."))
            {
                Product NewProduct = GetNewProduct();
                if (NewProduct == null)
                {
                    var app = AppDrawer.GetInstance();
                    await app.DisplayAlert("Error", "Product's infor is missing or invalid, please check again!", "OK");
                    return;
                }

                if (ImagePath != "" && isNewImage)
                {
                    Account account = new Account(
                        "ungdung-grocery-xamarin-by-dk",
                        "378791526477571",
                        "scsyCxQS_C74MbAGdOutpwrzlnU"
                        );

                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(ImagePath)
                    };
                    try
                    {
                        using (UserDialogs.Instance.Loading("Saving.."))
                        {
                            var uploadResult = await cloudinary.UploadAsync(uploadParams);
                            string url = uploadResult.SecureUri.ToString();
                            NewProduct.ImageURL = url;
                            isNewImage = false;
                        }


                    }
                    catch (Exception ex)
                    {
                        var page = TabbarStoreManager.GetInstance();
                        await page.DisplayAlert("Error", "Error upload image to server, try again!", "Ok");
                    }
                }

                (TabbarStoreManager.GetInstance().Children.ElementAt(1).BindingContext as ProductManagerViewModel).AddProduct(NewProduct);

                await PopupNavigation.Instance.PopAsync();
            }
            MessageService.Show("Add product successfully", 0);
        }


        public Product GetNewProduct()
        {
            if (!CheckValidData()) return null;
            SourceProduct.IDProduct = "Product_"+ DateTime.Now.ToString("HHmmss");
            SourceProduct.StateInStore = ProductStateInStore.Selling;
            SourceProduct.IDStore = Infor.IDStore;
            //SourceProduct.ImageURL = ImageURL;
            SourceProduct.ProductName = ProductName;
            SourceProduct.IDType = GetIDCurrentType();
            SourceProduct.Unit = GetUnit();
            SourceProduct.QuantityInventory = QuantityInventory;
            SourceProduct.Price = Double.Parse(Price);
            SourceProduct.ProductDescription = Description;
            SourceProduct.State = ProductState.InStore;
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
            result += UnitAmount.ToString() + "#";

            if (RadioDefault) result += CurrentUnit;
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
