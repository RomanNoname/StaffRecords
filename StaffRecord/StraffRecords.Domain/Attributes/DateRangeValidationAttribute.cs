using System.ComponentModel.DataAnnotations;

namespace StaffRecords.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DateRangeValidationAttribute : ValidationAttribute
    {
        private readonly int _minYearsAgo;
        private readonly int _maxYearsAgo;

        public DateRangeValidationAttribute(int minYearsAgo, int maxYearsAgo)
        {
            _minYearsAgo = minYearsAgo;
            _maxYearsAgo = maxYearsAgo;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                DateTime minDateOfBirth = DateTime.Now.AddYears(-_maxYearsAgo);
                DateTime maxDateOfBirth = DateTime.Now.AddYears(-_minYearsAgo);

                if (dateOfBirth < minDateOfBirth || dateOfBirth > maxDateOfBirth)
                {
                    string fieldName = validationContext.MemberName;
                    return new ValidationResult($"Невалідне значення в полі '{fieldName}'. Межа від {_minYearsAgo} до {_maxYearsAgo}.");
                }
            }

            return ValidationResult.Success;
        }
    }


}
