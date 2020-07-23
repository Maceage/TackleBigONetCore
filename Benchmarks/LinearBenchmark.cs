using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using TackleBigONetCore.Domain;
using TackleBigONetCore.Models;

namespace TackleBigONetCore.Benchmarks
{
    public class LinearBenchmark
    {
        private const int N = 999;

        private readonly IList<LabRat> _labRats;

        public LinearBenchmark()
        {
            _labRats = new List<LabRat>(N);

            for (int i = 0; i < N; i++)
            {
                _labRats.Add(new LabRat
                {
                    TrackingId = i,
                    Color = (Color)(i % 3)
                });
            }
        }

        [Benchmark]
        public int DummyBenchmark()
        {
            var result = _labRats.First(l => l.TrackingId == N - 1);
            
            return (int)result.Color;
        }
    }
}