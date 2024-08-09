using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCPT.ABSTACTION
{
    public class CustomRoleValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null &&
                !string.IsNullOrEmpty(value.ToString()) &&
                !string.IsNullOrWhiteSpace(value.ToString()))
            {
                if (Enum.TryParse<Role>(value.ToString(), out var result))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage);

        }
    }
}
