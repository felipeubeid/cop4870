using System.Dynamic;
using Api.eCommerce.Database;
using Library.eCommerce.Models;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace Api.eCommerce.EC
{
    public class InventoryEC
    {
        public List<Item?> Get()
        {
            return Filebase.Current.Inventory;
            // return FakeDatabase.Inventory;
        }

        public IEnumerable<Item> Get(string? query)
        {
            return FakeDatabase.Search(query).Take(100) ?? new List<Item>();
        }

        public Item? Delete(int id)
        {
            // var itemToDelete = FakeDatabase.Inventory.FirstOrDefault(i => i?.Id == id);
            var itemToDelete = Filebase.Current.Inventory.FirstOrDefault(i => i?.Id == id);
            if (itemToDelete != null)
            {
                Filebase.Current.DeleteFromInventory("product", itemToDelete.Id);
                // FakeDatabase.Inventory.Remove(itemToDelete);
            }
            return itemToDelete;
        }

        public Item? AddOrUpdate(Item item)
        {
            // comment this and uncomment below
            
            // if(item.Id == 0)
            // {
            //     // item.Id = Filebase.Current.LastKey + 1;
            //     item.Id = FakeDatabase.LastKey_Item + 1;
            //     item.Product.Id = item.Id;
            //     // Filabase.Current.Inventory.Add(item);
            //     FakeDatabase.Inventory.Add(item);
            // }
            // else
            // {
            //     var existingItem = FakeDatabase.Inventory.FirstOrDefault(p => p.Id == item.Id);
            //     var index = FakeDatabase.Inventory.IndexOf(existingItem);
            //     FakeDatabase.Inventory.RemoveAt(index);
            //     FakeDatabase.Inventory.Insert(index, new Item(item));
            // }

            return Filebase.Current.AddOrUpdateInventory(item);
            
            return item;
        }
        
        
    }
}