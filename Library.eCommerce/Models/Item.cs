using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Library.eCommerce.DTO;
using Library.eCommerce.Services;

namespace Library.eCommerce.Models
{
    public class Item
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        
        public override string ToString()
        {
            return $"{Product}, Quantity: {Quantity}, Price: {Price:C}";
        }

        public string Display { 
            get
            {
                return $"{Product?.Display ?? string.Empty}, {Quantity}, {Price:C}";
            }
        }

        public Item()
        {
            Product = new ProductDTO();
            Quantity = 0;
            Price = 0;
        }
        
        public Item(Item i)
        {
            Product = new ProductDTO(i.Product);
            Quantity = i.Quantity;
            Id = i.Id;
            Price = i.Price;
        }
        
    }
}
