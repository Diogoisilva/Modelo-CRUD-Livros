using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Modelo.Cross.Helpers
{
    public static class ValidationHelper
    {
        public static void ValidateString(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{fieldName} não pode ser vazio ou nulo.");
            }
        }

        public static void ValidateDecimalRange(decimal value, decimal minValue, decimal maxValue, string fieldName)
        {
            if (value < minValue || value > maxValue)
            {
                throw new ArgumentOutOfRangeException($"{fieldName} deve estar entre {minValue} e {maxValue}.");
            }
        }

        public static void ValidateIntRange(int value, int minValue, int maxValue, string fieldName)
        {
            if (value < minValue || value > maxValue)
            {
                throw new ArgumentOutOfRangeException($"{fieldName} deve estar entre {minValue} e {maxValue}.");
            }
        }
    }
}

