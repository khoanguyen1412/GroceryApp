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
                if (store.IDStore != Infor.IDStore && store.IsActive==1)
                    listStores.Add(store);

            return listStores;
        }

        public List<ProductType> GetProductTypes()
        {
            List<ProductType> listTypes = Database.ProductTypes;

            return listTypes;
        }

        public List<OrderBill> GetMyOrderBills()
        {
            List<OrderBill> orderBills = new List<OrderBill>();
            foreach (OrderBill order in Database.OrderBills)
                if (order.IDUser == Infor.IDUser)
                    orderBills.Add(order);
            return orderBills;
        }

        public List<Product> GetProductsInCart()
        {
            List<Product> addedProducts = new List<Product>();
            foreach (Product product in Database.Products)
                if (product.State == ProductState.InCart && product.IDCart == Infor.IDUser)
                    addedProducts.Add(product);

            return addedProducts;
        }

        //CART==========================================================

        public Product GetProductByID(string ID)
        {
            foreach (Product product in Database.Products)
                if (product.IDProduct == ID) return product;
            return null;
        }
        public List<string> GetIDStoreFromAddedProduct()
        {
            List<string> stores = new List<string>();
            List<Product> AddedProducts = this.GetProductsInCart();

            foreach (Product product in AddedProducts)
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
            List<Product> AddedProducts = this.GetProductsInCart();
            List<Product> products = new List<Product>();
            foreach (Product product in AddedProducts)
                if (product.IDStore == IDStore )
                    products.Add(product);

            return products;
        }

        public Product GetProductInCartByIDSourceProduct(string idSource)
        {
            List<Product> productIncart= this.GetProductsInCart();
            foreach (Product product in productIncart)
                if (product.IDSourceProduct == idSource)
                    return product;
            return null;
        }

        public bool CheckExistInMyCart(Product checkedProduct)
        {
            foreach (Product product in Database.Products)
                if (product.State == ProductState.InCart && product.IDCart == Infor.IDUser && product.IDSourceProduct == checkedProduct.IDSourceProduct)
                    return true;

            return false;
        }

        public List<Product> GetExistProducInCarttByListProduct(List<Product> inputProducts)
        {
            List<Product> result = new List<Product>();
            foreach(Product product in Database.Products)
                if(product.State==ProductState.InCart && product.IDCart==Infor.IDUser)
                {
                    foreach(Product inputProduct in inputProducts)
                        if(product.IDSourceProduct==inputProduct.IDSourceProduct)
                        {
                            result.Add(product);
                            break;
                        }
                }

            return result;
        }

        //==========================================================

        //SHOW STORE VIEW==========================================================
        public List<Product> GetProductByIDStore(string IDStore)
        {
            List<Product> products = new List<Product>();

            foreach (Product product in Database.Products)
                if (product.State==ProductState.InStore && product.IDStore == IDStore && product.StateInStore!=ProductStateInStore.Hidden)
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

        //LIST STORE VIEW==========================================================
        public bool isTypeInStore(string IDType, string IDStore)
        {
            bool result = false;
            List<Product> products = GetProductByIDStore(IDStore);
            foreach(Product product in products)
                if(product.IDType==IDType)
                {
                    result = true;
                    break;
                }
            return result;
        }

        //==========================================================

        //CART VIEW==========================================================
        public string GetUserAddress()
        {
            foreach (User user in Database.Users)
                if(user.IDUser==Infor.IDUser)
                {
                    return user.Address;
                }

            return "";
        }

        //==========================================================

        //DASH BOARD VIEW==========================================================
        public List<ItemChart> GetDataForChart()
        {
            List<OrderBill> orders = this.GetOrderBillByIDStore(Infor.IDStore);

            List<ItemChart> itemCharts = new List<ItemChart>();

            int currentMonth = DateTime.Today.Month;
            int LastSixMonth = currentMonth - 5;
            for(int i = LastSixMonth; i <= currentMonth; i++)
            {
                int month = i;
                int year = DateTime.Today.Year;
                if (month == 0)
                {
                    month = 12;
                    year -= 1;
                }
                if (month < 0)
                {
                    month = 12 + i;
                    year -= 1;
                }

                int countOrder = 0;
                foreach(OrderBill order in orders)
                {
                    DateTime orderDate = order.Date;
                    if (orderDate.Year == year && orderDate.Month == month)
                        countOrder++;
                }

                ItemChart itemChart = new ItemChart
                {
                    Month = month,
                    NumberOrder = countOrder,
                };

                itemCharts.Add(itemChart);
            }
            return itemCharts;
        }

        public int GetNumberProductOutOfStock()
        {
            List<Product> products = this.GetProductByIDStore(Infor.IDStore);
            int count = 0;
            foreach (Product product in products)
                if (product.QuantityInventory == 0)
                    count++;
            return count;
        }

        public int GetNumberNewOrder()
        {
            List<OrderBill> orders = this.GetOrderBillByIDStore(Infor.IDStore);
            int count = 0;
            foreach (OrderBill order in orders)
                if (order.State == OrderState.Waiting)
                    count++;

            return count;
        }

        public int GetNumberNewFeedback()
        {
            List<OrderBill> orders = this.GetOrderBillByIDStore(Infor.IDStore);
            int count = 0;
            foreach (OrderBill order in orders)
                if (order.State == OrderState.Received && order.Review != null && order.Review != "" &&
                    (order.StoreAnswer == null || order.StoreAnswer == ""))
                    count++;

            return count;
        }

        public List<OrderBill> GetMyReviewedOrder()
        {
            List<OrderBill> orders = this.GetOrderBillByIDStore(Infor.IDStore);
            List<OrderBill> reviewedOrders = new List<OrderBill>();

            foreach (OrderBill order in orders)
                if (order.State==OrderState.Received && order.Review != null && order.Review != "")
                    reviewedOrders.Add(order);

            return reviewedOrders;
        }

        public User GetUserByIDUser(string IDUser)
        {
            foreach (User user in Database.Users)
                if (user.IDUser == IDUser)
                    return user;
            return null;
        }

        //==========================================================

        //PRODUCT MANAGER VIEW==========================================================
        public List<Product> GetProductOfMyStore()
        {
            List<Product> products = new List<Product>();

            foreach (Product product in Database.Products)
                if (product.State==ProductState.InStore && product.IDStore == Infor.IDStore)
                    products.Add(product);

            return products;
        }

        //==========================================================
        //ORDER MANAGER VIEW==========================================================
        public List<OrderBill> GetOrderBillsOfCustomer()
        {
            List<OrderBill> orderBills = new List<OrderBill>();

            foreach (OrderBill order in Database.OrderBills)
                if (order.IDStore == Infor.IDStore)
                    orderBills.Add(order);

            return orderBills;
        }

        //==========================================================
        //REVIEW MANAGER VIEW==========================================================
        public List<ReviewItem> GetReviewOfMyStore()
        {
            List<ReviewItem> result = new List<ReviewItem>();
            foreach(OrderBill order in Database.OrderBills)
                if(order.State==OrderState.Received && order.IDStore==Infor.IDStore && 
                    order.Review!=null && order.Review!="")
                {
                    ReviewItem review = new ReviewItem
                    {
                        IDOrderBill=order.IDOrderBill,
                        CustomerImage = this.GetUserByIDUser(order.IDUser).ImageURL,
                        CustomerName = this.GetUserByIDUser(order.IDUser).UserName,
                        Date = order.Date,
                        Content = order.Review,
                        StoreAnswer = order.StoreAnswer,
                        Rating = order.Rating
                    };

                    review.SetStar();
                    result.Add(review);
                }

            return result;
        }

        public List<Product> GetProductsInBillByIDBill(string IDOrderBill)
        {
            List<Product> productsInBill = new List<Product>();
            foreach (Product product in Database.Products)
                if (product.State == ProductState.InBill && product.IDOrderBill == IDOrderBill)
                    productsInBill.Add(product);

            return productsInBill;
        }

        public OrderBill GetOrderBillByIDOrderBill(string IDOrderBill)
        {
            foreach (OrderBill order in Database.OrderBills)
                if (order.IDOrderBill == IDOrderBill)
                {
                    order.Init();
                    return order;
                }

            return null;
        }
        //==========================================================
        //LOGIN VIEW==========================================================
        public bool CheckUserExist(string IDUser)
        {
            foreach (User user in Database.Users)
                if (user.IDUser == IDUser) return true;
            return false;
        }

        public string GetIDStoreByIDUser(string IDUser)
        {
            foreach (User user in Database.Users)
                if (user.IDUser == IDUser) return user.IDStore;
            return null;
        }
        //==========================================================

    }
}
