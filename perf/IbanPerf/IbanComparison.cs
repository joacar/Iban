using System.Globalization;
using System.Linq;
using System.Numerics;

namespace IbanPerf
{
    /// <summary>
    /// Naive benchmark implementation.
    /// </summary>
    public static class IbanComparison
    {
        public static bool Validate(string iban)
        {
            iban = iban.Substring(4) + iban.Substring(0, 4);
            var number = string.Join(string.Empty, iban.Select(c => char.IsDigit(c) ? char.GetNumericValue(c).ToString(CultureInfo.InvariantCulture) : (c - 55).ToString()));
            return BigInteger.Parse(number) % 97 == 1;
        }
    }
}
