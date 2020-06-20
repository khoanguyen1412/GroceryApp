using GroceryApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GroceryApp.Data
{
    public class DataUpdater
    {
        //CART
        public static void UpdateProduct(List<Product> updatedProducts)
        {
            foreach (Product newItem in updatedProducts)
                foreach (Product oldItem in Database.Products)
                    if (newItem.IDProduct == oldItem.IDProduct)
                        oldItem.Update(newItem);
        }

        public static void UpdateProduct(ObservableCollection<Product> updatedProducts)
        {
            foreach (Product newItem in updatedProducts)
                foreach (Product oldItem in Database.Products)
                    if (newItem.IDProduct == oldItem.IDProduct)
                        oldItem.Update(newItem);
        }

        
        public static void InsertProduct(List<Product> newProducts)
        {
            foreach (Product newItem in newProducts)
                Database.Products.Add(newItem);
        }

        public static void InsertOrderBill(OrderBill orderBill)
        {
            Database.OrderBills.Add(orderBill);
        }

        public static void ReturnProductToSourceProduct(Product returnProduct)
        {
            foreach(Product product in Database.Products)
                if (product.IDProduct == returnProduct.IDSourceProduct)
                {
                    product.QuantityInventory += returnProduct.QuantityOrder;
                    break;
                }
        }

        public static void DeletedProductInCart(Product deletedProduct)
        {
            foreach (Product product in Database.Products)
                if (product.IDProduct == deletedProduct.IDProduct)
                {
                    Database.Products.Remove(product);
                    return;
                }
                    
        }

        public static void UpdateExistProductIncart(List<Product> updatedProducts)
        {
            foreach (Product product in Database.Products)
                if(product.State == ProductState.InCart && product.IDCart == Infor.IDUser)
                {
                    foreach (Product updatedProduct in updatedProducts)
                        if (product.IDSourceProduct == updatedProduct.IDSourceProduct)
                            product.QuantityOrder += updatedProduct.QuantityOrder;
                }
        }

        //LIST ORDERS
        public static List<Product> ReturnListProductToSource(List<Product> returnProducts)
        {
            List<Product> sourceProducts = new List<Product>();
            foreach(Product returnProduct in returnProducts)
                foreach(Product product in Database.Products)
                    if(returnProduct.IDSourceProduct==product.IDProduct)
                    {
                        product.QuantityInventory += returnProduct.QuantityOrder;
                        sourceProducts.Add(product);
                        break;
                    }

            return sourceProducts;
        }

        public static void DeleteProducts(List<Product> deletedProducts)
        {
            foreach(Product deletedProduct in deletedProducts)
                foreach(Product product in Database.Products)
                    if (deletedProduct.IDProduct == product.IDProduct)
                    {
                        Database.Products.Remove(product);
                        break;
                    }
        }

        public static void DeleteOrderBillByID(string id)
        {
            foreach(OrderBill order in Database.OrderBills)
                if(order.IDOrderBill==id)
                {
                    Database.OrderBills.Remove(order);
                    return;
                }    
        }

        public static void ReceiveOder(OrderBill order)
        {
            foreach(OrderBill orderBill in Database.OrderBills)
                if (orderBill.IDOrderBill == order.IDOrderBill)
                {
                    orderBill.State = OrderState.Received;
                }
        }

        //PRODUCT MANAGER
        public static void UpdateProduct(Product updatedProduct)
        {
            foreach (Product product in Database.Products)
                if (product.IDProduct == updatedProduct.IDProduct)
                {
                    product.Update(updatedProduct);
                    return;
                }
        }

        public static void AddProduct(Product newProduct)
        {
            Database.Products.Add(newProduct);
        }

        public static Product DeleteProductInStore(Product deleteProduct)
        {
            foreach(Product product in Database.Products)
                if (product.IDProduct == deleteProduct.IDProduct)
                {
                    product.StateInStore = ProductStateInStore.Hidden;
                    return product;
                }
            return null;
        } 

        public static Product RestoreProductInStore(Product restoredProduct)
        {
            foreach(Product product in Database.Products)
                if(product.IDProduct==restoredProduct.IDProduct)
                {
                    product.StateInStore = ProductStateInStore.Selling;
                    return product;
                }

            return null;
        }

        //REVIEW MANAGER
        public static void UpdateStoreAnswerOrderbill(OrderBill updatedOrder)
        {
            foreach(OrderBill order in Database.OrderBills)
                if(order.IDOrderBill==updatedOrder.IDOrderBill)
                {
                    order.StoreAnswer = updatedOrder.StoreAnswer;
                    return;
                }
        }
        public static void UpdateStateOrderbill(OrderBill updatedOrder)
        {
            foreach (OrderBill order in Database.OrderBills)
                if (order.IDOrderBill == updatedOrder.IDOrderBill)
                {
                    order.State = updatedOrder.State;
                    return;
                }
        }
    }
}
