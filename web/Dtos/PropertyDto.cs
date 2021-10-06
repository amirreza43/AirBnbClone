using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace web
{
    public class PropertyDto
    {
        [Required]
        public int Zipcode{get; set;}
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string City{get; set;}
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string State{get; set;}
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string RentalType{get; set;}
        [Required]
        public int NumberOfBeds{get; set;}
        [Required]
        public int NumberOfBaths{get; set;}
        [Required]
        public int Size{get; set;}
        [Required]
        public decimal Price{get; set;}
        [Required]
        [MaxLength(50)]
        public string Title{get; set;}
        
    }
}