using Blazored.LocalStorage;
using MyRoof.WASM.Models;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace MyRoof.WASM.Service
{
    public class CartService
    {
        private readonly ILocalStorageService _localStorageService;

        private const string StorageKey = "Cart";

        public CartService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;

        }

        public async Task AddToCart(Product product, int amount)
        {
            var cart = await GetCartItems();
            var existingItems = cart.FirstOrDefault(x => x.Id == product.Id);

            if (existingItems != null)
            {
                existingItems.amount++;
            }

            else
            {
                cart.Add(new CartItem
                {
                    Pr


                });  

                    
                  
            }
            

        }



    }
}
