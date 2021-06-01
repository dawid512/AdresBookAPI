using Address_Book_API;
using Address_Book_API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Adress_Book_API.Controllers
{
   public class TestingWebAppFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var bookContext = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<AddressBookContext>));


                if (bookContext != null)
                {
                    services.Remove(bookContext);
                }



                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<AddressBookContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryBookDbTest");
                    options.UseInternalServiceProvider(serviceProvider);
                });



                var sp = services.BuildServiceProvider();
                SeedDatabase(sp);
            });
        }

        private void SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            using var bookDbContext = services.GetRequiredService<AddressBookContext>();


            bookDbContext.Database.EnsureCreated();




            bookDbContext.AddressBooks.AddRange(GetAddressBooksTest());
            bookDbContext.SaveChanges();
        }

        private IEnumerable<AddressBook> GetAddressBooksTest()
        {
            return new List<AddressBook>{
            new(){Id=1,FirstName="Jan",LastName="Kowalski",City="Bielsko-Biała",Address="Bielska 12"},

            new(){Id=2,FirstName="Kamil",LastName="Nowak",City="Bielsko-Biała",Address="Bielska 12"},

            new(){Id=3,FirstName="Karol",LastName="Duda",City="Katowice",Address="Bielska 12"}


            };
        }



    }
}