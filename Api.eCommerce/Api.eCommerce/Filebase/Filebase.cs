
using Library.eCommerce.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.eCommerce.Database
{
    public class Filebase
    {
        private string _root;
        private string _productRoot;
        private string _cartRoot;
        private static Filebase _instance;


        public static Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }
        
        private Filebase()
        {
            _root = "/tmp";
            _productRoot = Path.Combine(_root, "Products");
            _cartRoot = Path.Combine(_root, "Cart");
           
            // if (!Directory.Exists(_productRoot))
            // {
            //     Directory.CreateDirectory(_productRoot);
            // }
        }
        
        public int LastKey
        {
            get
            {
                if (Inventory.Any())
                {
                    return Inventory.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        
        public List<Item?> Inventory
        {
            get
            {
                var root = new DirectoryInfo(_productRoot);
                var _products = new List<Item>();
                foreach(var productsFile in root.GetFiles())
                {
                    var product = JsonConvert
                        .DeserializeObject<Item>
                            (File.ReadAllText(productsFile.FullName));
                    if(product != null)
                    {
                        _products.Add(product);
                    }

                }
                return _products;
            }
        }
        
        public Item AddOrUpdateInventory(Item item)
        {
            //set up a new Id if one doesn't already exist
            if(item.Id <= 0)
            {
                item.Id = LastKey + 1;
            }
            
            item.Product.Id = item.Id;
            
            //go to the right place
            string path = Path.Combine(_productRoot, $"{item.Id}.json");
            

            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(item));

            //return the item, which now has an id
            return item;
        }
        
        public bool DeleteFromInventory(string type, int id)
        {
            string path = Path.Combine(_productRoot, $"{id}.json");

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }
        
        public List<Item?> ShoppingCart
        {
            get
            {
                var root = new DirectoryInfo(_cartRoot);
                var _cart = new List<Item>();
                foreach (var cartFile in root.GetFiles())
                {
                    var item = JsonConvert.DeserializeObject<Item>(File.ReadAllText(cartFile.FullName));
                    if (item != null)
                    {
                        _cart.Add(item);
                    }
                }
                return _cart;
            }
        }
        
        public Item? AddOrUpdateShoppingCart(Item item)
        {
            var existingInventoryItem = Inventory.FirstOrDefault(p => p.Id == item.Id);
            if (existingInventoryItem == null || existingInventoryItem.Quantity == 0)
            {
                return null;
            }
            
            existingInventoryItem.Quantity--;
            File.WriteAllText(Path.Combine(_productRoot, $"{existingInventoryItem.Id}.json"),
                JsonConvert.SerializeObject(existingInventoryItem));

            var existingCartItem = ShoppingCart.FirstOrDefault(p => p.Id == item.Id);
            string cartPath = Path.Combine(_cartRoot, $"{item.Id}.json");

            if (existingCartItem == null)
            {
                var newItem = new Item(item);
                newItem.Quantity = 1;
                File.WriteAllText(cartPath, JsonConvert.SerializeObject(newItem));
                return newItem;
            }
            else
            {
                existingCartItem.Quantity++;
                File.WriteAllText(cartPath, JsonConvert.SerializeObject(existingCartItem));
                return existingCartItem;
            }
            // return existingCartItem;
        }
        
        public bool ReturnItemToInventory(Item item)
        {
            string cartPath = Path.Combine(_cartRoot, $"{item.Id}.json");
            if (File.Exists(cartPath))
            {
                var itemToReturn = JsonConvert.DeserializeObject<Item>(File.ReadAllText(cartPath));
                if (itemToReturn != null)
                {
                    if (itemToReturn.Quantity > 0)
                    {
                        itemToReturn.Quantity--;
                        File.WriteAllText(cartPath, JsonConvert.SerializeObject(itemToReturn));
                        
                        var inventoryItem = Inventory.FirstOrDefault(i => i.Id == itemToReturn.Id);
                        if (inventoryItem != null)
                        {
                            inventoryItem.Quantity++;
                            File.WriteAllText(Path.Combine(_productRoot, $"{inventoryItem.Id}.json"), JsonConvert.SerializeObject(inventoryItem));
                        }
                    }
                    if (itemToReturn.Quantity == 0)
                    {
                        File.WriteAllText(cartPath, JsonConvert.SerializeObject(itemToReturn));
                    }
                    return true;
                }
            }
            return false;
        }
        
        public List<Item?> ClearCart()
        {
            var cartFiles = Directory.GetFiles(_cartRoot, "*.json");
            foreach (var file in cartFiles)
            {
                File.Delete(file);
            }
            return new List<Item?>();
        }

    }
}