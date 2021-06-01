using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Address_Book_API.Models
{
    public class AddressBookContext : DbContext
    {
        public AddressBookContext(DbContextOptions<AddressBookContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<AddressBook> AddressBooks { get; set; }
    }
}
