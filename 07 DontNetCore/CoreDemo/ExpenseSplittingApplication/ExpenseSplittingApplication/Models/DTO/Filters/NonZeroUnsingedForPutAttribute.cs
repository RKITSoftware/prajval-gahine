using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO.Filters
{
    /// <summary>
    /// Custom validation attribute to ensure non-zero unsigned integer for PUT requests.
    /// </summary>
    public class NonZeroUnsingedForPutAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the value based on the HTTP method. For PUT requests, the value must be a non-zero unsigned integer.
        /// </summary>
        /// <param name="value">The value of the member to validate.</param>
        /// <param name="validationContext">The context in which the validation is performed.</param>
        /// <returns>
        /// A <see cref="ValidationResult"/> indicating whether the value is valid. 
        /// Returns a validation error if the value is zero or less during a PUT request.
        /// </returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor)validationContext.GetRequiredService(typeof(IHttpContextAccessor));
            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null)
            {
                string httpMethod = httpContextAccessor.HttpContext.Request.Method;

                if (httpMethod == HttpMethods.Put && (int?)value <= 0)
                {
                    return new ValidationResult("Invalid user ID", new string[] { validationContext.MemberName ?? "" });
                }
            }
            return ValidationResult.Success;
        }
    }
}
