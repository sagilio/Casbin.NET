using BenchmarkDotNet.Running;

namespace NetCasbin.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var _ = BenchmarkRunner.Run<PolicyManagementBenchmark>();
        }
    }
}
