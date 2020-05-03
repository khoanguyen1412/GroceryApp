using GroceryApp.Views.Sliders;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GroceryApp.Views.Selectors
{
    public class DataTemplateSliderSelector : DataTemplateSelector
    {
        public DataTemplate ChartTemplate { get; set; }
        public DataTemplate StatisticTemplate { get; set; }

        public DataTemplateSliderSelector()
        {
            ChartTemplate = new DataTemplate(typeof(ChartSliderView));
            StatisticTemplate = new DataTemplate(typeof(StatisticSliderView));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var index = int.Parse(item.ToString());
            switch (index)
            {
                case 0:
                    return ChartTemplate;
                case 1:
                    return StatisticTemplate;

                default: return ChartTemplate;
            }
        }



    }
}
