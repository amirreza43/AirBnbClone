using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;

namespace web
{
    
    [ApiController]
    [Route("api")]

    public class AirbnbController : ControllerBase{

        private IAirbnbRepository _repository;

        public AirbnbController(IAirbnbRepository repository){
            _repository = repository;
        }


        //Add Owner
        [HttpPost("owners")]
        public async Task<IActionResult> AddOwner(OwnerDto ownerDto){
            var owner = new Owner(ownerDto);
            await _repository.AddOwnerAsync(owner);
            await _repository.SaveAsync();

            return CreatedAtAction("GetOwner", new { owner.Id }, owner);
        }

        //Get Owner
        [HttpGet("owners")]
        public async Task<IActionResult> GetOwner(Guid ownerId){
            
            var owner = await _repository.GetOwnerAsync(ownerId);

            return Ok(owner);
        }

        //Add Property
        [HttpPost("properties/{ownerId}")]
        public async Task<IActionResult> AddProperty(Guid ownerId, PropertyDto propertyDto){
            
            var owner = await _repository.GetOwnerAsync(ownerId);

            if( owner is null) return BadRequest();

            var property = new Property(propertyDto);
            await _repository.AddProperty(owner, property);

            await _repository.SaveAsync();

            return CreatedAtAction("GetProperty", new { PropertyId = property.Id }, property);
        }
        
        //get property
        [HttpGet("properties/{propertyId}")]
        public async Task<IActionResult> GetProperty(Guid propertyId){
            
            var property = await _repository.GetPropertyAsync(propertyId);

            return Ok(property);
        }

        //get all properties
        [HttpGet("properties/all")]
        public async Task<IActionResult> GetAllProperties(){
            
            var properties = await _repository.GetAllPropertiesAsync();

            return Ok(properties);
        }


        //get all properties of an owner
        [HttpGet("properties/all/{ownerId}")]
        public async Task<IActionResult> GetAllOwnerProperties(Guid ownerId){
            
            var owner = await _repository.GetOwnerAsync(ownerId);

            if( owner is null) return BadRequest();
            
            var properties = await _repository.GetOwnerPropertiesAsync(owner);

            return Ok(properties);
        }

        //add customer
        [HttpPost("customers")]
        public async Task<IActionResult> AddCustomer(CustomerDto customerDto){
            var customer = new Customer(customerDto);
            await _repository.AddCustomerAsync(customer);
            await _repository.SaveAsync();

            return CreatedAtAction("GetCustomer", new { customer.Id }, customer);
        }

        //Get Customer
        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomer(Guid customerId){
            
            var customer = await _repository.GetCustomerAsync(customerId);

            return Ok(customer);
        }
        
        //search by city
        [HttpGet("properties/city/{cityName}")]
        public async Task<IActionResult> SearchByCity(string cityName){
            
            var properties = await _repository.GetPropertyByCityAsync(cityName);

            return Ok(properties);
        }
        //search by state
        [HttpGet("properties/state/{stateName}")]
        public async Task<IActionResult> SearchByState(string stateName){
            
            var properties = await _repository.GetPropertyByStateAsync(stateName);

            return Ok(properties);
        }
        //search by zipcode
        [HttpGet("properties/zipcode/{zipcode}")]
        public async Task<IActionResult> SearchByZipcode(int zipcode){
            
            var properties = await _repository.GetPropertyByZipcodeAsync(zipcode);

            return Ok(properties);
        }

        //Reserve 
        [HttpPatch("Properties/Reserve/{propertyId}/{customerId}")]
        public async Task<IActionResult> Reserve(Guid propertyId, Guid customerId, DateRangeDto dateRangeDto){
            
            var dateRange = new DateRange(dateRangeDto);
            var customer = await _repository.GetCustomerAsync(customerId);
            var property = await _repository.GetPropertyAsync(propertyId);
            dateRange.Customer = customer;
            dateRange.Property = property;

            await _repository.ReserveAsync(dateRange);

            await _repository.SaveAsync();

            return Ok(3);
        }

        //get Reserve 
        [HttpGet("Properties/Reserve/{propertyId}/{customerId}")]
        public async Task<IActionResult> GetReserve(Guid propertyId, Guid customerId, DateRangeDto dateRangeDto){
            
            var dateRange = new DateRange(dateRangeDto);
            var customer = await _repository.GetCustomerAsync(customerId);
            var property = await _repository.GetPropertyAsync(propertyId);
            property.Customers.Add(customer);
            customer.Properties.Add(property);
            customer.DateRanges.Add(dateRange);
            property.DateRanges.Add(dateRange);
            dateRange.Customer = customer;
            dateRange.Property = property;

            await _repository.ReserveAsync(dateRange);

            await _repository.SaveAsync();

            return Ok(3);
        }
        //get Reserves by properties
        [HttpGet("Properties/Reserves/{propertyId}")]
        public async Task<IActionResult> GetReservesByProperties(Guid propertyId){
            
            var property = await _repository.GetPropertyAsync(propertyId);
        

            var dateRanges = await _repository.GetDateRangesByProperty(property);


            return Ok(dateRanges);
        }

        //get Reserves by Customers
        [HttpGet("Customer/Reserves/{customerId}")]
        public async Task<IActionResult> GetReservesByCustomers(Guid customerId){
            
            var property = await _repository.GetPropertyAsync(customerId);
        

            var dateRanges = await _repository.GetDateRangesByProperty(property);


            return Ok(dateRanges);
        }

    
    }

}