using Address_Book_API.Controllers;
using Address_Book_API.Models;
using Address_Book_API.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test_Adress_Book_API.Controllers
{
    public class AddressBooksControllerTest : IClassFixture<TestingWebAppFactory>
    {

        private readonly AddressBookContext _addressBookContext;
        private AddressBooksController _sut;
        private readonly Mock<IAddressBookRepository> _addressBookRepositoryMock = new();
        private readonly Mock<ILogger<AddressBooksController>> _logger = new();
        public AddressBooksControllerTest(TestingWebAppFactory Factory)
        {
            var scope = Factory.Services.CreateScope();
            _addressBookContext = scope.ServiceProvider.GetRequiredService<AddressBookContext>();
            _sut = new(scope.ServiceProvider.GetRequiredService<IAddressBookRepository>(), _logger.Object);

        }



        [Fact]
        public async Task GetAddressBooks_ShouldReturnAdresses()
        {
            //arrange
            _addressBookRepositoryMock.Setup(x => x.GetAsync()).ReturnsAsync(GetAddressBooksTest());

            //act
            var result1 = await _sut.GetAddressBooks();

            //assert
            Assert.NotNull(result1);
            Assert.Equal(3, result1.Count());


        }



        //[Fact]
        //public async Task GetLastAddressBooks_ShouldReturnAdresses()
        //{
        //    //arrange
            

        //    //act
        //    AddressBook result1 = await _sut.GetLastAddressBooks();

        //    //assert
        //    Assert.NotNull(result1);
        //    Assert.Matches("Karol", result1.FirstName);


        //}

        private IEnumerable<AddressBook> GetAddressBooksTest()
        {
            return new List<AddressBook>{
            new(){Id=1,FirstName="Jan",LastName="Kowalski",City="Bielsko-Biała",Address="Bielska 12"},

            new(){Id=2,FirstName="Kamil",LastName="Nowak",City="Bielsko-Biała",Address="Bielska 12"},

            new(){Id=3,FirstName="Karol",LastName="Duda",City="Katowice",Address="Bielska 12"}


            };
        }
        [Fact]
        public async Task GetAddressBooks_FindAddressByCity()
        {
            //arrange
            //_addressBookRepositoryMock.Setup(x => x.GetAddresByCityAsync("Bielsko-Biała")).ReturnsAsync(GetAddressBooksTest());

            //act
            var result1 = await _sut.GetAddressBooks("Bielsko-Biała");

            //assert
            Assert.NotNull(result1);
            Assert.Equal(2, result1.Count());
        }


        [Fact]
        public async Task PostAddresToContextTest()
        {
            //arrange
            var AddressBook = new AddressBook() { Id = 4, FirstName = "Dawid", LastName = "Kowalski", City = "Rybarzowice", Address = "Bielska 12" };

            //act
            var result1 = await _sut.PostAddressBooks(AddressBook);

            //assert
            Assert.NotNull(result1);
            Assert.Equal(4, _addressBookContext.AddressBooks.Count());
        }







    }
}
