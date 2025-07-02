using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views
{

    public partial class CheckoutView : ContentPage
    {
        public CheckoutView()
        {
            InitializeComponent();
            BindingContext = new CheckoutViewModel();
        }
        
        private void GoBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }
        
        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            (BindingContext as CheckoutViewModel)?.RefreshCheckout();
        }
        
        private void CheckoutClicked(object sender, EventArgs e)
        {
            (BindingContext as CheckoutViewModel)?.ClearCart();
        }
    }
}