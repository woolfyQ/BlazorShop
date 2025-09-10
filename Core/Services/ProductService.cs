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
                    Name = "Колпак на столб забора: Серый Графит (RAL 7024)",
                    Description = "Металлический колпак на столб забора из оцинкованной стали цвета RAL 7024. Надежно защищает торец столба от влаги и коррозии, продлевая срок службы конструкции. Элегантный серый цвет придает забору завершенный вид и гармонично сочетается с любым дизайном.",
                    Price = 2290.00m,
                    ImgUrl = new List<string> { "/Pics/7024_grafit_sverh.jpg", "/Pics/7024_grafit.jpg", "/pics/Colors.jpg", "/pics/klienty.png" }
                },
                new Product
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440002"),
                    Name = "Колпак на столб забора: Коричневый Шоколад (RAL 8017)",
                    Description = "Защитный колпак на заборный столб из оцинкованной стали с покрытием RAL 8017. Предотвращает попадание осадков в поры материала, обеспечивая долговечность ограждения. Богатый коричневый оттенок подчеркивает натуральность деревянных и каменных конструкций.",
                    Price = 2290.00m,
                    ImgUrl = new List<string> { "/Pics/8017_ugol.webp", "/Pics/8017_svethy.jpg", "/pics/Colors.jpg", "/pics/klienty.png" }
                },
                new Product
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440003"),
                    Name = "Колпак на столб забора: Серый Глянец (RAL 7024)",
                    Description = "Декоративный колпак на столб забора из оцинкованной стали глубокого черного цвета (RAL 9005). Создает стильный акцент в оформлении ограждения и эффективно защищает от разрушающего воздействия влаги. Универсальное решение для современных и классических экстерьеров.",
                    Price = 2290.00m,
                    ImgUrl = new List<string> { "/Pics/7024_glanec.jpg", "/Pics/7024_glanec_sverhy.jpg", "/pics/Colors.jpg", "/pics/klienty.png" }
                },
                new Product
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440004"),
                    Name = "Колпак на столб забора с кованой вставкой",
                    Description = "Эксклюзивное завершение для столба забора с декоративной металлической вставкой. Сочетает практичную защиту от осадков с художественными элементами ковки. Идеально подходит для элитных ограждений и парадных входных групп.",
                    Price = 2490.00m,
                    ImgUrl = new List<string> { "/Pics/7024_s_metall_vstavka.jpg", "/Pics/Ral7024_ugol_Vstavka.jpg", "/pics/Colors.jpg", "/pics/klienty.png" }
                },
                new Product
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440005"),
                    Name = "Колпак на столб забора: Графит",
                    Description = "Специализированный колпак для столбов забора. Обеспечивает плотное прилегание к нестандартным профилям и полную защиту торцов. Особенно рекомендован для столбов с сложной геометрией и угловых опорных конструкций.",
                    Price = 2690.00m,
                    ImgUrl = new List<string> { "/Pics/kolpak_grafit.webp", "/pics/Colors.jpg", "/pics/klienty.png" }
                },
                new Product
                {
                    Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440006"),
                    Name = "Колпак на столб забора: Черный Глянец (RAL 9005)",
                    Description = "Колпак на столб забора из оцинкованной стали в цвете RAL 9005. Предотвращает скапливание влаги на сложных участках ограждения и добавляет архитектурной выразительности. Устойчив к ультрафиолету и перепадам температур.",
                    Price = 2290.00m,
                    ImgUrl = new List<string> { "/Pics/ral9005_1.webp", "/Pics/ral9005_obsh.webp", "/pics/Colors.jpg", "/pics/klienty.png" }
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