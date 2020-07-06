using Acr.UserDialogs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GroceryApp.Models;
using GroceryApp.Services;
using GroceryApp.Views.Drawer;
using GroceryApp.Views.TabBars;
using ImTools;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
                    "Fruit","Vegetable","Drink","Candy","Cake"
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

        private string _iDType;
        public string IDType
        {
            get { return _iDType; }
            set { _iDType = value; OnPropertyChanged(nameof(IDType)); }
        }

        private string _unitNameSelect;
        public string UnitNameSelect
        {
            get { return _unitNameSelect; }
            set { _unitNameSelect = value; OnPropertyChanged(nameof(UnitNameSelect)); }
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

        #region Command and command function

        public ICommand ChooseDefaultCommand { get; set; }
        public ICommand ChooseOtherCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DecInventoryCommand { get; set; }
        public ICommand IncInventoryCommand { get; set; }
        public ICommand DecUnitAmountCommand { get; set; }
        public ICommand IncUnitAmountCommand { get; set; }
        public ICommand ChangeImageCommand { get; set; }

        public async void ChangeImage()
        {

            try
            {
                string[] types = { "image/*" };
                FileData fileData = await CrossFilePicker.Current.PickFile(types);
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

        public async void Exit()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public Product GetUpdatedProduct()
        {
            if (!CheckValidData()) return null;
 
            //SourceProduct.ImageURL = ImageURL;
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
            if (RadioDefault)
            {
                if (CurrentUnit == null || CurrentUnit == "") return false;
            }
            else
            {
                if (OtherUnit == null || OtherUnit == "") return false;
            }

            if (Price == null || Price==""|| Double.Parse(Price)<0) return false;

            return result;
        }

        public async void Save()
        {
            //TEST INTERNET CONNECTTION 
            var httpClient = new HttpClient();
            string x = "";
            try
            {
                var testInternet = await httpClient.GetStringAsync("https://newappgroc.azurewebsites.net/store/getstorebyid/test");
                x = testInternet;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Action fail, check your internet connection and try again!", "OK");
                return;
            }

            using (UserDialogs.Instance.Loading("Updating.."))
            {
                Product UpdatedProduct = GetUpdatedProduct();
                if (UpdatedProduct == null)
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
                            UpdatedProduct.ImageURL = url;
                            isNewImage = false;
                        }


                    }
                    catch (Exception ex)
                    {
                        var page = TabbarStoreManager.GetInstance();
                        await page.DisplayAlert("Error", "Error upload image to server, try again!", "Ok");
                    }
                }

                (TabbarStoreManager.GetInstance().Children.ElementAt(1).BindingContext as ProductManagerViewModel).UpdateProduct(UpdatedProduct);

                await PopupNavigation.Instance.PopAsync();
            }
            MessageService.Show("Update product successfully", 0);
        }

        public string GetUnit()
        {
            string result = "";
            result += UnitAmount.ToString()+ "#";

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

        #endregion
        public EditProductPopupViewModel(Product product)
        {
            SourceProduct = product;
            ExitCommand = new Command(Exit);
            SaveCommand = new Command(Save);
            DecInventoryCommand = new Command(DecInventory);
            IncInventoryCommand = new Command(IncInventory);
            DecUnitAmountCommand = new Command(DecUnitAmount);
            IncUnitAmountCommand = new Command(IncUnitAmount);
            ChooseDefaultCommand = new Command(ChooseDefault);
            ChooseOtherCommand = new Command(ChooseOther);
            ChangeImageCommand = new Command(ChangeImage);

            SetValueProperties(product);


            //xác định currentType
            CurrentType = typeNames[(Int16.Parse(product.IDType) - 1)];

            InitRadioButton();
        }

        public void SetValueProperties(Product product)
        {
            OtherUnit = "";

            if (SourceProduct.ImageURL == null || SourceProduct.ImageURL=="") ProductImage = "";
            else ProductImage = SourceProduct.ImageURL;

            if (SourceProduct.ProductName == null) ProductName = "";
            else ProductName = SourceProduct.ProductName;

            QuantityInventory = SourceProduct.QuantityInventory;
            Price = SourceProduct.Price.ToString();

            if (SourceProduct.ProductDescription == null) Description = "";
            else Description = SourceProduct.ProductDescription;
        }

        public void InitRadioButton()
        {
            RadioDefault = false;
            string[] UnitItems = SourceProduct.Unit.Split('#');
            UnitAmount = (Int16.Parse(UnitItems[0].ToString()));

            for (int i = 0; i < UnitNames.Count; i++)
                if (UnitNames[i] == UnitItems[1])
                {
                    CurrentUnit = UnitNames[i];
                    RadioDefault = true;
                    break;
                }

            if (!RadioDefault) OtherUnit = UnitItems[1];
            RadioOther = !RadioDefault;
        }

        public EditProductPopupViewModel()
        {
            //NO USE
        }
    }
}
