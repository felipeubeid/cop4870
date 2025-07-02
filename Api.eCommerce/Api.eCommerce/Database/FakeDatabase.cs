using Library.eCommerce.Models;
using Library.eCommerce.DTO;
namespace Api.eCommerce.Database
{
    public static class FakeDatabase
    {
        private static List<Item?> inventory = new List<Item?>
        {
            new Item{ Product = new ProductDTO{Id = 1, Name ="Product 1 WEB"}, Id = 1, Quantity = 1, Price = 10 },
            new Item{ Product = new ProductDTO{Id = 2, Name ="Product 2 WEB"}, Id = 2 , Quantity = 2, Price = 15 },
            new Item{ Product = new ProductDTO{Id = 3, Name ="Product 3 WEB"}, Id = 3 , Quantity = 3, Price = 20 }
        };

        private static List<Item?> shoppingcart = new List<Item?>();

        public static int LastKey_Item
        {
            get
            {
                if(!inventory.Any())
                {
                    return 0;
                }

                return inventory.Select(p => p?.Id ?? 0).Max();
            }
        }
        
        public static List<Item?> Inventory
        {
            get
            {
                return inventory;
            }
        }
        
        public static List<Item?> ShoppingCart
        {
            get
            {
                return shoppingcart;
            }
        }
        
        public static IEnumerable<Item?> Search(string? query)
        {
            return Inventory.Where(p => p?.Product?.Name?.ToLower()
                .Contains(query?.ToLower() ?? string.Empty) ?? false);
        }
        
        public static IEnumerable<Item?> SearchShoppingCart(string? query)
        {
            return ShoppingCart.Where(p => p?.Product?.Name?.ToLower()
                .Contains(query?.ToLower() ?? string.Empty) ?? false);
        }
    }
}