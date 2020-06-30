using CloudinaryDotNet.Actions;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using GroceryApp.Data;
using GroceryApp.Models;
using GroceryApp.ViewModels;
using GroceryApp.Views.Screens;
using GroceryApp.Views.TabBars;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;

namespace GroceryApp.Services
{
    public class PushNotificationService
    {
        public static void HandleNotificationReceived(OSNotification notification)
        {
            OSNotificationPayload payload = notification.payload;
            string message = payload.body;
            Console.WriteLine("message là: " + message);
            if (string.IsNullOrEmpty(message))
            {
                Console.WriteLine("noti trống");
            }

            var json = notification.payload.additionalData["content"].ToString();
            string[] datas = json.Split('~');
            switch (datas[1])
            {
                case "1":
                    AddToCartAction(datas[0]);
                    break;
                case "2":
                    InsertOrderBillAction(datas[0]);
                    break;
                case "3":
                    ReturnProductCartAction(datas[0]);
                    break;
                case "4":
                    CancelOrderAction(datas[0]);
                    break;
                case "5":
                    ReceiveOrderAction(datas[0]);
                    break;
                case "6":
                    UpdateProductAction(datas[0]);
                    break;
                case "7":
                    AddProductAction(datas[0]);
                    break;
                case "8":
                    AnswerFeedbackAction(datas[0]);
                    break;
                case "9":
                    DeliverOrderAction(datas[0]);
                    break;
                case "10":
                    UpdateStoreAction(datas[0]);
                    break;
                case "11":
                    UpdateUserAction(datas[0]);
                    break;
                case "12":
                    LoginAction(datas[0]);
                    break;
            }
            


        }
        #region process received data from Notification
        public static void LoginAction(string data)
        {
            User user = JsonConvert.DeserializeObject<User>(data);
            DataUpdater.UpdateUser(user);

            OneSignal.Current.SetExternalUserId(user.IDUser);
            OneSignal.Current.SendTag("IsLogined", "0");
            Preferences.Set("IDLogin", "");
            App.Current.MainPage.DisplayAlert("Notice", "This account has been signed up in another device!", "OK");
            App.Current.MainPage.Navigation.PushAsync(new LogoutView());

        }
        public static void AddToCartAction(string data)
        {
            List<Product> receivedProducts = JsonConvert.DeserializeObject<List<Product>>(data);
            DataUpdater.UpdateProduct(receivedProducts);
            if(receivedProducts[0].IDStore==Infor.IDStore)
                (TabbarStoreManager.GetInstance().Children.ElementAt(1).BindingContext as ProductManagerViewModel).LoadData(false);
        }

        public async static void InsertOrderBillAction(string data)
        {
            //fetch data products in server
            await ServerDatabase.FetchProductData();
            OrderBill newOrder = JsonConvert.DeserializeObject<OrderBill>(data);
            DataUpdater.InsertOrderBill(newOrder);
            if(Infor.IDStore==newOrder.IDStore)
                (TabbarStoreManager.GetInstance().Children.ElementAt(2).BindingContext as OrderManagerViewModel).LoadData();

        }

        public static void ReturnProductCartAction(string data)
        {
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(data);
            DataUpdater.UpdateProduct(products[0]);
            products.RemoveAt(0);
            DataUpdater.DeleteProducts(products);
            //Load lại data cho các user đang ở trong cửa hàng đó
            var ShowStoreVM = ShowStoreView.GetInstance().BindingContext as ShowStoreViewModel;
            if (ShowStoreVM != null && ShowStoreVM.IDStore == products[0].IDStore) ShowStoreVM.LoadData(true);
            //Load lại list product cho cửa hàng đó
            if(products[0].IDStore==Infor.IDStore)
                (TabbarStoreManager.GetInstance().Children.ElementAt(1).BindingContext as ProductManagerViewModel).LoadData(true);

        }

