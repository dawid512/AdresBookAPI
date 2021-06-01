using Address_Book_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Address_Book_API.Repositories
{
    public interface IAddressBookRepository
    {
        Task<IEnumerable<AddressBook>> GetAsync();
        Task<AddressBook> GetAsync(int id);
        Task<AddressBook> GetLastAsync();
        Task<IEnumerable<AddressBook>> GetAddresByCityAsync(string city);
        Task<AddressBook> CreateAsync(AddressBook addressBook);
        Task UpdateAsync(AddressBook addressBook);
        Task DeleteAsync(int id);
    }
}
