namespace Joacar.Banking
{
    public sealed class IbanChecksumCalculator
    {
        public int Checksum(IbanDigitizer digitizer)
        {
            var digits = 0;
            var n = 0;
            int d;
            while (digitizer.MoveNext())
            {
                // We need to read until we have a nine
                // digit number. Since it can start with zero
                // it's not possible to use Math.Log10().
                var integer = digitizer.Current;
                if (++digits < 9)
                {
                    n = 10 * (n + integer);
                    continue;
                }

                n += integer;
                d = n % 97;
                n = 10 * d;
                digits = d > 10 ? 2 : 1;
            }

            // Last digit is base 10^0 so "divide it away"
            d = n / 10 % 97;
            return d; // == 1;
        }
    }
}
