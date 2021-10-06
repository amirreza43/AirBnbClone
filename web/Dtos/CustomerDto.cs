using System;
using System.ComponentModel.DataAnnotations;


namespace web
{
    public class CustomerDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Name{get; set;}
      
    }

}