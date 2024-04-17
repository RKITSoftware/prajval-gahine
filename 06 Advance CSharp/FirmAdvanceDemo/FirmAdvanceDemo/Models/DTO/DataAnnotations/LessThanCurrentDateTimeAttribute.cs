using System;
using System.ComponentModel.DataAnnotations;

namespace FirmAdvanceDemo.Models.DTO.DataAnnotations
{
    public class LessThanCurrentDateTimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d <= DateTime.Now.Date;
        }
    }
}