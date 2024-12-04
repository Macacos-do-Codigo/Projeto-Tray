namespace Core.DTOs
{
    public class ProductDTO
    {
        public required string Name { get; set; }
        public required string Brand { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public bool MercadoLibre { get; set; }
        public bool AliExpress { get; set; }
        public bool Shopee { get; set; }
    }
}
