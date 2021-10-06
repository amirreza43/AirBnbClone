using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace web
{

  [Table("DateRanges")]
  public class DateRange
  {
    public Customer Customer { get; set; }
    public Property Property { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public DateRange(){
      Customer = null;
      Property = null;
    }

    public DateRange(DateRangeDto dateRangeDto){
      Start = dateRangeDto.Start;
      End = dateRangeDto.End;
    }

  }
}