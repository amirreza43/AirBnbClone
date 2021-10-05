using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace web
{

    public interface IAirbnbRepository{

        Task AddOwnerAsync(Owner owner);
        Task<Owner> GetOwnerAsync(Guid ownerId);
        void AddProperty(Owner owner, Property property);
        Task<Property> GetPropertyAsync(Guid propertyId);
        Task<IEnumerable<Property>> GetOwnerPropertiesAsync(Owner owner);
        Task<IEnumerable<Property>> GetAllPropertiesAsync();
        Task AddCustomerAsync(Customer customer);
        Task<IEnumerable<Property>> GetPropertyByCityAsync(string city);
        Task<IEnumerable<Property>> GetPropertyByStateAsync(string state);
        Task<IEnumerable<Property>> GetPropertyByZipcodeAsync(int zipcode);
        Task ReserveAsync(DateRange dateRange);
        Task<Weather> GetWeatherAsync(Property property);
    }

}