        public async static void CancelOrderAction(string data)
        {
            OrderBill canceledOder= JsonConvert.DeserializeObject<OrderBill>(data);

            //fetch data products in server
            await ServerDatabase.FetchProductData();

            //xóa orderbill
            DataUpdater.DeleteOrderBillByID(canceledOder.IDOrderBill);

            //update list order+số lượng product cho cửa hàng bị hủy order
            if (Infor.IDStore == canceledOder.IDStore)
            {
                (TabbarStoreManager.GetInstance().Children.ElementAt(1).BindingContext as ProductManagerViewModel).LoadData(true);
                (TabbarStoreManager.GetInstance().Children.ElementAt(2).BindingContext as OrderManagerViewModel).LoadData();

            }

            if (Infor.IDUser == canceledOder.IDUser)
            {
                (TabBarCustomer.GetInstance().Children.ElementAt(3).BindingContext as ListOrdersViewModel).LoadData();


            }

            //update số lượng product cho các user khác (gồm cả user là store bị hủy)
            var ShowStoreVM = ShowStoreView.GetInstance().BindingContext as ShowStoreViewModel;
            if (ShowStoreVM != null && ShowStoreVM.IDStore == canceledOder.IDStore) ShowStoreVM.LoadData(true);

        }

        public static void ReceiveOrderAction(string data)
        {
            OrderBill order = JsonConvert.DeserializeObject<OrderBill>(data);
            DataUpdater.UpdateOrderBill(order);
            //nếu user không phải là store của order thì thoát
            if (order.IDStore != Infor.IDStore) return;
            //update màn hình list order
            (TabbarStoreManager.GetInstance().Children.ElementAt(2).BindingContext as OrderManagerViewModel).LoadData();
            (TabbarStoreManager.GetInstance().Children.ElementAt(3).BindingContext as ReviewManagerViewModel).LoadData();
        }

        public static void UpdateProductAction(string data)
        {
            Product product = JsonConvert.DeserializeObject<Product>(data);
            DataUpdater.UpdateProduct(product);
            //update hết tất cả những user đang ở trong màn hình xem cửa hàng (của cửa hàng update product)
            var ShowStoreVM = ShowStoreView.GetInstance().BindingContext as ShowStoreViewModel;
            if (ShowStoreVM != null && ShowStoreVM.IDStore == product.IDStore) ShowStoreVM.LoadData(true);

        }

        public static void AddProductAction(string data)
        {
            Product product = JsonConvert.DeserializeObject<Product>(data);
            DataUpdater.AddProduct(product);
            //update hết tất cả những user đang ở trong màn hình xem cửa hàng (của cửa hàng update product)
            var ShowStoreVM = ShowStoreView.GetInstance().BindingContext as ShowStoreViewModel;
            if (ShowStoreVM != null && ShowStoreVM.IDStore == product.IDStore) ShowStoreVM.LoadData(true);
        }

        public static void AnswerFeedbackAction(string data)
        {
            OrderBill order = JsonConvert.DeserializeObject<OrderBill>(data);
            DataUpdater.UpdateOrderBill(order);

            //Update lại cho tất cả user: list feedback của cửa hàng đó nếu họ đang xem
            var ShowStoreVM = ShowStoreView.GetInstance().BindingContext as ShowStoreViewModel;
            if (ShowStoreVM != null && ShowStoreVM.IDStore == order.IDStore) ShowStoreVM.LoadData(true);
            //update lại list orders cho user là người mua của order được trả lời feedback
            (TabBarCustomer.GetInstance().Children.ElementAt(3).BindingContext as ListOrdersViewModel).LoadData();

        }

        public static void DeliverOrderAction(string data)
        {
            OrderBill order = JsonConvert.DeserializeObject<OrderBill>(data);
            DataUpdater.UpdateOrderBill(order);
            //Chỉ load lại màn hình list order cho user là người đặt order
            if (order.IDUser != Infor.IDUser) return;
            (TabBarCustomer.GetInstance().Children.ElementAt(3).BindingContext as ListOrdersViewModel).LoadData();

        }

