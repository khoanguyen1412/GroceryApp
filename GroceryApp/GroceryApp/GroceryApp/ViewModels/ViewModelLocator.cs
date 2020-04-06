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
            _container = builder.Build();
        }
        public IHomePageViewModel HomePage
        {
            get { return _container.Resolve<IHomePageViewModel>(); }
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
    }
}
