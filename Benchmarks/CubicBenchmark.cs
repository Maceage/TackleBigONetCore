using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using TackleBigONetCore.Domain;
using TackleBigONetCore.Models;

namespace TackleBigONetCore.Benchmarks
{
    public class CubicBenchmark
    {
        private const int N = 999;

        private readonly IList<LabRat> _labRats;
        private readonly IList<Maze> _mazes;
        private readonly IList<Race> _races;

        public CubicBenchmark()
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

            _races = new List<Race>(N);

            for (int i = 0; i < N; i++)
            {
                _races.Add(new Race
                {
                    MazeNumber = i % (N / 3),
                    Participant = i,
                    FinishTime = (FinishTime)(i % 3)
                });
            }
        }

        [Benchmark]
        public int DummyBenchmark()
        {
            int result = 0;

            foreach (var maze in _mazes)
            {
                foreach (var labRat in _labRats)
                {
                    var race = _races.Where(r => r.MazeNumber == N / 3 - 1).ToArray();

                    result = maze.MazeNumber == race[2].MazeNumber && labRat.TrackingId == race[2].Participant
                        ? (int) race[2].FinishTime
                        : result;
                }
            }

            return result;
        }
    }
}