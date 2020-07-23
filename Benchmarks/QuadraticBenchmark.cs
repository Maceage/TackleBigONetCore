using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using TackleBigONetCore.Domain;
using TackleBigONetCore.Models;

namespace TackleBigONetCore.Benchmarks
{
    public class QuadraticBenchmark
    {
        private const int N = 999;

        private readonly IList<LabRat> _labRats;
        private readonly IList<Maze> _mazes;

        public QuadraticBenchmark()
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
            
            _mazes = new List<Maze>(N / 3);
            
            for (int i = 0; i < N; i++)
            {
                _mazes.Add(new Maze
                {
                    MazeNumber = i,
                    Difficulty = (Difficulty)(i % 3),
                    LabRat1 = i,
                    LabRat2 = i + N / 3,
                    LabRat3 = i + N / 3 * 2
                });
            }
        }

        [Benchmark]
        public int DummyBenchmark()
        {
            int result = 0;

            foreach (var maze in _mazes)
            {
                var labRat = _labRats.First(l => l.TrackingId == N - 1);

                result = maze.LabRat3 == labRat.TrackingId ? (int) maze.Difficulty : result;
            }

            return result;
        }
    }
}