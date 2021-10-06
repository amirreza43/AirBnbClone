using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace web
{
    
    public class Owner{
        public Guid Id{get; set;}
        public string Name{get; set;}
        [JsonIgnore]
        public List<Property> Properties{get; set;}

        public Owner(){
            Properties = new List<Property>();
        }
        public Owner(OwnerDto ownerDto){
            Id = Guid.NewGuid();
            Name = ownerDto.Name;
            Properties = new List<Property>();
        }
        
    }

}