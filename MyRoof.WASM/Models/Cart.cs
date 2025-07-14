namespace MyRoof.WASM.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public List<Product> ProductInCart { get; set; }
        public int Amount { get; set; } = 1;
        public decimal TotalPrice {  get; set; }

    }
}
