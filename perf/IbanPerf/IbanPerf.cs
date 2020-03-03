using BenchmarkDotNet.Attributes;
using Joacar.Banking;

namespace IbanPerf
{
    [MemoryDiagnoser]
    public class IbanPerf
    {
        private readonly string[] _ibans = 
        {
            "NO8330001234567", /* Length 15 */
            "SE7280000810340009783242" /* Length 24 */,
            "LC14BOSL123456789012345678901234" /* length 32 */
        };

        private IbanChecksumCalculator _calculator;

        [Params(0, 1, 2)]
        public int Index { get; set; }

        public string Iban => _ibans[Index];

        [GlobalSetup]
        public void Setup()
        {
            _calculator = new IbanChecksumCalculator();
        }


        [Benchmark(Description = "Modulo operations")]
        public bool SpaceEfficient()
        {
            var digitizer = new IbanDigitizer(Iban);
            return _calculator.Checksum(digitizer) == 1;
            //var verifier = new IbanChecksumVerifier(Iban);
            //return verifier.VerifyChecksum();
        }

        [Benchmark(Baseline = true, Description = "BigInteger")]
        public bool StringManipulation()
        {
            return IbanComparison.Validate(Iban);
        }
    }
}
