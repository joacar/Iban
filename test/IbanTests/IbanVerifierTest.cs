using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Joacar.Banking;
using Xunit;

namespace IbanTests
{
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class IbanVerifierTest
    {
        private readonly IbanChecksumCalculator _calculator = new IbanChecksumCalculator();
        
        /// <summary>
        /// Test all IBAN at https://www.iban.com/structure
        /// </summary>
        [Theory]
        [InlineData("AL35202111090000000001234567")]
        [InlineData("AD1400080001001234567890")]
        [InlineData("AT483200000012345864")]
        [InlineData("AZ96AZEJ00000000001234567890")]
        [InlineData("BH02CITI00001077181611")]
        [InlineData("BY86AKBB10100000002966000000")]
        [InlineData("BE71096123456769")]
        [InlineData("BA393385804800211234")]
        [InlineData("BR1500000000000010932840814P2")]
        [InlineData("BG18RZBB91550123456789")]
        [InlineData("CR23015108410026012345")]
        [InlineData("HR1723600001101234565")]
        [InlineData("CY21002001950000357001234567")]
        [InlineData("CZ5508000000001234567899")]
        [InlineData("DK9520000123456789")]
        [InlineData("DO22ACAU00000000000123456789")]
        [InlineData("EG800002000156789012345180002")]
        [InlineData("SV43ACAT00000000000000123123")]
        [InlineData("EE471000001020145685")]
        [InlineData("FO9264600123456789")]
        [InlineData("FI1410093000123458")]
        [InlineData("FR7630006000011234567890189")]
        [InlineData("GE60NB0000000123456789")]
        [InlineData("DE75512108001245126199")]
        [InlineData("GI04BARC000001234567890")]
        [InlineData("GR9608100010000001234567890")]
        [InlineData("GL8964710123456789")]
        [InlineData("GT20AGRO00000000001234567890")]
        [InlineData("VA59001123000012345678")]
        [InlineData("HU93116000060000000012345676")]
        [InlineData("IS750001121234563108962099")]
        [InlineData("IQ20CBIQ861800101010500")]
        [InlineData("IE64IRCE92050112345678")]
        [InlineData("IL170108000000012612345")]
        [InlineData("IT60X0542811101000000123456")]
        [InlineData("JO71CBJO0000000000001234567890")]
        [InlineData("KZ563190000012344567")]
        [InlineData("XK051212012345678906")]
        [InlineData("KW81CBKU0000000000001234560101")]
        [InlineData("LV97HABA0012345678910")]
        [InlineData("LB92000700000000123123456123")]
        [InlineData("LI7408806123456789012")]
        [InlineData("LT601010012345678901")]
        [InlineData("LU120010001234567891")]
        [InlineData("MT31MALT01100000000000000000123")]
        [InlineData("MR1300020001010000123456753")]
        [InlineData("MU43BOMM0101123456789101000MUR")]
        [InlineData("MD21EX000000000001234567")]
        [InlineData("MC5810096180790123456789085")]
        [InlineData("ME25505000012345678951")]
        [InlineData("NL02ABNA0123456789")]
        [InlineData("MK07200002785123453")]
        [InlineData("NO8330001234567")]
        [InlineData("PK36SCBL0000001123456702")]
        [InlineData("PS92PALS000000000400123456702")]
        [InlineData("PL10105000997603123456789123")]
        [InlineData("PT50002700000001234567833")]
        [InlineData("QA54QNBA000000000000693123456")]
        [InlineData("RO09BCYP0000001234567890")]
        [InlineData("LC14BOSL123456789012345678901234")]
        [InlineData("SM76P0854009812123456789123")]
        [InlineData("ST23000200000289355710148")]
        [InlineData("SA4420000001234567891234")]
        [InlineData("RS35105008123123123173")]
        [InlineData("SC52BAHL01031234567890123456USD")]
        [InlineData("SK8975000000000012345671")]
        [InlineData("SI56192001234567892")]
        [InlineData("ES7921000813610123456789")]
        [InlineData("SE7280000810340009783242")]
        [InlineData("CH5604835012345678009")]
        [InlineData("TL380010012345678910106")]
        [InlineData("TN5904018104004942712345")]
        [InlineData("TR320010009999901234567890")]
        [InlineData("UA903052992990004149123456789")]
        [InlineData("AE460090000000123456789")]
        [InlineData("GB33BUKB20201555555555")]
        [InlineData("VG21PACG0000000123456789")]
        public void VerifyChecksum(string iban)
        {
            // Arrange
            var digitizer = new IbanDigitizer(iban);

            // Act
            var checksum = _calculator.Checksum(digitizer);

            // Assert
            Assert.True(checksum == 1);
        }

        [Fact]
        public void ThrowException_InvalidCtorArgument_Empty()
        {
            // Arrange, Act, Assert
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentException>(() => new IbanDigitizer(ReadOnlySpan<char>.Empty));
        }
    }
}
