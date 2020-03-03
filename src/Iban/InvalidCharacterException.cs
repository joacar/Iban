using System;

namespace Joacar.Banking
{
    /// <summary>
    /// Invalid character detected in IBAN.
    /// </summary>
    public sealed class InvalidCharacterException : Exception
    {
        private const string MessageFormat = "Invalid character '{0}' found at position {1}";

        internal InvalidCharacterException(int i, char c) :base(string.Format(MessageFormat, c, i))
        {

        }
    }
}