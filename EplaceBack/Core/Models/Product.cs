using static System.Formats.Asn1.AsnWriter;

namespace Core.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public bool MercadoLibre { get; set; }
        public bool AliExpress { get; set; }
        public bool Shopee { get; set; }

        private Product() { }

        public Product(string id, string name,string brand, double price,
            int stockQuantity, bool mercadoLibre, bool aliExpress, bool shopee)
        {
            Id = id;
            Name = name;
            Brand = brand;
            Price = price;
            StockQuantity = stockQuantity;
            MercadoLibre = mercadoLibre;
            AliExpress = aliExpress;
            Shopee = shopee;
        }

        public Product(string name, string brand, double price,
            int stockQuantity, bool mercadoLibre, bool aliExpress, bool shopee)
        {
            Name = name;
            Brand = brand;
            Price = price;
            StockQuantity = stockQuantity;
            MercadoLibre = mercadoLibre;
            AliExpress = aliExpress;
            Shopee = shopee;
        }
    }
}
