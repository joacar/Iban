using System;

namespace Joacar.Banking
{
    /// <summary>
    /// Parse IBAN number into digits in a format suitable for module 97 algorithm
    /// https://en.wikipedia.org/wiki/International_Bank_Account_Number#Modulo_operation_on_IBAN. The start index is
    /// <c>4</c> (fifth character) according to algorithm.
    /// </summary>
    public ref struct IbanDigitizer
    {
        private const char Null = '\0';

        private readonly ReadOnlySpan<char> _iban;
        private char _current;
        private int _index;

        public IbanDigitizer(ReadOnlySpan<char> iban)
        {
            if (iban.IsEmpty)
            {
                throw new ArgumentException("IBAN must be of length greater than zero", nameof(iban));
            }

            _iban = iban;
            _current = Null;
            _index = -1;
        }

        /// <summary>
        /// Check if more data is available.
        /// </summary>
        /// <remarks>
        /// The index starts at <c>4</c> (fifth character).
        /// </remarks>
        /// <returns></returns>
        public bool MoveNext()
        {
            if (_index < 0)
            {
                // Start at the fourth character
                _index = 4;
                return true;
            }

            if (_current == Null)
            {
                ++_index;
            }

            return _index < _iban.Length + 4;
        }

        /// <summary>
        /// Gets the current integer.
        /// </summary>
        /// <remarks>
        /// When a character is encountered it is split into two digits according to <c>A=10,...,Z=35</c>.
        /// </remarks>
        /// <exception cref="InvalidCharacterException">Thrown if IBAN contains invalid character.</exception>
        public int Current
        {
            get
            {
                if (_index < 0)
                {
                    throw new InvalidOperationException("Call MoveNext to initialize");
                }

                if (_current != Null)
                {
                    // First digit already handled in previous iteration
                    // TODO: Optimize
                    var integer = (_current - 55) % 10;
                    _current = Null;
                    return integer;
                }

                var c = _iban[_index % _iban.Length];
                if (char.IsDigit(c))
                {
                    return c - '0';
                }

                if (char.IsLetter(c))
                {
                    _current = c;
                    // TODO: Optimize
                    return (int)((c - 55) / (double)10);
                }

                throw new InvalidCharacterException(_index, c);
            }
        }
    }
}
