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
    MakeBill,
    ReturnProductCart,
    CancelOrder,
    ReceiveOrder,
    UpdateProduct,
    AddProduct,
}

public class NotiContent
{
    public static string Get(NotiNumber notiNumber)
    {
        switch (notiNumber)
        {
            case NotiNumber.MakeBill:
                return "Cửa hàng của bạn vừa nhận một order mới!";
                break;
            case NotiNumber.CancelOrder:
                return "Cửa hàng của bạn có một order đã bị hủy";
                break;
            case NotiNumber.ReceiveOrder:
                return "Cửa hàng của bạn có một order đã được nhận!";
                break;
        }
        return "";
    }
}

