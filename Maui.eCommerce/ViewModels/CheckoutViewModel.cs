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
    public class CheckoutViewModel : INotifyPropertyChanged
    {
        private ShoppingCartService _cartSvc = ShoppingCartService.Current;
        
        public ObservableCollection<Item?> ShoppingCart 
        {
            get
            {
                return new ObservableCollection<Item?>(_cartSvc.CartItems.Where(i => i?.Quantity > 0));
            }
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

        public void RefreshCheckout()
        {
            Subtotal = _cartSvc.GetSubtotal();
            SalesTax = _cartSvc.GetSalesTax();
            GrandTotal = _cartSvc.GetGrandTotal();
            
            NotifyPropertyChanged(nameof(ShoppingCart));
        }
        
        private decimal _subtotal;
        private decimal _salesTax;
        private decimal _grandTotal;
        
        public decimal Subtotal
        {
            get { return _subtotal; }
            set
            {
                if (_subtotal != value)
                {
                    _subtotal = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public decimal SalesTax
        {
            get { return _salesTax; }
            set
            {
                if (_salesTax != value)
                {
                    _salesTax = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public decimal GrandTotal
        {
            get { return _grandTotal; }
            set
            {
                if (_grandTotal != value)
                {
                    _grandTotal = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public void ClearCart()
        {
            _cartSvc.ClearCart();
            _cartSvc.CartItems.Clear();
            NotifyPropertyChanged(nameof(ShoppingCart));

            Subtotal = 0;
            SalesTax = 0;
            GrandTotal = 0;
        }
        
    }
}