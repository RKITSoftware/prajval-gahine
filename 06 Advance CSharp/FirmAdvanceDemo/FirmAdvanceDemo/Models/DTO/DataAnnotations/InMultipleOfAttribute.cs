using System.ComponentModel.DataAnnotations;

namespace FirmAdvanceDemo.Models.DTO.DataAnnotations
{
    /// <summary>
    /// Custom validation attribute to validate if a numeric value is a multiple of a specified divisor.
    /// </summary>
    public class InMultipleOfAttribute : ValidationAttribute
    {
        #region Private Fields
        /// <summary>
        /// Divisor
        /// </summary>
        private readonly double _divisor;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the InMultipleOfAttribute class with the specified divisor.
        /// </summary>
        /// <param name="divisor">The divisor to use for validation.</param>
        public InMultipleOfAttribute(double divisor)
        {
            _divisor = divisor;
        }
        #endregion
        
        #region Public Methods
        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>True if the value is valid; otherwise, false.</returns>
        public override bool IsValid(object value)
        {
            double n = (double)value;

            return n % _divisor == 0;
        }
        #endregion
    }
}
