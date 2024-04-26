using FirmAdvanceDemo.Utility;
using System.ComponentModel.DataAnnotations;

namespace FirmAdvanceDemo.Models.DTO.DataAnnotations
{
    public class RequiredWhenAdmin : ValidationAttribute
    {
        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns>True if the value is valid; otherwise, false.</returns>
        public override bool IsValid(object value)
        {
            if (GeneralUtility.IsAdmin() && value == null)
            {
                return false;
            }
            return true;
        }
    }
}