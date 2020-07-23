using BenchmarkDotNet.Running;
using TackleBigONetCore.Benchmarks;

namespace TackleBigONetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // From: https://www.red-gate.com/simple-talk/dotnet/net-development/tackle-big-o-notation-in-net-core/
            
            BenchmarkRunner.Run<LinearBenchmark>();
            BenchmarkRunner.Run<QuadraticBenchmark>();
            BenchmarkRunner.Run<QuadraticDictionaryBenchmark>();
            BenchmarkRunner.Run<CubicBenchmark>();
            BenchmarkRunner.Run<CubicDictionaryBenchmark>();
            BenchmarkRunner.Run<LinearBenchmark>();
        }
    }
}
