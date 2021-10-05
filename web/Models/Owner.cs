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
        
    }

}