using GroceryApp.Data;
using ImTools;
using System.Collections.Generic;

public enum ProductState
{
    InStore=1,
    InCart=2,
    InBill=3
}

public enum ProductStateInStore
{
    Selling=1,
    Hidden=2
}

public enum OrderState
{
    Waiting=1,
    Delivering=2,
    Received=3
}

public enum NotiNumber
{
    AddToCart,
    MakeBillForStore,//push tới store được order
    MakeBillForOther,//push tới các user khác
    ReturnProductCart,
    CancelOrderForStore,//push tới store bị hủy order
    CancelOrderForOther,//Push trới các user khác
    CancelOrderForCustomer,//push tới customer bị hủy order
    ReceiveOrderForStore,//push tới store của order được nhận
    ReceiveOrderForOther,//push tới các user khác
    UpdateProduct,
    AddProduct,
    AnswerFeedback,
    DeliverOrderForCustomer,//push tới khách hàng bị hủy order
    DeliverOrderForOther,//push tới các user khác
    UpdateStore,
    UpdateUser,
    Login,
    SendReview
}

public class NotiContent
{
    public static string Get(NotiNumber notiNumber)
    {
        switch (notiNumber)
        {
            case NotiNumber.MakeBillForStore:
                //return "Cửa hàng của bạn vừa nhận một order mới!";
                return "Your store has just received a new order!";
                break;
            case NotiNumber.CancelOrderForStore:
                //return "Cửa hàng của bạn có một order đã bị hủy";
                return "An order has just been canceled by customer";
                break;
            case NotiNumber.CancelOrderForCustomer:
                //return "Bạn có một order đã bị hủy";
                return "An order has just been canceled by store";
                break;
            case NotiNumber.ReceiveOrderForStore:
                //return "Cửa hàng của bạn có một order đã được nhận!";
                return "An order has just been received by customer!";
                break;
            case NotiNumber.DeliverOrderForCustomer:
                return "Your order has just started delivering!";
                break;
            case NotiNumber.Login:
                return "Your account has just been signed up in another device!";
                break;
            case NotiNumber.SendReview:
                //return "Order của cửa hàng vừa nhận được một feedback từ khách hàng!";
                return "Your store's order has just received a feedback from customer!";
        }
        return "";
    }

}

