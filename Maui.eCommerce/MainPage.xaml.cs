using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            // BindingContext = new ShoppingManagementViewModel();
        }

        private void InventoryClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagement");
        }

        private void ShopClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ShoppingManagement");
        }
        
        private void CheckoutClicked(object sender, EventArgs e)
        {
            // var shoppingViewModel = (BindingContext as ShoppingManagementViewModel);
            // shoppingViewModel?.ClearCart();
            // (BindingContext as ShoppingManagementViewModel)?.RefreshShoppingCart();
            Shell.Current.GoToAsync("//Checkout");
            // (BindingContext as ShoppingManagementViewModel)?.ClearCart();
        }
    }

}
