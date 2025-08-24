using Core.InterFaces;
using Core.Models;

namespace Core.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;

        public ProductService()
        {
            _products = new List<Product>
            {
                new Product
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440001"),
                    Name = "Колпак дымохода 7024",
                    Description = "Колпак дымохода из оцинкованной стали, цвет RAL 7024 (серый). Защищает дымоход от осадков и улучшает тягу.",
                    Price = 2500.00m,
                    ImgUrl = new List<string> { "/Pics/7024_grafit_obsh.jpg", "/Pics/7024_s_metall_vstavka.jpg" }
                },
                new Product
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440002"),
                    Name = "Колпак дымохода 8017",
                    Description = "Колпак дымохода из оцинкованной стали, цвет RAL 8017 (коричневый). Надежная защита от влаги и ветра.",
                    Price = 2800.00m,
                    ImgUrl = new List<string> { "/Pics/8017_svethy.jpg", "/Pics/8017_ugol.jpg" }
                },
                new Product
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440003"),
                    Name = "Колпак дымохода 9005",
                    Description = "Колпак дымохода из оцинкованной стали, цвет RAL 9005 (черный). Элегантный дизайн и долговечность.",
                    Price = 2200.00m,
                    ImgUrl = new List<string> { "/Pics/9005(2).jpg", "/Pics/9005(3).jpg" }
                },
                new Product
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440004"),
                    Name = "Колпак с металлической вставкой",
                    Description = "Колпак дымохода с декоративной металлической вставкой. Сочетание функциональности и красоты.",
                    Price = 3200.00m,
                    ImgUrl = new List<string> { "/Pics/7024_s_metall_vstavka.jpg", "/Pics/Ral7024_ugol_Vstavka.jpg" }
                },
                new Product
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440005"),
                    Name = "Колпак угловой 7024",
                    Description = "Угловой колпак дымохода, цвет RAL 7024. Идеально подходит для угловых дымоходов.",
                    Price = 2700.00m,
                    ImgUrl = new List<string> { "/Pics/Ral7024_mat_ugol.jpg", "/Pics/ral7024_sboky_vstavka.jpg" }
                },
                new Product
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440006"),
                    Name = "Колпак угловой 9005",
                    Description = "Угловой колпак дымохода, цвет RAL 9005. Стильное решение для современного дома.",
                    Price = 2400.00m,
                    ImgUrl = new List<string> { "/Pics/ral9005_ugol.jpg", "/Pics/Ral9005.jpg" }
                }
            };
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            await Task.Delay(100);
            return _products;
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            await Task.Delay(100);
            return _products.FirstOrDefault(p => p.Id == id);
        }
    }
}
