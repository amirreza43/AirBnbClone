using System;
using System.ComponentModel.DataAnnotations;


namespace web
{
    public class DateRangeDto
    {

        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
      
    }

}