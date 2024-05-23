using System.ComponentModel.DataAnnotations;

namespace School.CustomeValidations
{
    public class CouresEndDateAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var selectedDate = (DateTime)value;

            if (selectedDate > DateTime.Now.AddMonths(3))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Expiry date must be valid future date..", new[] { validationContext.MemberName });
            }
        }
       
    }
}
