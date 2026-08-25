using System.Text.RegularExpressions;

namespace UsersApi.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string OnlyDigits(this string value)
        {
            return value is null ? string.Empty : Regex.Replace(value, "[^0-9]", "");
        }

        public static bool IsValidCpf(this string cpf)
        {
            if (cpf.IsNullOrEmpty() || cpf.Length != 11 || cpf.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var tempCpf = cpf[..9];
            var soma = 0;

            for (var i = 0; i < 9; i++)
                soma += (tempCpf[i] - '0') * multiplicador1[i];

            var resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            var digito = resto.ToString();
            tempCpf += digito;
            soma = 0;

            for (var i = 0; i < 10; i++)
                soma += (tempCpf[i] - '0') * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito += resto;

            return cpf.EndsWith(digito);
        }
    }
}
