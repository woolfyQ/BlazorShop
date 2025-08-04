using Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public class SeedData
    {
        public static void Initialize(IApplicationBuilder applicationBuilder)
        {
            using var ServiceScope = applicationBuilder.ApplicationServices.CreateScope();

            var context = ServiceScope.ServiceProvider.GetService<ApplicationDbContext>();  

            context.Database.EnsureCreated();

            if (!context.Products.Any())
            {
                context.Products.AddRange(

                  new Product
                  {
                      Id = Guid.NewGuid(),
                      Name = "Колпак на столб забора. RAL 7024",
                      Price = 2500,
                      ImgUrl = new List<string> {"/pics/Kolpak1.jpg"},
                      Description = "Колпак для забора - это не только стильный декоративный элемент, но и функциональное решение для защиты вашего забора"
                  },
                   new Product
                   {
                       Id = Guid.NewGuid(),
                       Name = "Колпак на столб забор. RAL 7024m",
                       Price = 2500,
                       ImgUrl = new List<string> { "/pics/Kolpak1.jpg" },
                       Description = "Колпак для забора - это не только стильный декоративный элемент, но и функциональное решение для защиты вашего забора"
                   },
                    new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = "Колпак на столб забора. RAL 8017",
                        Price = 2500,
                        ImgUrl = new List<string> { "/pics/Kolpak1.jpg" },
                        Description = "Колпак для забора - это не только стильный декоративный элемент, но и функциональное решение для защиты вашего забора"
                    },
                     new Product
                     {
                         Id = Guid.NewGuid(),
                         Name = "Колпак на столб забора. RAL 9005",
                         Price = 2800,
                         ImgUrl = new List<string> { "/pics/Kolpak1.jpg" },
                         Description = "Колпак для забора - это не только стильный декоративный элемент, но и функциональное решение для защиты вашего забора"
                     },
                      new Product
                      {
                          Id = Guid.NewGuid(),
                          Name = "Колпак на столб забора. RAL 8017",
                          Price = 2800,
                          ImgUrl = new List<string> { "/pics/Kolpak1.jpg" },
                          Description = "Колпак для забора - это не только стильный декоративный элемент, но и функциональное решение для защиты вашего забора"

                      },
                       new Product
                       {
                           Id = Guid.NewGuid(),
                           Name = "Колпак на столб забора. Ral 9005",
                           Price = 2800,
                           ImgUrl = new List<string> { "/pics/Kolpak1.jpg" },
                           Description = "Колпак для забора - это не только стильный декоративный элемент, но и функциональное решение для защиты вашего забора"

                       });

                context.SaveChanges();


            }
        }



    }
}