        public static void UpdateStoreAction(string data)
        {
            Store store = JsonConvert.DeserializeObject<Store>(data);
            DataUpdater.UpdateStore(store);
            //load lại list store view cho tất cả user
            (TabBarCustomer.GetInstance().Children.ElementAt(1).BindingContext as ListStoresViewModel).LoadData();
            (TabbarStoreManager.GetInstance().Children.ElementAt(0).BindingContext as StoreDashBoardViewModel).LoadData();

        }

        public static void UpdateUserAction(string data)
        {
            User user = JsonConvert.DeserializeObject<User>(data);
            DataUpdater.UpdateUser(user);
            //load lại list review cho tất cả user
            (TabbarStoreManager.GetInstance().Children.ElementAt(3).BindingContext as ReviewManagerViewModel).LoadData();

        }

        #endregion



        #region create json data
        public static string ConvertDataAddToCart(List<Product> changedProducts)
        {
            var json = JsonConvert.SerializeObject(changedProducts);
            json += "~1";
            return json;
        }

        public static string ConvertDataInsertOrderBill(OrderBill order)
        {
            var json = JsonConvert.SerializeObject(order);
            json += "~2";
            return json;
        }

        public static string ConvertDataReturnProductCart(List<Product> products)
        {
            var json = JsonConvert.SerializeObject(products);
            json += "~3";
            return json;
        }

        public static string ConvertDataCancelOrder(OrderBill order)
        {
            var json = JsonConvert.SerializeObject(order);
            json += "~4";
            return json;
        }

        public static string ConvertDataReceiveOrder(OrderBill order)
        {
            var json = JsonConvert.SerializeObject(order);
            json += "~5";
            return json;
        }

        public static string ConvertDataUpdateProduct(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            json += "~6";
            return json;
        }
        public static string ConvertDataAddProduct(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            json += "~7";
            return json;
        }
        public static string ConvertDataAnswerFeedback(OrderBill order)
        {
            var json = JsonConvert.SerializeObject(order);
            json += "~8";
            return json;
        }

        public static string ConvertDataDeliverOrder(OrderBill order)
        {
            var json = JsonConvert.SerializeObject(order);
            json += "~9";
            return json;
        }

        public static string ConvertDataUpdateStore(Store store)
        {
            var json = JsonConvert.SerializeObject(store);
            json += "~10";
            return json;
        }

        public static string ConvertDataUpdateUser(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            json += "~11";
            return json;
        }

        public static string ConvertDataLogin(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            json += "~12";
            return json;
        }
        #endregion

        public async static void Push(NotiNumber notiNumber, string datas, bool isSilent)
        {

            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("authorization", "Basic MzUyOWU3MDItMmJlMS00ZWRkLTlkYzEtMDhlZGMyZmEzYjQx");

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("content", datas);

            //GET EXTERNALIDS DỰA VÀO NOTINUMBER Ở ĐÂY
            DataProvider dataProvider = DataProvider.GetInstance();
            string[] externalIDs = dataProvider.GetUserIdsByPushnotiType(notiNumber,datas).ToArray();
            
            object obj = null;
            if (isSilent)
                obj = SilentNoti(externalIDs, dictionary);
            else obj = ShownNoti(externalIDs, dictionary, notiNumber);

            var serializer = new JavaScriptSerializer();
            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }
        }
        public static object SilentNoti(string[] externalIDs, Dictionary<string, object> datas)
        {
            var silentObj = new
            {
                app_id = "b5f59a9f-3873-47a9-80e5-ca37fb75610a",
                //contents = new { en = datas },
                content_available = true,
                include_external_user_ids = externalIDs,
                filters = new object[] { new { field = "tag", key = "IsLogined", value = "1" } },
                data = datas
            };
            return silentObj;
        }

        public static object ShownNoti(string[] externalIDs, Dictionary<string, object> datas, NotiNumber notiNumber)
        {
            var shownObj = new
            {
                headings = new { en = "Grocery App" },
                app_id = "b5f59a9f-3873-47a9-80e5-ca37fb75610a",
                contents = new { en = NotiContent.Get(notiNumber) },
                content_available = true,
                include_external_user_ids = externalIDs,
                
                data = datas
            };
            return shownObj;
        }
    }
}
