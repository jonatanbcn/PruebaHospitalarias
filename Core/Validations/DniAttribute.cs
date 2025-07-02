using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Core.Validations
{
    public class DniAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            string dni = value.ToString();
            if (string.IsNullOrEmpty(dni)) return ValidationResult.Success;

            // Validar formato DNI español (8 números + 1 letra)
            var regex = new Regex(@"^[0-9]{8}[A-Za-z]$");
            if (!regex.IsMatch(dni))
            {
                return new ValidationResult("El DNI debe tener 8 números seguidos de una letra");
            }

            // Validar letra correcta
            string numero = dni.Substring(0, 8);
            string letra = dni.Substring(8, 1).ToUpper();
            string letrasValidas = "TRWAGMYFPDXBNJZSQVHLCKE";
            int numeroDni = int.Parse(numero);
            char letraCorrecta = letrasValidas[numeroDni % 23];

            if (letra != letraCorrecta.ToString())
            {
                return new ValidationResult("La letra del DNI no es correcta");
            }

            return ValidationResult.Success;
        }
    }

}

