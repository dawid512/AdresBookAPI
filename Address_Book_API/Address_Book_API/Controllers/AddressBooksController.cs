using Address_Book_API.Models;
using Address_Book_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Address_Book_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressBooksController : ControllerBase
    {
        private readonly ILogger<AddressBooksController> _logger;
        private readonly IAddressBookRepository _addressBookRepository;

        public AddressBooksController(IAddressBookRepository addressBookRepository, ILogger<AddressBooksController> logger)
        {
            _addressBookRepository = addressBookRepository;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<AddressBook>> GetAddressBooks()
        {
            _logger.LogInformation("Get all addresses.");
            return await _addressBookRepository.GetAsync();
        }
        [HttpGet("last")]
        public async Task<ActionResult<AddressBook>> GetLastAddressBooks()
        {
            _logger.LogInformation("Get the last one address");
            var result = await _addressBookRepository.GetLastAsync();
            return Ok( result);
        }

        [HttpGet("{city}")]
        public async Task<IEnumerable<AddressBook>> GetAddressBooks(string city)
        {
            _logger.LogInformation("Get addresses with certain city.");
            return await _addressBookRepository.GetAddresByCityAsync(city);
        }
        [HttpPost("Add new")]
        public async Task<ActionResult<AddressBook>> PostAddressBooks([FromBody] AddressBook addressBook)
        {
            _logger.LogInformation("Posting addres to database.");
            var newAddress = await _addressBookRepository.CreateAsync(addressBook);
            return CreatedAtAction(nameof(GetAddressBooks), new { id = newAddress.Id }, newAddress);
        }
        [HttpPost("Update")]
        public async Task<ActionResult<AddressBook>> UpdateAddressBooks(int id, [FromBody] AddressBook addressBook)
        {
            _logger.LogInformation($"Update address with id: {id}.");
            if (id != addressBook.Id)
            {
                return BadRequest();
            }
            await _addressBookRepository.UpdateAsync(addressBook);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult<AddressBook>> DeleteAddressBook(int id)
        {
            _logger.LogInformation($"Delete address with id: {id}.");
            var addressBookToDelete = await _addressBookRepository.GetAsync(id);
            if (addressBookToDelete == null)
            {
                return NotFound();
            }
            await _addressBookRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
