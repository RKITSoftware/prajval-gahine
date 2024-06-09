using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace ExpenseSplittingApplication.Models.DTO.Filters
{
    public class NonZeroUnsingedForPutAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor)validationContext.GetRequiredService(typeof(IHttpContextAccessor));
            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null)
            {
                string httpMethod = httpContextAccessor.HttpContext.Request.Method;

                if (httpMethod == HttpMethods.Put && (int?)value <= 0)
                {
                    return new ValidationResult("invalid userid", new string[] { validationContext.MemberName ?? "" });
                }
            }
            return ValidationResult.Success;
        }
    }
}
