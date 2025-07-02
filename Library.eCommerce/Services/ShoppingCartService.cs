using System.Collections.Generic;
using System.Linq;
using Library.eCommerce.Models;
using Library.eCommerce.Util;
using Library.eCommerce.Utilities;
using Newtonsoft.Json;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        private ProductServiceProxy _prodSvc = ProductServiceProxy.Current;
        private List<Item> items;
        private const decimal SalesTax = 0.07m;
        
        private ShoppingCartService() 
        {
            // items = new List<Item>();
            var cartPayload = new WebRequestHandler().Get("/ShoppingCart").Result;
            items = JsonConvert.DeserializeObject<List<Item>>(cartPayload) ?? new List<Item>();
        }

        public List<Item> CartItems
        {
            get
            {
                return items;
            }
        }
        public static ShoppingCartService Current {  
            get
            {
                if(instance == null)
                {
                    instance = new ShoppingCartService();
                }

                return instance;
            } 
        }
        private static ShoppingCartService? instance;
        
        public Item? AddOrUpdate(Item item)
        {
            var response = new WebRequestHandler().Post("/ShoppingCart", item).Result;
            var updatedItem = JsonConvert.DeserializeObject<Item>(response);
            
            if (updatedItem == null)
            {
                return null;
            }
            
            var existingInvItem = _prodSvc.GetById(item.Id);
            if (existingInvItem == null || existingInvItem.Quantity == 0)
            {
                return null;
            }
            if (existingInvItem != null)
            {
                existingInvItem.Quantity--;
            }
            
            var existingItem = CartItems.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem == null)
            {
                var newItem = new Item(item);
                newItem.Quantity = 1;
                CartItems.Add(newItem);
            }
            else
            {
                existingItem.Quantity++;
            }

            return existingInvItem;
        }
        
        public Item? ReturnItem(Item item)
        {
            if (item?.Id <= 0 || item == null)
            {
                return null;
            }
            
            var response = new WebRequestHandler().Post("/ShoppingCart/Return", item).Result;
            var returnedItem = JsonConvert.DeserializeObject<Item>(response);
            
            if (returnedItem == null)
            {
                return null;
            }
            
            var itemToReturn = CartItems.FirstOrDefault(c => c.Id == item.Id);
            if (itemToReturn != null)
            {
                itemToReturn.Quantity--;
                var inventoryItem = _prodSvc.Products.FirstOrDefault(p => p.Id == itemToReturn.Id);
                if (inventoryItem == null)
                {
                    _prodSvc.AddOrUpdate(new Item(itemToReturn));
                }
                else
                {
                    inventoryItem.Quantity++;
                }
            }
            
            return itemToReturn;
        }
        
        public decimal GetSubtotal()
        {
            decimal subtotal = 0;
            foreach (var item in CartItems)
            {
                subtotal += (item.Price ?? 0) * (item.Quantity ?? 0);
            }
            return subtotal;
        }

        public decimal GetSalesTax()
        {
            return GetSubtotal() * SalesTax;
        }

        public decimal GetGrandTotal()
        {
            return GetSubtotal() + GetSalesTax();
        }
        
        public async Task<IEnumerable<Item?>> Search(string? query)
        {
            if (query == null)
            {
                return new List<Item>();
            }
            var response = await new WebRequestHandler().Post("/ShoppingCart/Search", new QueryRequest { Query = query });
            items = JsonConvert.DeserializeObject<List<Item?>>(response) ?? new List<Item?>();
            return items;
        }
        
        public List<Item?> ClearCart()
        {
            var response = new WebRequestHandler().Delete("/ShoppingCart/Clear").Result;
            var clearedCart = JsonConvert.DeserializeObject<List<Item?>>(response) ?? new List<Item?>();
            CartItems.Clear();
            return clearedCart;
        }
        
    }
}
