using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

namespace IbanPerf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ManualConfig.Create(DefaultConfig.Instance);
            config.Add(Job.ShortRun.With(InProcessEmitToolchain.Instance));
            var summary = BenchmarkRunner.Run<IbanPerf>(config);
            //var summary = BenchmarkRunner.Run<IbanPerf>();
        }
    }
}