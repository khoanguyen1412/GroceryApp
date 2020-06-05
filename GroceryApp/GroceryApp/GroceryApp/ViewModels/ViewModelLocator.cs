using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryApp.ViewModels
{
    public class ViewModelLocator
    {
        private IContainer _container;
        public ViewModelLocator()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<HomePageViewModel>().As<IHomePageViewModel>().SingleInstance();
            builder.RegisterType<ListStoresViewModel>().As<IListStoresViewModel>().SingleInstance();
            builder.RegisterType<CartViewModel>().As<ICartViewModel>().SingleInstance();
            builder.RegisterType<ListOrdersViewModel>().As<IListOrdersViewModel>().SingleInstance();
            builder.RegisterType<OrderDetailPopupViewModel>().As<IOrderDetailPopupViewModel>().SingleInstance();
            builder.RegisterType<ShowStoreViewModel>().As<IShowStoreViewModel>().SingleInstance();
            builder.RegisterType<DetailProductViewModel>().As<IDetailProductViewModel>().SingleInstance();
            builder.RegisterType<ConfirmInforOrderPopupViewModel>().As<IConfirmInforOrderPopupViewModel>().SingleInstance();
            builder.RegisterType<ReviewStorePopupViewModel>().As<IReviewStorePopupViewModel>().SingleInstance();
            builder.RegisterType<FinalBillViewModel>().As<IFinalBillViewModel>().SingleInstance();
            builder.RegisterType<ProductManagerViewModel>().As<IProductManagerViewModel>().SingleInstance();
            builder.RegisterType<AddProductPopupViewModel>().As<IAddProductPopupViewModel>().SingleInstance();
            builder.RegisterType<EditProductPopupViewModel>().As<IEditProductPopupViewModel>().SingleInstance();
            builder.RegisterType<OrderManagerViewModel>().As<IOrderManagerViewModel>().SingleInstance();
            builder.RegisterType<ReviewManagerViewModel>().As<IReviewManagerViewModel>().SingleInstance();
            builder.RegisterType<StoreDashBoardViewModel>().As<IStoreDashBoardViewModel>().SingleInstance();
            builder.RegisterType<StoreSetttingViewModel>().As<IStoreSetttingViewModel>().SingleInstance();
            builder.RegisterType<OrderDetailManagerPopupViewModel>().As<IOrderDetailManagerPopupViewModel>().SingleInstance();
            builder.RegisterType<UserSettingViewModel>().As<IUserSettingViewModel>().SingleInstance();
            builder.RegisterType<ChangePasswordViewModel>().As<IChangePasswordViewModel>().SingleInstance();
            _container = builder.Build();
        }

        public IChangePasswordViewModel ChangePassword
        {
            get { return _container.Resolve<IChangePasswordViewModel>(); }
        }
        public IUserSettingViewModel UserSetting
        {
            get { return _container.Resolve<IUserSettingViewModel>(); }
        }

        public IOrderDetailManagerPopupViewModel OrderDetailManager
        {
            get { return _container.Resolve<IOrderDetailManagerPopupViewModel>(); }
        }

        public IStoreSetttingViewModel StoreSetting
        {
            get { return _container.Resolve<IStoreSetttingViewModel>(); }
        }
        public IStoreDashBoardViewModel StoreDashBoard
        {
            get { return _container.Resolve<IStoreDashBoardViewModel>(); }
        }
        public IReviewManagerViewModel ReviewManager
        {
            get { return _container.Resolve<IReviewManagerViewModel>(); }
        }
        public IOrderManagerViewModel OrderManager
        {
            get { return _container.Resolve<IOrderManagerViewModel>(); }
        }
        public IHomePageViewModel HomePage
        {
            get { return _container.Resolve<IHomePageViewModel>(); }
        }
        public IAddProductPopupViewModel AddProduct
        {
            get { return _container.Resolve<IAddProductPopupViewModel>(); }
        }

        public IEditProductPopupViewModel EditProduct
        {
            get { return _container.Resolve<IEditProductPopupViewModel>(); }
        }

        public IListStoresViewModel ListStores
        {
            get { return _container.Resolve<IListStoresViewModel>(); }
        }

        public ICartViewModel Cart
        {
            get { return _container.Resolve<ICartViewModel>(); }
        }

        public IListOrdersViewModel ListOrders
        {
            get { return _container.Resolve<IListOrdersViewModel>(); }
        }
        public IOrderDetailPopupViewModel OrderDetail
        {
            get { return _container.Resolve<IOrderDetailPopupViewModel>(); }
        }

        public IShowStoreViewModel ShowStore
        {
            get { return _container.Resolve<IShowStoreViewModel>(); }
        }
        public IDetailProductViewModel DetailProduct
        {
            get { return _container.Resolve<IDetailProductViewModel>(); }
        }

        public IConfirmInforOrderPopupViewModel ConfirmOrder
        {
            get { return _container.Resolve<IConfirmInforOrderPopupViewModel>(); }
        }

        public IReviewStorePopupViewModel ReviewStorePopup
        {
            get { return _container.Resolve<IReviewStorePopupViewModel>(); }
        }

        public IFinalBillViewModel FinalBill
        {
            get { return _container.Resolve<IFinalBillViewModel>(); }
        }

        public IProductManagerViewModel ProductManager
        {
            get { return _container.Resolve<IProductManagerViewModel>(); }
        }
    }
}
