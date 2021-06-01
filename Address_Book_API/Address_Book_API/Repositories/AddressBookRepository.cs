using Address_Book_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Address_Book_API.Repositories
{
    public class AddressBookRepository : IAddressBookRepository
    {
        private readonly ILogger<AddressBookRepository> _logger;
        private readonly AddressBookContext _context;

        public AddressBookRepository(AddressBookContext context, ILogger<AddressBookRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<AddressBook> CreateAsync(AddressBook addressBook)
        {
            _logger.LogInformation($"Adding an object of type {addressBook.GetType()} to the context ");
            _context.AddressBooks.Add(addressBook);
            await _context.SaveChangesAsync();
            return addressBook;
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation($"Deleting an object of id {id} from the context ");
            var addressToDelete = await _context.AddressBooks.FindAsync(id);
            _context.AddressBooks.Remove(addressToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AddressBook>> GetAsync()
        {
            _logger.LogInformation("Getting all adress ");
            return await _context.AddressBooks.ToListAsync();
        }

        public async Task<AddressBook> GetAsync(int id)
        {
            _logger.LogInformation("Getting all adress ");
            return await _context.AddressBooks.FindAsync(id);
        }

        public async Task<AddressBook> GetLastAsync()
        {
            _logger.LogInformation("Getting last added adress ");
            return await _context.AddressBooks.OrderBy(x=>x.Id).LastOrDefaultAsync();

        }
        public async Task<IEnumerable<AddressBook>> GetAddresByCityAsync(string city)
        {
            _logger.LogInformation($"Getting all adress from {city}  ");
            return await _context.AddressBooks.Where(x => x.City.Contains(city)).ToListAsync();
                
           
        }

        public async Task UpdateAsync(AddressBook addressBook)
        {
            _logger.LogInformation($"Udpating an of type {addressBook.GetType()} to the context ");
            _context.Entry(addressBook).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
