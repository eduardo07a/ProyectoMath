using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Core
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var password = value as string;

            if (string.IsNullOrEmpty(password))
                return false;

            var regex = new Regex(@"^.{5,}$");

            return regex.IsMatch(password);
        }

        public override string FormatErrorMessage(string name)
        {
            return "La contraseña debe tener al menos 5 caracteres";
        }
    }
}
