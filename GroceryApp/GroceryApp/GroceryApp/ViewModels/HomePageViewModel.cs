using GroceryApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GroceryApp.ViewModels
{
    public interface IHomePageViewModel
    {

    }
    public class HomePageViewModel: BaseViewModel, IHomePageViewModel
    {
        private ObservableCollection<Store> _stores;

        public ObservableCollection<Store> Stores
        {
            get { return _stores; }
            set { _stores = value; OnPropertyChanged(nameof(Stores)); }
        }
        public HomePageViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            List<Store> stores = new List<Store>
            {
                new Store{ID=0,StoreName="Groceriers",ImageURL="https://www.iowapublicradio.org/sites/ipr/files/201904/grocery-store-2119702_1920.jpg", StoreAddress="",StoreDescription="A lot of fresh vegetables"},
                new Store{ID=1,StoreName="Fruits Kingdom",ImageURL="https://st3.depositphotos.com/1346781/17800/i/1600/depositphotos_178006858-stock-photo-a-wide-variety-of-fresh.jpg", StoreAddress="",StoreDescription="All kind of fruits"},
                new Store{ID=2,StoreName="Candy World",ImageURL="https://bluebiz-media.azureedge.net/4a3743/contentassets/24911eb5727c4be7a1b354db9b16e9dd/bluebook_sweets_624x380.jpg", StoreAddress="",StoreDescription="Candy, fruits and more"},
                new Store{ID=3,StoreName="Funny Bakers",ImageURL="https://saohanoi.vn/wp-content/uploads/2018/10/bang-hieu-banh-kem-8.jpeg", StoreAddress="",StoreDescription="Serve Bakes and Sweet food"},
                new Store{ID=4,StoreName="Coronas",ImageURL="https://api.time.com/wp-content/uploads/2020/02/corona-beer-virus-.jpg", StoreAddress="",StoreDescription="Get viruss away"},

            };

            _stores = new ObservableCollection<Store>(stores);
        }
    }
}
