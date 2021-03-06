using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace web
{
    public class Property
    {
        public Guid Id{get; set;}
        public int Zipcode{get; set;}
        public string City{get; set;}
        public string State{get; set;}
        public string RentalType{get; set;}
        public int NumberOfBeds{get; set;}
        public int NumberOfBaths{get; set;}
        public int Size{get; set;}
        public decimal Price{get; set;}
        public string Title{get; set;}
        public Owner Owner{get; set;}
        [JsonIgnore]
        public List<Customer> Customers{get; set;}        
        [JsonIgnore]
        public List<DateRange> DateRanges{get; set;}

        Property(){
            Customers = new List<Customer>();
            DateRanges = new List<DateRange>(); 
        }
        public Property(PropertyDto propertyDto){
            Id = Guid.NewGuid();
            Zipcode = propertyDto.Zipcode;
            City = propertyDto.City;
            State = propertyDto.State;
            RentalType = propertyDto.RentalType;
            NumberOfBeds = propertyDto.NumberOfBeds;
            NumberOfBaths = propertyDto.NumberOfBaths;
            Size = propertyDto.Size;
            Price = propertyDto.Price;
            Title = propertyDto.Title;
            Owner = new();
            Customers = new List<Customer>();
            DateRanges = new List<DateRange>();   
        }
    }
}