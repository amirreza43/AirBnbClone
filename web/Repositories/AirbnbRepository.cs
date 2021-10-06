using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace web
{
    public class AirbnbRepository : IAirbnbRepository{

        private Database _db;

        public AirbnbRepository(Database db){
            _db = db;
        }

        public async Task AddOwnerAsync(Owner owner){
            await _db.AddAsync(owner);
        }

        public async Task<Owner> GetOwnerAsync(Guid ownerId){
            return await _db.Owners.Where(o => o.Id == ownerId)
            .Include(o => o.Properties)
            .SingleOrDefaultAsync();
        }

        //owner is passed from the controller and is checked
        public async Task AddProperty(Owner owner, Property property){
            property.Owner = owner;
            await _db.AddAsync(property);
            owner.Properties.Add(property);
        }

        public async Task<Property> GetPropertyAsync(Guid propertyId){
            if(_db.Properties.Where(p => p.Id == propertyId) is null) return null;
            return await _db.Properties.Where(p => p.Id == propertyId).Include(p => p.Owner).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Property>> GetOwnerPropertiesAsync(Owner owner){
            return await _db.Properties.Where(p => p.Owner == owner).ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllPropertiesAsync(){
            return await _db.Properties.Include(p => p.Owner).Include(p => p.DateRanges).ToListAsync();
        }

        public async Task AddCustomerAsync(Customer customer){
            await _db.AddAsync(customer);
        }

        public async Task<Customer> GetCustomerAsync(Guid customerId){
            return await _db.Customers.Where(o => o.Id == customerId)
            .Include(o => o.Properties)
            .Include(c => c.DateRanges)
            .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Property>> GetPropertyByCityAsync(string city){
            if(_db.Properties.Where(p => p.City == city) is null) return null;
            return await _db.Properties.Where(p => p.City == city).Include(p => p.Owner).ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetPropertyByStateAsync(string state){
            if(_db.Properties.Where(p => p.State == state) is null) return null;
            return await _db.Properties.Where(p => p.State == state).Include(p => p.Owner).ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetPropertyByZipcodeAsync(int zipcode){
            if(_db.Properties.Where(p => p.Zipcode == zipcode) is null) return null;
            return await _db.Properties.Where(p => p.Zipcode == zipcode).Include(p => p.Owner).ToListAsync();
        }

        public async Task ReserveAsync(DateRange dateRange){

            await _db.AddAsync(dateRange);
        }

        public async Task<IEnumerable<IEnumerable<DateRange>>> GetDateRangesByProperty(Property property){
            return await _db.Properties.Where(p => p.Id == property.Id)
            .Include(p => p.Customers)
            .Select(p => p.DateRanges).ToListAsync();
        }

        public async Task<IEnumerable<IEnumerable<DateRange>>> GetDateRangesByCustomer(Customer customer){
            return await _db.Customers.Where(c => c.Id == customer.Id)
            .Select(p => p.DateRanges).ToListAsync();
        }

        public async void GetWeatherAsync(Property property){
            var client = new HttpClient();
            var apiKey = await File.ReadAllTextAsync("./keys.txt");
            var zipcode = property.Zipcode;
            var weather = await client.GetAsync($"api.openweathermap.org/data/2.5/weather?q={zipcode}&appid={apiKey}");
            Console.WriteLine(weather);
            
        }

        public async Task SaveAsync(){
            await _db.SaveChangesAsync();
        }

    }
}