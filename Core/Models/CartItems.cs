namespace Core.Models
{
    public class CartItems
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public required List<string> ImgUrl { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice => Amount * Price;




    }
}
