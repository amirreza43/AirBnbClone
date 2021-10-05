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
        public void AddProperty(Owner owner, Property property){
            owner.Properties.Add(property);
        }

        public async Task<Property> GetPropertyAsync(Guid propertyId){
            if(_db.Properties.Where(p => p.Id == propertyId) is null) return null;
            return await _db.Properties.Where(p => p.Id == propertyId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Property>> GetOwnerPropertiesAsync(Owner owner){
            return await _db.Properties.Where(p => p.Owner == owner).ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllPropertiesAsync(){
            return await _db.Properties.ToListAsync();
        }

        public async Task AddCustomerAsync(Customer customer){
            await _db.AddAsync(customer);
        }

        public async Task<IEnumerable<Property>> GetPropertyByCityAsync(string city){
            if(_db.Properties.Where(p => p.City == city) is null) return null;
            return await _db.Properties.Where(p => p.City == city).ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetPropertyByStateAsync(string state){
            if(_db.Properties.Where(p => p.State == state) is null) return null;
            return await _db.Properties.Where(p => p.State == state).ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetPropertyByZipcodeAsync(int zipcode){
            if(_db.Properties.Where(p => p.Zipcode == zipcode) is null) return null;
            return await _db.Properties.Where(p => p.Zipcode == zipcode).ToListAsync();
        }

        public async Task ReserveAsync(DateRange dateRange){
            await _db.AddAsync(dateRange);
        }

        public async Task<Weather> GetWeatherAsync(Property property){
            var client = new HttpClient();
            var apiKey = await File.ReadAllTextAsync("./keys.txt");
            var zipcode = property.Zipcode;
            var weather = await client.GetFromJsonAsync<List<Weather>>($"https://api.openweathermap.org/data/2.5/weather?q={zipcode}&appid={apiKey}&units=imperial");

            return weather;
        }

    }
}