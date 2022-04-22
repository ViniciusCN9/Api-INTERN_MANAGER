using System.ComponentModel.DataAnnotations;

namespace DesafioAPI.application.Validations
{
    public class AbbreviationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null || string.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success;
                
            return value.ToString().Length == 4 ? ValidationResult.Success : new ValidationResult("Abreviação deve conter 4 letras");
        }
    }
}