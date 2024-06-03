
using System.ComponentModel.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO.Filters
{
    public class RequiredForPostAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor) validationContext.GetService(typeof(IHttpContextAccessor));
            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null)
            {
                string httpMethod = httpContextAccessor.HttpContext.Request.Method;

                if (httpMethod == HttpMethods.Post && string.IsNullOrEmpty(value as string))
                {
                    return new ValidationResult("niet! niet!", new[] { validationContext.MemberName });
                }
            }
            return ValidationResult.Success;
        }
    }
}
