using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO.Filters
{
    /// <summary>
    /// Validation attribute to ensure userid is zero for user creation during HTTP POST.
    /// </summary>
    public class ZeroForPostAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the userid for user creation during HTTP POST.
        /// </summary>
        /// <param name="value">The value of the userid.</param>
        /// <param name="validationContext">The context for validation.</param>
        /// <returns>A ValidationResult indicating success or failure with an error message.</returns>
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            // Retrieve IHttpContextAccessor from validation context
            IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));

            // Ensure IHttpContextAccessor and HttpContext are not null
            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null)
            {
                // Retrieve HTTP method from current request
                string httpMethod = httpContextAccessor.HttpContext.Request.Method;

                // Check if HTTP method is POST and userid is not zero
                if (httpMethod == HttpMethods.Post && (int)value != 0)
                {
                    // Return validation error if userid is not zero
                    return new ValidationResult("userid must be 0 for user creation", new[] { validationContext.MemberName });
                }
            }

            // Return success if validation passes
            return ValidationResult.Success;
        }
    }
}
