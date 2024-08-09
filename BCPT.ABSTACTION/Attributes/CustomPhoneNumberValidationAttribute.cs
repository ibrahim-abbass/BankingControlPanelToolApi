using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCPT.ABSTACTION
{
    public class CustomPhoneNumberValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null &&
                !string.IsNullOrEmpty(value.ToString()) &&
                !string.IsNullOrWhiteSpace(value.ToString()))
            {
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();
                string phoneNumberString = value as string;

                if (string.IsNullOrEmpty(phoneNumberString))
                    return new ValidationResult(ErrorMessage);

                try
                {
                    PhoneNumber phoneNumber = phoneNumberUtil.Parse(phoneNumberString, null);
                    if (!phoneNumberUtil.IsValidNumber(phoneNumber))
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
                catch (NumberParseException)
                {
                    return new ValidationResult(ErrorMessage);
                }

            }
            return ValidationResult.Success;
        }
    }
}
