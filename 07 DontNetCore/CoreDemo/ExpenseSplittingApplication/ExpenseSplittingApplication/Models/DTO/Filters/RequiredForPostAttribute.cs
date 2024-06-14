using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO.Filters
{
    /// <summary>
    /// Custom validation attribute to ensure a required field for POST requests.
    /// </summary>
    public class RequiredForPostAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the value based on the HTTP method. For POST requests, the value must not be null or empty.
        /// </summary>
        /// <param name="value">The value of the member to validate.</param>
        /// <param name="validationContext">The context in which the validation is performed.</param>
        /// <returns>
        /// A <see cref="ValidationResult"/> indicating whether the value is valid.
        /// Returns a validation error if the value is null or empty during a POST request.
        /// </returns>
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));
            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null)
            {
                string httpMethod = httpContextAccessor.HttpContext.Request.Method;

                if (httpMethod == HttpMethods.Post && string.IsNullOrEmpty(value as string))
                {
                    return new ValidationResult("Password is required for user creation", new[] { validationContext.MemberName });
                }
            }
            return ValidationResult.Success;
        }
    }
}
