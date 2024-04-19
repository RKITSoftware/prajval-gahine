using System.ComponentModel.DataAnnotations;

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