using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views;

public partial class ShoppingManagementView : ContentPage
{
	public ShoppingManagementView()
	{
		InitializeComponent();
		BindingContext = new ShoppingManagementViewModel();
	}

	private void RemoveFromCartClicked(object sender, EventArgs e)
	{
		(BindingContext as ShoppingManagementViewModel).ReturnItem();
	}
	
    private void AddToCartClicked(object sender, EventArgs e)
    {
	    (BindingContext as ShoppingManagementViewModel).PurchaseItem();
    }
    private void GoBackClicked(object sender, EventArgs e)
    {
	    Shell.Current.GoToAsync("//MainPage");
    }
    
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
	    (BindingContext as ShoppingManagementViewModel)?.RefreshInventory();
	    (BindingContext as ShoppingManagementViewModel)?.RefreshShoppingCart();
    }

    private void InlineAddClicked(object sender, EventArgs e)
    {
	    (BindingContext as ShoppingManagementViewModel).RefreshUX();
    }
    
    private void SearchClicked(object sender, EventArgs e)
    {
	    (BindingContext as ShoppingManagementViewModel)?.RefreshShoppingCart();
    }
}