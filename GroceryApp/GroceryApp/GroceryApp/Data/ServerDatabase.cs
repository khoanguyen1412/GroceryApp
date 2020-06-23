using GroceryApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GroceryApp.Data
{
    public class ServerDatabase
    {
        public static string localhost = "http://192.168.137.1:3000/";

        public static List<User> Users = new List<User>();
        public static List<Store> Stores = new List<Store>();
        public static List<ProductType> ProductTypes = new List<ProductType>();
        public static List<Product> Products = new List<Product>();
        public static List<OrderBill> OrderBills = new List<OrderBill>();

        public static async Task LoadDataFromServer()
        {
            if (Users.Count > 0) return;
            var httpClient = new HttpClient();
            JsonSerializer serializer = new JsonSerializer();

            //GET PRODUCTTYPES
            var TypeResponse = await httpClient.GetStringAsync(ServerDatabase.localhost + "producttype/getallproducttype");
            JObject TypeResponseObj = JObject.Parse(TypeResponse);
            try
            {
                int count = 0;
                while (true)
                {
                    ProductType newType = (ProductType)serializer.Deserialize(new JTokenReader(TypeResponseObj["result"][count]), typeof(ProductType));
                    ProductTypes.Add(newType);
                    count++;
                }
            }
            catch (Exception e)
            {
                //Đã đọc hết data
            }

            //GET USERS
            var UserResponse = await httpClient.GetStringAsync(ServerDatabase.localhost+ "user/getalluser");
            JObject UserResponseObj = JObject.Parse(UserResponse);
            try
            {
                int count = 0;
                while (true)
                {
                    User newUser = (User)serializer.Deserialize(new JTokenReader(UserResponseObj["result"][count]), typeof(User));
                    Users.Add(newUser);
                    count++;
                }
            }
            catch (Exception e)
            {
                //Đã đọc hết data
            }

            //GET PRODUCTS
            var ProductResponse = await httpClient.GetStringAsync(ServerDatabase.localhost + "product/getallproduct");
            JObject ProductResponseObj = JObject.Parse(ProductResponse);
            try
            {
                int count = 0;
                while (true)
                {
                    Product newProduct = (Product)serializer.Deserialize(new JTokenReader(ProductResponseObj["result"][count]), typeof(Product));
                    Products.Add(newProduct);
                    count++;
                }
            }
            catch (Exception e)
            {
                //Đã đọc hết data
            }

            //GET STORES
            var StoreResponse = await httpClient.GetStringAsync(ServerDatabase.localhost + "store/getallstore");
            JObject StoreResponseObj = JObject.Parse(StoreResponse);
            try
            {
                int count = 0;
                while (true)
                {
                    Store newStore = (Store)serializer.Deserialize(new JTokenReader(StoreResponseObj["result"][count]), typeof(Store));
                    Stores.Add(newStore);
                    count++;
                }
            }
            catch (Exception e)
            {
                //Đã đọc hết data
            }

            

            //GET ORDERBILLS
            var OrderBillResponse = await httpClient.GetStringAsync(ServerDatabase.localhost + "orderbill/getallorderbill");
            JObject OrderBillResponseObj = JObject.Parse(OrderBillResponse);
            try
            {
                int count = 0;
                while (true)
                {
                    OrderBill newOrderBill = (OrderBill)serializer.Deserialize(new JTokenReader(OrderBillResponseObj["result"][count]), typeof(OrderBill));
                    OrderBills.Add(newOrderBill);
                    count++;
                }
            }
            catch (Exception e)
            {
                //Đã đọc hết data
            }            

        }

        public static async Task FetchProductData()
        {
            var httpClient = new HttpClient();
            JsonSerializer serializer = new JsonSerializer();
            List<Product> newProducts = new List<Product>();

            var ProductResponse = await httpClient.GetStringAsync(ServerDatabase.localhost + "product/getallproduct");
            JObject ProductResponseObj = JObject.Parse(ProductResponse);
            try
            {
                int count = 0;
                while (true)
                {
                    Product newProduct = (Product)serializer.Deserialize(new JTokenReader(ProductResponseObj["result"][count]), typeof(Product));
                    newProducts.Add(newProduct);
                    count++;
                }
            }
            catch (Exception e)
            {
                //Đã đọc hết data
            }

            Products = newProducts;
            Database.Products = newProducts;
        }
    }
}
