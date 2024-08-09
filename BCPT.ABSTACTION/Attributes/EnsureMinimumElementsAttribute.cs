using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCPT.ABSTACTION
{
    public class EnsureMinimumElementsAttribute : ValidationAttribute
    {
        private readonly int _minimumElements;

        public EnsureMinimumElementsAttribute(int minimumElements)
        {
            _minimumElements = minimumElements;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as List<AccountDto>;

            if (list == null || list.Count < _minimumElements)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
