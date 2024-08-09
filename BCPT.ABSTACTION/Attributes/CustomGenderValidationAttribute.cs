using System.ComponentModel.DataAnnotations;

namespace BCPT.ABSTACTION
{
    public class CustomGenderValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null &&
                !string.IsNullOrEmpty(value.ToString()) &&
                !string.IsNullOrWhiteSpace(value.ToString()))
            {
                if (Enum.TryParse<Gender>(value.ToString().ToTitleCase(), out var result))
                    return ValidationResult.Success;
                else
                    return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
