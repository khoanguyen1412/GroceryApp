//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("GroceryApp.Views.Popups.SelectedProductsPopupView.xaml", "Views/Popups/SelectedProductsPopupView.xaml", typeof(global::GroceryApp.Views.Popups.SelectedProductsPopupView))]

namespace GroceryApp.Views.Popups {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\Popups\\SelectedProductsPopupView.xaml")]
    public partial class SelectedProductsPopupView : global::Xamarin.Forms.ContentView {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.CollectionView BasketSummary;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.PancakeView.PancakeView ExpandButton;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.PancakeView.PancakeView CollapseButton;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(SelectedProductsPopupView));
            BasketSummary = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.CollectionView>(this, "BasketSummary");
            ExpandButton = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.PancakeView.PancakeView>(this, "ExpandButton");
            CollapseButton = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.PancakeView.PancakeView>(this, "CollapseButton");
        }
    }
}
