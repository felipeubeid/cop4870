using Api.eCommerce.Database;
using Library.eCommerce.Models;

namespace Api.eCommerce.EC
{
    public class ShoppingCartEC
    {
        public List<Item?> Get()
        {
            return Filebase.Current.ShoppingCart;
            // return FakeDatabase.ShoppingCart;
        }
        
        public IEnumerable<Item> Get(string? query)
        {
            return FakeDatabase.SearchShoppingCart(query).Take(100) ?? new List<Item>();
        }

        public Item? AddOrUpdate(Item item)
        {
            // var existingInvItem = FakeDatabase.Inventory.FirstOrDefault(p => p.Id == item.Id);
            // if (existingInvItem == null || existingInvItem.Quantity == 0)
            // {
            //     return null;
            // }
            // if (existingInvItem != null)
            // {
            //     existingInvItem.Quantity--;
            // }
            //
            // var existingItem = FakeDatabase.ShoppingCart.FirstOrDefault(p => p.Id == item.Id);
            // if (existingItem == null)
            // {
            //     var newItem = new Item(item);
            //     newItem.Quantity = 1;
            //     FakeDatabase.ShoppingCart.Add(newItem);
            // }
            // else
            // {
            //     existingItem.Quantity++;
            // }
            // return existingItem;
            
            return Filebase.Current.AddOrUpdateShoppingCart(item);
        }

        public Item? ReturnItem(Item item)
        {
            // if (item?.Id <= 0)
            // {
            //     return null;
            // }
            //
            // var itemToReturn = FakeDatabase.ShoppingCart.FirstOrDefault(c => c.Id == item.Id);
            // if (itemToReturn != null)
            // {
            //     itemToReturn.Quantity--;
            //     var inventoryItem = FakeDatabase.Inventory.FirstOrDefault(p => p.Id == itemToReturn.Id);
            //     if (inventoryItem == null)
            //     {
            //         FakeDatabase.Inventory.Add(new Item(itemToReturn));
            //     }
            //     else
            //     {
            //         inventoryItem.Quantity++;
            //     }
            // }
            // return itemToReturn;
            
            return Filebase.Current.ReturnItemToInventory(item) ? item : null;
        }

        public List<Item?> ClearCart()
        {
            // FakeDatabase.ShoppingCart.Clear();
            // return FakeDatabase.ShoppingCart;
            return Filebase.Current.ClearCart();
        }
    }
}