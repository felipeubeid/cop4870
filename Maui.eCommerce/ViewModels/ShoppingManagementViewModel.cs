using Library.eCommerce.Models;
using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class ShoppingManagementViewModel : INotifyPropertyChanged
    {
        private ProductServiceProxy _invSvc = ProductServiceProxy.Current;
        private ShoppingCartService _cartSvc = ShoppingCartService.Current;
        public string? CartQuery { get; set; }
        public ItemViewModel? SelectedItem { get; set; }
        public ItemViewModel? SelectedCartItem { get; set; }

        public ObservableCollection<ItemViewModel?> Inventory 
        {
            get
            {
                return new ObservableCollection<ItemViewModel?>(_invSvc.Products
                    .Where(i => i?.Quantity > 0).Select(m => new ItemViewModel(m))
                    );
            }
        }
        
        public ObservableCollection<ItemViewModel?> ShoppingCart 
        {
            get
            {
                var filteredList = _cartSvc.CartItems
                    .Where(p => (p?.Quantity > 0) && (p?.Product?.Name?.ToLower()
                        .Contains(CartQuery?.ToLower() ?? string.Empty) ?? false))
                            .Select(p => new ItemViewModel(p));
                return new ObservableCollection<ItemViewModel?>(filteredList);
            }
            // get
            // {
            //     return new ObservableCollection<ItemViewModel?>(_cartSvc.CartItems
            //         .Where(i => i?.Quantity > 0).Select(m => new ItemViewModel(m))
            //         );
            // }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshUX()
        {
            NotifyPropertyChanged(nameof(Inventory));
            NotifyPropertyChanged(nameof(ShoppingCart));
        }

        public void PurchaseItem()
        {
            if (SelectedItem != null)
            {
                var shouldRefresh = SelectedItem.Model.Quantity >= 1;
                var updatedItem = _cartSvc.AddOrUpdate(SelectedItem.Model);
                
                if (updatedItem != null && shouldRefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }
        }

        public void ReturnItem()
        {
            if (SelectedItem != null)
            {
                var shouldRefresh = SelectedCartItem.Model.Quantity >= 1;
                var updatedItem = _cartSvc.ReturnItem(SelectedCartItem.Model);
                
                if (updatedItem != null && shouldRefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }
            }   
        }
        
        public void RefreshInventory()
        {
            NotifyPropertyChanged(nameof(Inventory));
        }
        
        public void RefreshShoppingCart()
        {
            NotifyPropertyChanged(nameof(ShoppingCart));
        }
        
        // public ObservableCollection<Item?> Items
        // {
        //     get
        //     {
        //         var filteredList = _cartSvc.CartItems
        //             .Where(p => p?.Product?.Name?.ToLower()
        //                 .Contains(CartQuery?.ToLower() ?? string.Empty) ?? false);
        //         return new ObservableCollection<Item?>(filteredList);
        //     }
        // }
        
        public async Task <bool> Search()
        {
            await _cartSvc.Search(CartQuery);
            NotifyPropertyChanged(nameof(ShoppingCart));
            return true;
        }
        
        // public List<Item?> ClearCart()
        // {
        //     var clearedCart = _cartSvc.ClearCart();
        //     _cartSvc.CartItems.Clear();
        //     NotifyPropertyChanged(nameof(ShoppingCart));
        //     return clearedCart;
        // }
    }
}
