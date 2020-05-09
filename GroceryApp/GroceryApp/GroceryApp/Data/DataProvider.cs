using GroceryApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.Data
{
    public class DataProvider
    {
        private static DataProvider _instance;
        public static DataProvider GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataProvider();
            }
            return _instance;
        }

        private DataProvider()
        {

        }

        public List<Store> GetListStores()
        {
            List<Store> listStores = new List<Store>();

            foreach (Store store in Database.Stores)
                if (store.IDStore != Infor.IDStore)
                    listStores.Add(store);

            return listStores;
        }

        public List<ProductType> GetProductTypes()
        {
            List<ProductType> listTypes = Database.ProductTypes;

            return listTypes;
        }

        public List<OrderBill> GetOrderBills()
        {
            List<OrderBill> orderBills = new List<OrderBill>();

            foreach (OrderBill order in Database.OrderBills)
                if (order.IDUser == Infor.IDUser)
                    orderBills.Add(order);

            return orderBills;
        }

        public Cart GetCart()
        {
            foreach (var cart in Database.Carts)
                if (cart.IDCart == Infor.IDCart)
                    return cart;

            return null;
        }

        //CART==========================================================
        public List<string> GetIDStoreFromAddedProduct()
        {
            List<string> stores = new List<string>();
            Cart myCart = this.GetCart();

            foreach(Product product in myCart.AddedProducts)
            {
                string idStore = product.IDStore;
                Boolean isExist = false;
                foreach(string id in stores)
                    if(id== idStore)
                    {
                        isExist = true;
                        break;
                    }
                if (!isExist) stores.Add(idStore);
            }

            return stores;
        }

        public string GetStoreNameFromIDStore(string IDStore)
        {
            foreach (Store store in Database.Stores)
                if (store.IDStore == IDStore)
                    return store.StoreName;
            return "";
        }


        public List<Product> GetProductInCartByIDStore(string IDStore)
        {
            List<Product> AddedProducts = this.GetCart().AddedProducts;
            List<Product> products = new List<Product>();
            foreach (Product product in AddedProducts)
                if (product.IDStore == IDStore)
                    products.Add(product);

            return products;
        }

        //==========================================================

        //SHOW STORE VIEW==========================================================
        public List<Product> GetProductByIDStore(string IDStore)
        {
            List<Product> products = new List<Product>();

            foreach (Product product in Database.Products)
                if (product.IDStore == IDStore)
                    products.Add(product);

            return products;
        }

        public List<OrderBill> GetOrderBillByIDStore(string IDStore)
        {
            List<OrderBill> orders = new List<OrderBill>();

            foreach (OrderBill order in Database.OrderBills)
                if (order.IDStore == IDStore)
                    orders.Add(order);

            return orders;
        }

        public Store GetStoreByIDStore(string IDStore)
        {
            foreach (Store store in Database.Stores)
                if (store.IDStore == IDStore)
                    return store;

            return null;
        }
        //==========================================================
    }
}
