using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO.Filters
{
    public class ZeroForPostAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));
            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null)
            {
                string httpMethod = httpContextAccessor.HttpContext.Request.Method;

                if (httpMethod == HttpMethods.Post && (int)value != 0)
                {
                    return new ValidationResult("userid must be 0 for user creation", new[] { validationContext.MemberName });
                }
            }
            return ValidationResult.Success;
        }
    }
}
