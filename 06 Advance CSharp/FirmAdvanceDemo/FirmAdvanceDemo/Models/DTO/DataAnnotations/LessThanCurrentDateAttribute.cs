using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Models.DTO.DataAnnotations
{
    public class LessThanCurrentDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d <= DateTime.Now.Date;
        }
    }
}