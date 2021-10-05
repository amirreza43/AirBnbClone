using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace web
{
    
    public class Weather{
        public string Description{get; set;}
        public double Temp{get; set;}
        public int Humidity{get; set;}
        
    }

}