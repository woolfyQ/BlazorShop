using Blazored.LocalStorage;
using Core.Models;

namespace MyRoof.WASM.Service
{
    public class CartService
    {
        private readonly ILocalStorageService _localStorageService;

        private const string StorageKey = "Cart";

        public event Action OnChange;
        public CartService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task AddToCart(Product product, int amount)
        { 
            var cart = await GetCartItems();
            
            var existingItem = cart.FirstOrDefault(x => x.ProductId == product.Id);

            if (existingItem != null)
            {
                existingItem.Amount++;
            }

            else
            { 
                cart.Add(new CartItems
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ImgUrl = product.ImgUrl,
                    Price = product.Price,
                    Amount = amount
                });  

            }
            await SaveCart(cart);
            OnChange?.Invoke();
        }

        public async Task RemoveItems(Guid productId)
        {
            var cart = await GetCartItems();

            var items = cart.FirstOrDefault(x
                => x.ProductId == productId);

            if (items != null)
            {
                cart.Remove(items);
                await SaveCart(cart);
                OnChange?.Invoke();
            }
        }

        public async Task UpdateAmount(Guid productId, int newAmount)
        {
            var cart = await GetCartItems();    
            
            var items = cart.FirstOrDefault(i => i.ProductId == productId);

            if(items != null)
            {
                items.Amount = newAmount;
                await SaveCart(cart);
                OnChange?.Invoke();
            }

        }

        public async Task<int> GetItemAmount()
        {
            var cart = await GetCartItems();
            return cart.Sum(item => item.Amount);
        }


        public async Task ClearCart()
        {
            await _localStorageService.RemoveItemAsync(StorageKey);
            OnChange?.Invoke();
        }

        public async Task<List<CartItems>> GetCartItems()
        {
            return await _localStorageService.GetItemAsync<List<CartItems>>(StorageKey) ?? new List<CartItems>();
        }

        private async Task SaveCart(List<CartItems> cart)
        {
            await _localStorageService.SetItemAsync(StorageKey, cart);

        }
        

    }
}
