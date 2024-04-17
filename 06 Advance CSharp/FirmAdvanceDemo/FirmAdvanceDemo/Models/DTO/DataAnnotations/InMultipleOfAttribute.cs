using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Models.DTO.DataAnnotations
{
    public class InMultipleOfAttribute : ValidationAttribute
    {
        private double _divisor;
        public InMultipleOfAttribute(double divisor)
        {
            _divisor = divisor;
        }
        public override bool IsValid(object value)
        {
            double n = (double)value;

            return n % _divisor == 0;
        }
    }
}