using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace web
{
    public class Customer
    {
        public Guid Id {get; set;}
        public string Name{get; set;}
        [JsonIgnore]
        public List<Property> Properties{get; set;}
        [JsonIgnore]
        public List<DateRange> DateRanges{get; set;}

        public Customer(){
            Properties = new List<Property>();
            DateRanges = new List<DateRange>();
        }
        public Customer(CustomerDto customerDto){
            Id = Guid.NewGuid();
            Name = customerDto.Name;
            Properties = new List<Property>();
            DateRanges = new List<DateRange>();
        }
    }

}