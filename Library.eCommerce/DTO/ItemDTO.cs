namespace Library.eCommerce.DTO
{

    public class ItemDTO
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        
        public override string ToString()
        {
            return Display ?? string.Empty;
        }
        
        public string Display { 
            get
            {
                return $"{Product?.Display ?? string.Empty}, {Quantity}, {Price:C}";
            }
        }

        public ItemDTO()
        {
            Product = new ProductDTO();
            Quantity = 0;
            Price = 0;
        }
        
        public ItemDTO(ItemDTO i)
        {
            Product = new ProductDTO(i.Product);
            Quantity = i.Quantity;
            Id = i.Id;
            Price = i.Price;
        }
    }
}