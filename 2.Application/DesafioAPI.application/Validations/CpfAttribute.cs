using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAPI.application.Validations
{
    public class CpfAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null || string.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success;

            return IsCpfValid((string) value) ? ValidationResult.Success :  new ValidationResult("Cpf inválido");
        }

        private bool IsCpfValid(string cpf)
        {
            int[] firstMultiplier = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] secondMultiplier = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string temporaryCpf;
            string digit;

            int sum;
            int rest;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            
            if (cpf.Length != 11)
                return false;

            temporaryCpf = cpf.Substring(0, 9);
            sum = 0;

            for(int i=0; i<9; i++)
                sum += int.Parse(temporaryCpf[i].ToString()) * firstMultiplier[i];

            rest = sum % 11;
            if ( rest < 2 )
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();
            temporaryCpf = temporaryCpf + digit;

            sum = 0;
            for(int i=0; i<10; i++)
                sum += int.Parse(temporaryCpf[i].ToString()) * secondMultiplier[i];

            rest = sum % 11;
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = digit + rest.ToString();
            return cpf.EndsWith(digit);
        }
    }
}